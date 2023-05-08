using FileHandling;
using Types;
using Natures;
using Items;
using Moves;
using Abilities;
using Parsing;

//We're just going to read each file we want and then parse it into our character information
namespace Characters{

    public struct MainFields {
        public string name { get; private set; }
        public Typing type_one { get; private set; }
        public Typing? type_two { get; private set; }
        public string occupation { get; private set; }
        public NatureType nature { get; private set; }
        public int level { get; private set; }
        public int experience { get; private set; }
        public string pokemon { get; private set; }
        public string background { get; private set; }
        public string notes { get; private set; }

        public MainFields(string name, Typing type_one, Typing? type_two, string occupation, NatureType nature, int level, int experience, string pokemon, string background, string notes) {
            this.name = name;
            this.type_one = type_one;
            this.type_two = type_two;
            this.occupation = occupation;
            this.nature = nature;
            this.level = level;
            this.experience = experience;
            this.pokemon = pokemon;
            this.background = background;
            this.notes = notes;
        }
    }

    public struct PointFields {
        public int health_points { get; private set; }
        public int belly_points { get; private set; }
        public int power_points { get; private set; } 

        public PointFields(int health_points, int belly_points, int power_points) {
            this.health_points = health_points;
            this.belly_points = belly_points;
            this.power_points = power_points;
        }
    }

    public struct AttributeFields {
        public int strength { get; private set; }
        public int dexterity { get; private set; }
        public int constitution { get; private set; }
        public int intelligence { get; private set; }
        public int wisdom { get; private set; }
        public int charisma { get; private set; }

        public AttributeFields(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma) {
            this.strength = strength;
            this.dexterity = dexterity;
            this.constitution = constitution;
            this.intelligence = intelligence;
            this.wisdom = wisdom;
            this.charisma = charisma;
        }
    }


    public struct SkillFields {
        public bool acrobatics { get; private set; } 
        public bool athletics { get; private set; } 
        public bool deception { get; private set; } 
        public bool history { get; private set; } 
        public bool insight { get; private set; } 
        public bool intimidation { get; private set; } 
        public bool investigation { get; private set; } 
        public bool medicine { get; private set; } 
        public bool nature { get; private set; } 
        public bool perception { get; private set; } 
        public bool performance { get; private set; } 
        public bool persuasion { get; private set; } 
        public bool religion { get; private set; } 
        public bool sleight_of_hand { get; private set; } 
        public bool stealth { get; private set; } 
        public bool survival { get; private set; } 
        public bool aura { get; private set; } 
        public bool instinct { get; private set; } 

        public SkillFields(bool acrobatics, bool athletics, bool deception, bool history, bool insight, bool intimidation, bool investigation, bool medicine, 
                          bool nature, bool perception, bool performance, bool persuasion, bool religion, bool sleight_of_hand, bool stealth, bool survival, 
                          bool aura, bool instinct) {
            this.acrobatics = acrobatics;
            this.athletics = athletics;
            this.deception = deception;
            this.history = history;
            this.insight = insight;
            this.intimidation = intimidation;
            this.investigation = investigation;
            this.medicine = medicine;
            this.nature = nature;
            this.perception = perception;
            this.performance = performance;
            this.persuasion = persuasion;
            this.religion = religion;
            this.sleight_of_hand = sleight_of_hand;
            this.stealth = stealth;
            this.survival = survival;
            this.aura = aura;
            this.instinct = instinct;
        }
    }

    public struct StatFields {
        public int attack { get; private set; } 
        public int special_attack { get; private set; } 
        public int defense { get; private set; } 
        public int special_defense { get; private set; } 
        public int speed { get; private set; } 

        public StatFields(int attack, int special_attack, int defense, int special_defense, int speed) {
            this.attack = attack;
            this.special_attack = special_attack;
            this.defense = defense;
            this.special_defense = special_defense;
            this.speed = speed;
        }
    }

    public struct InventoryFields{
        public int carrying_capacity { get; private set; }
        public List<Item> items { get; private set; }

