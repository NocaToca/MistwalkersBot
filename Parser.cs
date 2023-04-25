using System.Text.RegularExpressions;
using Types;
using Moves;
using Characters;

namespace Parsing{

    public class Parser{

        public Parser(){

        }

        public Move InterpretMove(string name, string type, int accuracy, int range, string damage, string effectString){
            Move move = new Move();
            move.name = name;
            move.ability_type = ""; // not used in this example
            move.accuracy = accuracy;
            Tuple<Roll, Typing> tuple = ParseDamage(damage);
            move.damage = tuple.Item1;
            move.type = tuple.Item2;
            //move.effect = InterpretEffect(effectString);
            return move;
        }
        
        //Let's keep in mind what can happen for each effect:
        /*
            Heal
            Status effect
            Attribute change
            Nothing
        */
        public class Effect{

            public bool heal_effect;

            public struct HealInformation{
                public bool percentage_heal;
                public float amount;

                public HealInformation(bool percentage_heal, float amount){
                    this.amount = amount;
                    this.percentage_heal = percentage_heal;
                }
            }

            public HealInformation heal_info;

            public bool status_effect;

            public struct StatusInformation{
                public bool set_duration;
                public int duration;
                public Status status;

                public bool saving_throw = false;

                public SavingThrow? throw_information;

                public StatusInformation(bool set_duration, int duration, Status status, bool saving_throw, SavingThrow? throw_information = null){
                    this.set_duration = set_duration;
                    this.duration = duration;
                    this.status = status;
                    this.saving_throw = saving_throw;
                    this.throw_information = throw_information;
                }
            }

            public StatusInformation status_info;

            public Effect(){

            }

            public string DescribeEffect(){
                string s = "User Effect:\n";
                if(heal_effect){
                    s += "Heal: True";

                    s += "\nHeal Amount: " + heal_info.amount;
                    s = (heal_info.percentage_heal) ? s + "%" : s;
                    s += "\n";
                } else {
                    s += "Heal: False\n";
                }

                if(status_effect){
                    s += "Status: True";
                    s += "\nStatus Name: " + status_info.status.ToString();
                    s = (!status_info.set_duration) ? s + "\nDuration: None" : s + "\nDuration: " + status_info.duration;
                }

                return s;
            }

        }

        public void ParseEffect(string effect_string){
            //We can do this incrementally, first assuming there is one effect for each
            string parsing_string = effect_string.ToLower();

            Effect user_effect = new Effect();
            Effect target_effect = new Effect();

            //First let's find out if it's a heal effect and if it is take the heal amount
            Regex heal_regex = new Regex(@"(heal)s?\s*(([0-9]*\.?[0-9]+)%?)?");
            Match heal_match = heal_regex.Match(parsing_string);

            Regex percent = new Regex(@"%");

            if(heal_match.Success){
                user_effect.heal_effect = true;

                string heal_string = heal_match.Groups[0].Value;
                Match percent_heal_match = percent.Match(parsing_string);

                bool percentage_heal = false;
                if(percent_heal_match.Success){
                    percentage_heal = true;
                }

                string amount_substring = (new Regex("[0-9]+")).Match(heal_string).Groups[0].Value;

                user_effect.heal_info = new Effect.HealInformation(percentage_heal, int.Parse(amount_substring));
            }

            Console.WriteLine(user_effect.DescribeEffect());


            //Then we can look for a status effect as well:
            parsing_string = effect_string.ToLower();
            Regex status_regex = new Regex(@"(paralyze|sleep|burn|flinch)(e?d?)");
            Match status_match = status_regex.Match(parsing_string);

            if(status_match.Success){
                user_effect.status_effect = true;

                string status_string = status_match.Groups[0].Value;

                string pattern = "(" + status_string + ")";
                pattern += @"\s*for\s*([0-9]+)\s*(turn)s?"; 

                Regex status_duration = new Regex(pattern);
                Match duration_match = status_duration.Match(parsing_string);
                if(duration_match.Success){
                    //We'll do duration stuff when we get to it
                } else {
                    user_effect.status_info = new Effect.StatusInformation(false, 0, new Sleep(), false);
                }
            }

            Console.WriteLine(user_effect.DescribeEffect());



        }