        public InventoryFields(int capacity, List<Item> item_list)
        {
            carrying_capacity = capacity;
            items = item_list;

            if (item_list.Count > carrying_capacity)
            {
                throw new ArgumentException("The total amount of the items exceeds the carrying capacity of the inventory.");
            }
        }
    }

    public struct MoveFields{
        public List<Move> moves { get; private set; }
        public MoveFields(List<Move> moves){
            this.moves = moves;
        }
    }

    public struct AbilityFields{
        public List<Ability> abilities { get; private set; }
        public AbilityFields(List<Ability> abilities){
            this.abilities = abilities;
        }
    }


    public struct CharacterFields{
        public MainFields main_fields { get; private set; }
        public PointFields point_fields { get; private set; } 
        public AttributeFields attribute_fields { get; private set; } 
        public SkillFields skill_fields { get; private set; } 
        public StatFields stat_fields { get; private set; } 
        public InventoryFields inventory_fields { get; private set; } 
        public MoveFields move_fields  { get; private set; }
        public AbilityFields ability_fields  { get; private set; }  

        public CharacterFields(MainFields main, PointFields points, AttributeFields attributes, SkillFields skills, 
                                StatFields stats, InventoryFields inv, MoveFields moves, AbilityFields abilites){
            main_fields = main;
            point_fields = points;
            attribute_fields = attributes;
            skill_fields = skills;
            stat_fields = stats;
            inventory_fields = inv;
            move_fields = moves;
            ability_fields = abilites;
        }
    }

    public class CharacterParser{
        public Parser main_parser = new Parser();

        public T[] StripFirstElement<T>(T[] array){
            T[] new_array = new T[array.Length-1];
            
            for(int i = 1; i < array.Length; i++){
                new_array[i-1] = array[i]; 
            }
            return new_array;
        }

        public void ReadCharacters(string filename, int number){
            for(int i = 1; i <= number; i++){
                Game.AddCharacter(ReadCharacter(filename, i));
            }
        }

        public Character ReadCharacter(string filename, int num){
            FileReader fr = new FileReader();
            string[] columns = fr.ReadFileByColumns(filename, num).Split('\n');

            MainFields main_fields = new MainFields();
            PointFields point_fields = new PointFields(); 
            AttributeFields attribute_fields = new AttributeFields();
            SkillFields skill_fields = new SkillFields();
            StatFields stat_fields = new StatFields();
            InventoryFields inv_fields = new InventoryFields();
            MoveFields move_fields = new MoveFields();
            AbilityFields ability_fields = new AbilityFields();

            int i = 0;
            try{
                for(i = 0; i < columns.Length; i++){
                    string[] column_information = columns[i].Split(",-=-,");
                    column_information = StripFirstElement<string>(column_information);
                    if(i == 0){
                        main_fields = GrabMainFields(column_information);
                    } else
                    if(i == 1){
                        try{
                            point_fields = GrabPointFields(column_information);
                        }catch(FormatException format_error){
                            throw new ArgumentException("Point fields are not all integers");
                        }
                    } else
                    if(i == 2){
                        try{
                            attribute_fields = GrabAttributeFields(column_information);
                        }catch(FormatException format_error){
                            throw new ArgumentException("Attribute fields are not all integers");
                        }
                    } else 
                    if(i == 3){
                        try{
                            skill_fields = GrabSkillFields(column_information);
                        }catch(FormatException format_error){
                            throw new ArgumentException("Skill fields are not all booleans.");
                        }
                    } else
                    if(i == 4){
                        try{
                            stat_fields = GrabStatFields(column_information);
                        }catch(FormatException format_error){
                            throw new ArgumentException("Stat fields are not all integers");
                        }
                    } else
                    if(i == 5){
                        inv_fields = GrabInventoryFields(column_information);
                    } else
                    if(i == 6){
                        // foreach(string s in column_information){
                        //     Console.WriteLine(s);
                        // }

                        move_fields = GrabMoveFields(column_information);
                    } else 
                    if(i == 7){
                        ability_fields = GrabAbilityFields(column_information);
                    }
                }
            }catch(Exception e){
                throw new ArgumentException("Failure in parsing character at step " + i + ". Error details here:\n" + e.Message);
            }

            CharacterFields character_fields = new CharacterFields(main_fields, point_fields, attribute_fields, skill_fields, stat_fields, inv_fields, move_fields, ability_fields);

            return new Character(character_fields);
        }

        private AbilityFields GrabAbilityFields(string[] information){
            List<Ability> abilities = new List<Ability>();
            foreach(string ability in information){
                abilities.Add(Game.GetAbility(ability));
            }

            return new AbilityFields(abilities);
        }

        public bool IsAlphabeticalOrWhiteSpace(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c)){
                    return false;
                }
            }
            return true;
        }

        private MoveFields GrabMoveFields(string[] information){
            List<Move> moves = new List<Move>();
            for(int i = 0; i < information.Length; i ++){
                
                try{
                    moves.Add(Game.FindMove(information[i]));
                } catch(Exception e){

                }
                
            }

            return new MoveFields(moves);
        }

        private InventoryFields GrabInventoryFields(string[] information){
            int capacity = int.Parse(information[0]);

            List<Item> items = new List<Item>();
            for(int i = 1; i < information.Length; i++){
                items.Add(Game.GetItem(information[i]));
            }

            return new InventoryFields(capacity, items);
        }

        private StatFields GrabStatFields(string[] information){
            int attack = int.Parse(information[0]);
            int special_attack = int.Parse(information[1]);
            int defense = int.Parse(information[2]);
            int special_defense = int.Parse(information[3]);
            int speed = int.Parse(information[4]);
            return new StatFields(attack, special_attack, defense, special_defense, speed);
        }

        private SkillFields GrabSkillFields(string[] information){
            bool acrobatics = bool.Parse(information[0]);
            bool athletics = bool.Parse(information[1]);
            bool deception = bool.Parse(information[2]);
            bool history = bool.Parse(information[3]);
            bool insight = bool.Parse(information[4]);
            bool intimidation = bool.Parse(information[5]);
            bool investigation = bool.Parse(information[6]);
            bool medicine = bool.Parse(information[7]);
            bool nature = bool.Parse(information[8]);
            bool perception = bool.Parse(information[9]);
            bool performance = bool.Parse(information[10]);
            bool persuasion = bool.Parse(information[11]);
            bool religion = bool.Parse(information[12]);
            bool sleight_of_hand = bool.Parse(information[13]);
            bool stealth = bool.Parse(information[14]);
            bool survival = bool.Parse(information[15]);
            bool aura = bool.Parse(information[16]);
            bool instinct = bool.Parse(information[17]);
            return new SkillFields(acrobatics, athletics, deception, history, insight, intimidation, investigation,
                                    medicine, nature, perception, performance, persuasion, religion, sleight_of_hand,
                                    stealth, survival, aura, instinct);
        }

        private AttributeFields GrabAttributeFields(string[] information){
            int strength = int.Parse(information[0]);
            int dexterity = int.Parse(information[1]);
            int constitution = int.Parse(information[2]);
            int intelligence = int.Parse(information[3]);
            int wisdom = int.Parse(information[4]);
            int charisma = int.Parse(information[5]);
            return new AttributeFields(strength, dexterity, constitution, intelligence, wisdom, charisma);
        }

        private PointFields GrabPointFields(string[] information){
            int hp = int.Parse(information[0]);
            int bp = int.Parse(information[1]);
            int pp = int.Parse(information[2]);

            return new PointFields(hp, bp, pp);
        }

        private MainFields GrabMainFields(string[] information){
            string name = information[0];
            Typing type_one = main_parser.ParseType(information[1]);
            Typing? type_two;
            try{
                type_two = main_parser.ParseType(information[2]);
            }catch(ArgumentException e){
                type_two = null;
            }
            string occupation = information[3];
            NatureType nature = Parser.ParseNatureType(information[4]);
            int level = int.Parse(information[5]);
            int experience = int.Parse(information[6]);
            string pokemon = information[7];
            string background = information[8];
            string notes = information[9];
            return new MainFields(name, type_one, type_two, occupation, nature, level, experience, pokemon, background, notes);
        }

    }
}