        public Tuple<Roll, Typing> ParseDamage(string damage_string){

            //First let's format our string uniformally:
            string parsing_string = damage_string.ToLower();

            //We can work with an example string here:
            //For example Fire Fang is:
            //2d6 + 0.5 STR fire damage 

            //Almost all of our damage is in this form - if it's not it's reliant in the effect and we don't have to worry about it
            //We want to extract how many dice we will use (the integer in front of the d) and the size of the die (the integer after)
            //Then we will grab the additional attribute, which will be 0.5 * the strength stat of the user
            //Finally, we will grab that the move is fire damage!

            //To do this, first let us tackle grabbing the dice:
            Regex dice_regex = new Regex(@"(\d+)d(\d+)"); //The "(\d+)d(\d+)" can be broken down as follows:
            /*
            (\d+): Grab any number of digits
            d: before and after the character d
            (\d+): Grab any number of digits again
            */
            //Then we just match it:
            Match match = dice_regex.Match(parsing_string);

            int num_dice = 0;
            int dice_num = 0;

            if(match.Success){
                num_dice = int.Parse(match.Groups[1].Value);
                dice_num = int.Parse(match.Groups[2].Value);
            } else {
                throw new ArgumentException("Invalid Dice Arguments");
            }

            int[] dice_array = new int[num_dice];
            for(int i = 0; i < num_dice; i++){
                dice_array[i] = dice_num;
            }

            //If the match does not succeed, we are going to assume that we are doing no damage and it's a self target or in the effect

            //Next, we have to get the weights and attribute

            Regex damage_regex = new Regex(@"([-+]?[0-9]*\.?[0-9]+)\s*(str|int|wis|con|dex|cha)");
            MatchCollection matches = damage_regex.Matches(parsing_string);

            float[] weights = new float[matches.Count];
            AttributeType[] attributes = new AttributeType[matches.Count];
            // Console.WriteLine(matches.Count);
            for(int i = 0; i < matches.Count; i++){
                weights[i] = float.Parse(matches[i].Groups[1].Value);
                attributes[i] = ParseAttribute(matches[i].Groups[2].Value);
            }


            //Now we just have to grab the typing.
            Regex type_regex = new Regex(@"\b(\w+)\s*damage$");
            Match type_match = type_regex.Match(parsing_string);

            Typing move_type = null;
            if(match.Success){
                move_type = ParseType(type_match.Groups[1].Value);
            } else {
                throw new ArgumentException("Invalid Damage Arguments");
            }

            Roll move_roll = new Roll(weights, dice_array, attributes);

            Tuple<Roll, Typing> tuple = new Tuple<Roll, Typing>(move_roll, move_type);

            return tuple;
        }

        public AttributeType ParseAttribute(string attribute_string){
            switch(attribute_string.ToLower()){
                case "str":
                    return AttributeType.Strength;
                case "int":
                    return AttributeType.Intelligence;
                case "dex":
                    return AttributeType.Dexterity;
                case "con":
                    return AttributeType.Constitution;
                case "cha":
                    return AttributeType.Charisma;
                case "wis":
                    return AttributeType.Wisdom;
                default:
                    throw new ArgumentException("Invalid attribute string: " + attribute_string);
            }

        }

        public Typing ParseType(string type_string){
            switch (type_string.ToLower()){
                case "normal":
                    return Typing.Normal;
                case "fire":
                    return Typing.Fire;
                case "water":
                    return Typing.Water;
                case "electric":
                    return Typing.Electric;
                case "grass":
                    return Typing.Grass;
                case "ice":
                    return Typing.Ice;
                case "fighting":
                    return Typing.Fighting;
                case "poison":
                    return Typing.Poison;
                case "ground":
                    return Typing.Ground;
                case "flying":
                    return Typing.Flying;
                case "psychic":
                    return Typing.Psychic;
                case "bug":
                    return Typing.Bug;
                case "rock":
                    return Typing.Rock;
                case "ghost":
                    return Typing.Ghost;
                case "dragon":
                    return Typing.Dragon;
                case "dark":
                    return Typing.Dark;
                case "steel":
                    return Typing.Steel;
                case "fairy":
                    return Typing.Fairy;
                default:
                    throw new ArgumentException("Invalid type: " + type_string);
            }
        }

    }

}