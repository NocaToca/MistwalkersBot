//Let's make our own simple language for this
        /*
            First, we can ask "What does an effect do?"
            We have targets:
                -Single
                    -User
                    -Target
                -Range
                    -All
                    -Enemy
                    -Allies
            Targeting will be in the format:
            apply {effect} for target in range{All:10ft}
            or
            apply {effect} for target in single{User}

            Next we have the effect. First, let's think of a simple case like rest:
            "Restore all HP to the user and apply the sleep status effect"

            For the effect portion we can have it be like
            restore{HP:100%} status{sleep}

            Now let's make it able to run through rest
        */
//#define DEBUG_INTERP
// #define DEBUG_PARSESUBEFFECT
#pragma warning disable
using System.Text.RegularExpressions;
using Characters;
using Parsing;
using Moves;
using Types;

public class ApplyInformation{
    public Func<Character, object> Apply {get; set;}
    public Type ReturnType {get; set;}
} 

namespace Interpreter{

    public class MoveInterpreter{

        Parser main_parser;

        public MoveInterpreter(){
            main_parser = new Parser();
        }

        public Move InterpretMove(string[] move_cell_information){
            /*
                These cells should be pretty uniform - if they are inputted wrong we will throw an exception
            */

            //Most of this is just parsing besides the final bit
            //Robust debugging!
            string name = move_cell_information[0].Trim();

            Move.MoveType move_type = Move.MoveType.Special;
            try{
               move_type = main_parser.ParseMoveType(move_cell_information[1].Trim());
            }catch(Exception e){
                throw new ArgumentException("Failure to parse move. Inputted string: " + move_cell_information[1].Trim()+".\nError message: " + e.Message);
            }
             

            int accuracy = 0;
            try{
                accuracy = main_parser.ParseAccuracy(move_cell_information[2].Trim());
            }catch(Exception e){
                throw new ArgumentException("Failure to parse accuracy. Inputted string: " + move_cell_information[2].Trim()+".\nError message: " + e.Message);
            }

            Moves.Range? range_information = null;
            try{
                range_information = main_parser.ParseRange(move_cell_information[3].Trim());
            }catch(Exception e){
                throw new ArgumentException("Failure to parse range. Inputted string: " + move_cell_information[3].Trim()+".\nError message: " + e.Message);
            }

            

            Tuple<Roll?, Typing>? tuple = null;
            try{
                if(move_cell_information[4].Trim() == "None"){
                    tuple = new Tuple<Roll?, Typing>(null, Typing.Normal);
                } else{
                    tuple = main_parser.ParseDamage(move_cell_information[4].Trim());
                }
            }catch(Exception e){
                throw new ArgumentException("Failure to parse damage. Inputted string: " + move_cell_information[4].Trim()+".\nError message: " + e.Message);
            }

            

            string description = move_cell_information[5].Trim();

            Effect? move_effect = null;
            try{
                move_effect = ParseEffect(move_cell_information[6].Trim());
            }catch(Exception e){
                throw new ArgumentException("Failure to parse effect. Inputted string: " + move_cell_information[6].Trim()+".\nError message: " + e.Message);
            }

            Move move = new Move(name, move_type, accuracy, range_information, tuple.Item1, tuple.Item2, description, move_effect);
            return move;
        }

        

        private string[] SplitArguments(string unsplit_args, char split_char){
            List<string> base_list = new List<string>();

            char[] chars = unsplit_args.ToCharArray();

            string curr_string = "";

            int i = 0;
            for(i = 0; i < unsplit_args.Length; i++){
                if(chars[i] == '{'){
                    break;
                } else
                if(chars[i] == split_char){
                    base_list.Add(curr_string);
                    curr_string = "";
                } else {
                    curr_string += chars[i];
                }
            }

            base_list.Add(curr_string + unsplit_args.Substring(i));

            // foreach(string s in base_list){
            //     Console.WriteLine(s);
            // }

            return base_list.ToArray();
        }

        private ApplyInformation MakeRestoreEffect(string information){

            //For now, this should be just two
            string[] temp = SplitArguments(information, ':');

            string restoring_attribute = temp[0];
            string amount_string = temp[1];

            bool percentage = amount_string.Contains('%'); 
            if(percentage){
                amount_string = amount_string.Replace("%", "");
            }

            try{
                float amount = float.Parse(amount_string);

                amount = (percentage) ? amount/100.0f : amount;

                Character.PointType type = main_parser.ParsePointType(restoring_attribute);


                #if DEBUG_INTERP
                Console.WriteLine("Added Restore Effect for:\nType: " + type.ToString() +" Amount: " + amount.ToString() + " Percentage: " + percentage.ToString());
                #endif

                return new ApplyInformation {
                    Apply = (character) => {
                        return character.main_points.Restore(type, amount, percentage);
                    },
                    ReturnType = typeof(bool)
                };
            }catch(Exception e){
                try{
                    //Second arg has to be an int
                    ApplyInformation number = ParseSubEffect(amount_string);
                    if(number.ReturnType != typeof(int)){
                        throw new ArgumentException("Invalid type for Restore: " + number.ReturnType.ToString());
                    }

                    Character.PointType type = main_parser.ParsePointType(restoring_attribute);

                    return new ApplyInformation {
                    Apply = (character) => {
                        // Console.WriteLine("Test: Restore");
                        return character.main_points.Restore(type, (int)number.Apply(character), percentage);
                    },
                    ReturnType = typeof(bool)
                };
                }catch(Exception e2){
                    throw new ArgumentException("Invalid expression in secondary argument of restore: " + amount_string + "\nAddtional Error Message: " + e2.Message + "\n" + e.Message);
                }
            }
            

        }

        private ApplyInformation MakeStatusEffect(string information) {
            string[] temp = information.Split(':');

            if (temp.Length == 1) {
                // For now, we should just have the effect
                Status main_status = main_parser.ParseStatus(information);

                return new ApplyInformation {
                    Apply = (character) => {
                        return character.AddStatus(main_status);
                    },
                    ReturnType = typeof(bool)
                };
            } else {
                string status_string = temp[0];
                string arg = temp[1];

                DamageOverTime main_status = (DamageOverTime)main_parser.ParseStatus(status_string);

                arg = arg.Replace("%", "");
                main_status.amount = float.Parse(arg);

                return new ApplyInformation {
                    Apply = (character) => {
                        return character.AddStatus(main_status);
                    },
                    ReturnType = typeof(bool)
                };
            }
        }

        //Void type
        class UnitType{

        }

        private ApplyInformation MakeIgnoreEffect(string information){
            Character.IgnoreType type = main_parser.ParseIgnoreType(information);

            return new ApplyInformation {
                Apply = (character) => {
                    character.AddIgnore(type);
                    return new UnitType();
                },
                ReturnType = typeof(UnitType)
            };
        }

        private ApplyInformation MakeLowerEffect(string information){
            Character.StatType type = main_parser.ParseStatType(information);
            
            return new ApplyInformation {
                Apply = (character) => {
                    character.LowerStat(type);
                    return new UnitType();
                },
                ReturnType = typeof(UnitType)
            };
        }

        private ApplyInformation MakeRaiseEffect(string information) {
            Character.StatType type = main_parser.ParseStatType(information);
            
            return new ApplyInformation {
                Apply = (character) => {
                    character.RaiseStat(type);
                    return new UnitType();
                },
                ReturnType = typeof(UnitType)
            };
        }


        private ApplyInformation MakeDivEffect(string information) {
            // First, we split the arguments
            string[] arguments = SplitArguments(information.Trim(), ':');

            // Parse the first argument
            ApplyInformation arg1_function;
            try {
                int first_arg = int.Parse(arguments[0]);
                arg1_function = new ApplyInformation() {
                    Apply = (character) => first_arg,
                    ReturnType = typeof(int)
                };
            } catch {
                try {
                    arg1_function = ParseSubEffect(arguments[0]);
                } catch (Exception e2) {
                    throw new ArgumentException($"Invalid expression in secondary argument of div: {arguments[0]}\nAdditional Error Message: {e2.Message}");
                }
            }

            // Parse the second argument
            ApplyInformation arg2_function;
            try {
                int second_arg = int.Parse(arguments[1]);
                arg2_function = new ApplyInformation() {
                    Apply = (character) => second_arg,
                    ReturnType = typeof(int)
                };
            } catch {
                try {
                    arg2_function = ParseSubEffect(arguments[1]);
                } catch (Exception e2) {
                    throw new ArgumentException($"Invalid expression in secondary argument of div: {arguments[1]}\nAdditional Error Message: {e2.Message}");
                }
            }

            // Check that both arguments are of type int
            if (arg1_function.ReturnType != typeof(int)) {
                throw new ArgumentException("Invalid type for div: " + typeof(int).ToString());
            }
            if (arg2_function.ReturnType != typeof(int)) {
                throw new ArgumentException("Invalid type for div: " + typeof(int).ToString());
            }

            // Combine the functions into a single function that subtracts the second argument from the first
            return new ApplyInformation {
                Apply = (character) => {
                    int value = (int)arg1_function.Apply(character) / (int)arg2_function.Apply(character);
                    return value;
                },
                ReturnType = typeof(int)
            };
        }
        private ApplyInformation MakeAddEffect(string information) {
            // First, we split the arguments
            string[] arguments = SplitArguments(information.Trim(), ':');
            //Console.WriteLine(information);
            //Console.WriteLine("Test");

            // Parse the first argument
            ApplyInformation arg1_function;
            try {
                int first_arg = int.Parse(arguments[0]);
                arg1_function = new ApplyInformation() {
                    Apply = (character) => first_arg,
                    ReturnType = typeof(int)
                };
            } catch {
                try {
                    arg1_function = ParseSubEffect(arguments[0]);
                } catch (Exception e2) {
                    throw new ArgumentException($"Invalid expression in secondary argument of add: {arguments[0]}\nAdditional Error Message: {e2.Message}");
                }
            }

            // Parse the second argument
            ApplyInformation arg2_function;
            try {
                int second_arg = int.Parse(arguments[1]);
                arg2_function = new ApplyInformation() {
                    Apply = (character) => second_arg,
                    ReturnType = typeof(int)
                };
            } catch {
                try {
                    arg2_function = ParseSubEffect(arguments[1]);
                } catch (Exception e2) {
                    throw new ArgumentException($"Invalid expression in secondary argument of add: {arguments[1]}\nAdditional Error Message: {e2.Message}");
                }
            }

            // Check that both arguments are of type int
            if (arg1_function.ReturnType != typeof(int)) {
                throw new ArgumentException("Invalid type for add: " + typeof(int).ToString());
            }
            if (arg2_function.ReturnType != typeof(int)) {
                throw new ArgumentException("Invalid type for add: " + typeof(int).ToString());
            }

            // Combine the functions into a single function that adds the second argument from the first
            return new ApplyInformation {
                Apply = (character) => {
                    // Console.WriteLine("Test: Add");
                    int value = (int)arg1_function.Apply(character) + (int)arg2_function.Apply(character);
                    return value;
                },
                ReturnType = typeof(int)
            };
        }

        private ApplyInformation MakeSubEffect(string information) {
            // First, we split the arguments
            string[] arguments = SplitArguments(information.Trim(), ':');

            // Parse the first argument
            ApplyInformation arg1_function;
            try {
                int first_arg = int.Parse(arguments[0]);
                arg1_function = new ApplyInformation() {
                    Apply = (character) => first_arg,
                    ReturnType = typeof(int)
                };
            } catch {
                try {
                    arg1_function = ParseSubEffect(arguments[0]);
                } catch (Exception e2) {
                    throw new ArgumentException($"Invalid expression in secondary argument of sub: {arguments[0]}\nAdditional Error Message: {e2.Message}");
                }
            }

            // Parse the second argument
            ApplyInformation arg2_function;
            try {
                int second_arg = int.Parse(arguments[1]);
                arg2_function = new ApplyInformation() {
                    Apply = (character) => second_arg,
                    ReturnType = typeof(int)
                };
            } catch {
                try {
                    arg2_function = ParseSubEffect(arguments[1]);
                } catch (Exception e2) {
                    throw new ArgumentException($"Invalid expression in secondary argument of sub: {arguments[1]}\nAdditional Error Message: {e2.Message}");
                }
            }

            // Check that both arguments are of type int
            if (arg1_function.ReturnType != typeof(int)) {
                throw new ArgumentException("Invalid type for sub: " + typeof(int).ToString());
            }
            if (arg2_function.ReturnType != typeof(int)) {
                throw new ArgumentException("Invalid type for sub: " + typeof(int).ToString());
            }

            // Combine the functions into a single function that subtracts the second argument from the first
            return new ApplyInformation {
                Apply = (character) => {
                    int value = (int)arg1_function.Apply(character) - (int)arg2_function.Apply(character);
                    return value;
                },
                ReturnType = typeof(int)
            };
        }

        private ApplyInformation MakeMulEffect(string information){
            //First, we split the arguments
            string[] arguments = SplitArguments(information.Trim(), ':');

            int first_arg = 0;
            ApplyInformation arg1_function;
            try{
                first_arg = int.Parse(arguments[0]);
                arg1_function = new ApplyInformation(){
                    Apply = (character) => {
                        return first_arg;
                    },
                    ReturnType = typeof(int)
                };
            }catch(Exception e){
                try{
                    arg1_function = ParseSubEffect(arguments[0]);
                }catch(Exception e2){
                    throw new ArgumentException("Invalid expression in secondary argument of restore: " + arguments[0] + "\nAddtional Error Message: " + e2.Message + "\n" + e.Message);
                }
            }

            int second_arg = 0;
            ApplyInformation arg2_function;
            try{
                second_arg = int.Parse(arguments[1]);
                arg2_function = new ApplyInformation(){
                    Apply = (character) => {
                        return second_arg;
                    },
                    ReturnType = typeof(int)
                };
            }catch(Exception e){
                try{
                    arg2_function = ParseSubEffect(arguments[1]);
                }catch(Exception e2){
                    throw new ArgumentException("Invalid expression in secondary argument of restore: " + arguments[1] + "\nAddtional Error Message: " + e2.Message + "\n" + e.Message);
                }
            }

            if(arg1_function.ReturnType != typeof(int)){
                throw new ArgumentException("Invalid type for mul: " + typeof(int).ToString());
            }

            if(arg2_function.ReturnType != typeof(int)){
                throw new ArgumentException("Invalid type for mul: " + typeof(int).ToString());
            }

            
            return new ApplyInformation{
                Apply = (character) => {
                    // Console.WriteLine("Test: Mul");
                    int val2 = (int)arg2_function.Apply(character);
                    // Console.WriteLine(val2.ToString());
                    int value = (int)arg1_function.Apply(character) * val2;
                    return value;
                },
                ReturnType = typeof(int)
            };
        }

        //Special adds a certain modifier to the character that moves can use to perform certain calculations. Most of the time
        //These are stacking things put can also include things like charge or bide
        private ApplyInformation MakeSpecialEffect(string information){
            string[] arguments = SplitArguments(information, ':');

            //First argument is type of special (exists, add, remove)
            if(arguments[0] == "remove"){
                //Remove argument takes type of string, and an integer or all - all meaning that it will remove everything
                try{
                    int amount = int.Parse(arguments[2]);

                    Func<Character, object> main_function = delegate(Character c){

                        int amount_func = -1;
                        try{
                            amount_func = c.RemoveSpecial(arguments[1], amount);
                        }catch(SpecialParameterDoesNotExist e){
                            c.CreateSpecial(arguments[1]);
                            throw new IndexOutOfRangeException("Cannot Remove: Value Empty");
                        }

                        return amount_func;
                    };

                    return new ApplyInformation{
                        Apply = main_function,
                        ReturnType = typeof(int)
                    };

                }catch(FormatException e){
                    if(arguments[2] == "all"){
                        return new ApplyInformation{
                            Apply = (character) =>{
                                try{
                                    // Console.WriteLine("Test: Special");
                                    return character.RemoveAllSpecial(arguments[1]);
                                }catch(SpecialParameterDoesNotExist e){
                                    character.CreateSpecial(arguments[1]);
                                    throw new IndexOutOfRangeException("Cannot Remove: Value Empty");
                                }
                            },
                            ReturnType = typeof(int)
                        };
                    } else {
                        throw new ArgumentException("Invalid Parameter for remove: " + arguments[2]);
                    }
                }catch(Exception e){
                    throw new Exception("Unknown error within Special. Message: " + e.Message);
                }
            } else 
            if(arguments[0] == "add"){
                //Add only really takes two arguments: those being the keyword and how much to add
                try{
                    int amount = int.Parse(arguments[2]);

                    return new ApplyInformation{
                        Apply = (character) => {
                            try{
                                character.AddSpecial(arguments[1], amount);
                            }catch(SpecialParameterDoesNotExist e){
                                character.CreateSpecial(arguments[1]);
                                character.AddSpecial(arguments[1], amount);
                            }

                            return amount;
                        },
                        ReturnType = typeof(int)
                    };

                }catch(FormatException e){
                    throw new ArgumentException("Invalid argument to Special.Add: " + arguments[2] +"\nError message: " + e.Message);
                }

            } else
            if(arguments[0] == "exists"){
                //Exists is more complication as the first one is going to be the keyword and then the second one will be:
                /*
                > - Greater than
                < - Less than
                = - Equal to
                ! - Not equal to
                */
                //So let's just go through and see if the second argument contains any of these
                string query = arguments[2]; //idk how to spell it LMAO

                Func<int, bool> queuery_func;
                if(query.Contains('>')){
                    query = query.Replace(">", "");

                    int comparing_amount = 0;
                    try{
                        comparing_amount = int.Parse(query);
                    }catch(FormatException e){
                        throw new ArgumentException("Invalid Format for Special.Exists: " + query);
                    }

                    queuery_func = delegate(int amount){
                        return amount > comparing_amount;
                    };
                } else
                if(query.Contains('<')){
                    query = query.Replace("<", "");

                    int comparing_amount = 0;
                    try{
                        comparing_amount = int.Parse(query);
                    }catch(FormatException e){
                        throw new ArgumentException("Invalid Format for Special.Exists: " + query);
                    }

                    queuery_func = delegate(int amount){
                        return amount < comparing_amount;
                    };
                } else 
                if(query.Contains('=')){
                    query = query.Replace("=", "");

                    int comparing_amount = 0;
                    try{
                        comparing_amount = int.Parse(query);
                    }catch(FormatException e){
                        throw new ArgumentException("Invalid Format for Special.Exists: " + query);
                    }

                    queuery_func = delegate(int amount){
                        return amount == comparing_amount;
                    };
                } else
                if(query.Contains('!')){
                    query = query.Replace("!", "");

                    int comparing_amount = 0;
                    try{
                        comparing_amount = int.Parse(query);
                    }catch(FormatException e){
                        throw new ArgumentException("Invalid Format for Special.Exists: " + query);
                    }

                    queuery_func = delegate(int amount){
                        return amount != comparing_amount;
                    };
                } else {
                    throw new ArgumentException("Invalid queuery argument for Special.Exists: " + arguments[2]);
                }

                return new ApplyInformation{
                    Apply = (character) =>{
                        return character.QueuerySpecial(arguments[1], queuery_func);
                    },
                    ReturnType = typeof(bool)
                };
            } else {
                throw new ArgumentException("Unknown special argument: " + information);
            }
        }

        private ApplyInformation MakeWeatherEffect(string information){
            //There's two types of weather effects:
            //is and create
            //is checks to see if the weather is a certain type
            //create sets the global weather to that weather
            string[] arguments = SplitArguments(information, ':');

            if(arguments[0] == "is"){

                //For now I am just going to assume that the weather is weather and not an expression
                Weather weather = main_parser.ParseWeather(arguments[1]);

                return new ApplyInformation{
                    Apply = (character) => {
                        return Game.current_weather.ToString() == weather.ToString();
                    },
                    ReturnType = typeof(bool)
                };

            } else
            if(arguments[0] == "create"){

                //For now I am just going to assume that the weather is weather and not an expression
                Weather weather = main_parser.ParseWeather(arguments[1]);

                return new ApplyInformation{
                    Apply = (character) => {
                        Game.current_weather = weather;
                        return new UnitType();
                    },
                    ReturnType = typeof(UnitType)
                };

            } else {
                throw new ArgumentException("Invalid argument for Weather: " + arguments[0]);
            }
        }

        private ApplyInformation MakeKillEffect(string information){

            //Our apply is always going to return false, but tell the character to listen to see if a character has died. This is because
            //it adds the actual event to the listener that reacts if the target has died.
            //This is useful because the function for if is always checked while the one in it is not

            //Also I am not argument checking rn L bozo 
            return new ApplyInformation{
                Apply = (character) =>{
                    character.SetKillEvent(character.failedExpression);
                    return false;
                },
                ReturnType = typeof(bool)
            };

        }

        private ApplyInformation MakeAbsorbEffect(string information){
            //Absorb has 2 arguments, namely the type of absorption and the amount
            //The only type we have for now is going to be damage, since that is the main thing I care about
            string[] arguments = SplitArguments(information, ':');

            if(arguments[0] == "damage"){
                //Next we just want to grab the second argument
                string amount = arguments[1].Replace("%","");
                ApplyInformation integer;

                try{
                    int value = int.Parse(amount);
                    integer = new ApplyInformation{
                        Apply = (character) =>{
                            return value;
                        },
                        ReturnType = typeof(int)
                    };
                }catch(FormatException e){
                    try{
                        integer = ParseSubEffect(amount);
                        if(integer.ReturnType != typeof(int)){
                            throw new ArgumentException("Return type parsed, but not of type integer. Type mismatch.");
                        }
                    } catch(Exception e2){
                        throw new ArgumentException("Invalid parameters in Absorb: " + amount+"\nAddition Error Messages: " + e2.Message);
                    }
                }

                return new ApplyInformation{
                    Apply = (character) =>{
                        character.SetAbsorb((int)integer.Apply(character));
                        return new UnitType();
                    },
                    ReturnType = typeof(UnitType)
                };

            } else {
                throw new ArgumentException("Invalid Absorb arguments: " + information);
            }
        }

        private ApplyInformation MakeDamageEffect(string information){

            ApplyInformation integer;
            try{
                int value = int.Parse(information);
                integer = new ApplyInformation{
                    Apply = (character) => {
                        return value;
                    },
                    ReturnType = typeof(int)
                };
            }catch(FormatException e){
                try{
                    integer = ParseSubEffect(information);
                    if(integer.ReturnType != typeof(int)){
                        throw new ArgumentException("Type mismatch. Type " + integer.ReturnType.ToString() + " is not of type int");
                    }
                }catch(Exception e2){
                    throw new ArgumentException("Invalid Parameter for Damage: " + information + "\nAddition Error Information: " + e2.Message);
                }
            }

            return new ApplyInformation{
                Apply = (character) => {
                    character.ApplyExtraDamage((int)integer.Apply(character));
                    return new UnitType();
                },
                ReturnType = typeof(UnitType)
            };
        }

        private ApplyInformation ParseSubEffect(string sub_effect){

            sub_effect = sub_effect.Trim();

            #if DEBUG_PARSESUBEFFECT
            Console.WriteLine(sub_effect);
            #endif

            //We're just going to look and see if it's any of the effects we want
            Regex restore_regex = new Regex(@"^restore\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match restore_match = restore_regex.Match(sub_effect);

            if(restore_match.Success){
                //Console.WriteLine(restore_match.Groups[1]);
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Restore");
                #endif

                return MakeRestoreEffect(restore_match.Groups[1].Value);
            }

            Regex status_regex = new Regex(@"^status\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match status_match = status_regex.Match(sub_effect);

            if(status_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Status");
                #endif

                return MakeStatusEffect(status_match.Groups[1].Value);
            }

            Regex debuff_regex = new Regex(@"^ignore\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match debuff_match = debuff_regex.Match(sub_effect);

            if(debuff_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Debuff");
                #endif

                return MakeIgnoreEffect(debuff_match.Groups[1].Value);
            }

            Regex lower_regex = new Regex(@"^lower\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match lower_match = lower_regex.Match(sub_effect);

            if(lower_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Lower");
                #endif

                return MakeLowerEffect(lower_match.Groups[1].Value);
            }

            Regex raise_regex = new Regex(@"^raise\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match raise_match = raise_regex.Match(sub_effect);

            if(raise_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Raise");
                #endif

                return MakeRaiseEffect(raise_match.Groups[1].Value);
            }

            Regex sub_regex = new Regex(@"^sub\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match sub_match = sub_regex.Match(sub_effect);

            if(sub_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Sub");
                #endif

                return MakeSubEffect(sub_match.Groups[1].Value);
            }

            Regex mul_regex = new Regex(@"^mul\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match mul_match = mul_regex.Match(sub_effect);

            if(mul_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Mul");
                #endif

                return MakeMulEffect(mul_match.Groups[1].Value);
            }

            Regex div_regex = new Regex(@"^div\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match div_match = div_regex.Match(sub_effect);

            if(div_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Div");
                #endif

                return MakeDivEffect(div_match.Groups[1].Value);
            }

            Regex add_regex = new Regex(@"^add\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match add_match = add_regex.Match(sub_effect);

            if(add_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Add");
                #endif

                return MakeAddEffect(add_match.Groups[1].Value);
            }

            Regex special_regex = new Regex(@"^special\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match special_match = special_regex.Match(sub_effect);

            if(special_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Special");
                #endif

                return MakeSpecialEffect(special_match.Groups[1].Value);
            }

            Regex chance_regex = new Regex(@"^chance\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match chance_match = chance_regex.Match(sub_effect);

            if(chance_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Chance");
                #endif

               return CreateChance(chance_match.Groups[1].Value);
            }

            Regex saving_throw_regex = new Regex(@"^savingthrow\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match saving_throw_match = saving_throw_regex.Match(sub_effect);

            if(saving_throw_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Saving Throw");
                #endif

                return MakeSavingThrowEffect(saving_throw_match.Groups[1].Value);
            }

            Regex weather_regex = new Regex(@"^weather\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match weather_match = weather_regex.Match(sub_effect);

            if(weather_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Weather");
                #endif

                return MakeWeatherEffect(weather_match.Groups[1].Value);
            }

            Regex absorb_regex = new Regex(@"^absorb\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match absorb_match = absorb_regex.Match(sub_effect);

            if(absorb_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Absorb");
                #endif

                return MakeAbsorbEffect(absorb_match.Groups[1].Value);
            }

            Regex kill_regex = new Regex(@"^kill\{((?:[^{}]+|\{(?<DEPTH>)|\}(?<-DEPTH>))*(?(DEPTH)(?!)))\}");
            Match kill_match = kill_regex.Match(sub_effect);

            if(kill_match.Success){
                #if DEBUG_PARSESUBEFFECT
                Console.WriteLine("Weather");
                #endif

                return MakeKillEffect(kill_match.Groups[1].Value);
            }

            throw new ArgumentException("Effect of \"" + sub_effect + "\" not yet implemented");

        }

        public Target CreateTargetInformation(string information){
            //We already know the main denmoninator to parse this, so we just have to see what it is and there isn't much of an easier way
            bool ranged_bool = information.Contains("range");
            if(ranged_bool){
                Ranged ranged = new Ranged();
                if(information.Contains("all")){
                    ranged.targeting_mode = Ranged.Mode.All;
                } else 
                if(information.Contains("allies")){
                    ranged.targeting_mode = Ranged.Mode.Allies;
                } else {
                    ranged.targeting_mode = Ranged.Mode.Enemy;
                }

                string[] parts = information.Split(':');
                string input = parts[1].Replace("}","");
                
                ranged.range = int.Parse(input);

                return ranged;
            } else {
                Moves.Single single = new Moves.Single();
                if(information.Contains("user")){
                    single.targeting_mode = Moves.Single.Mode.User;
                } else 
                if(information.Contains("enemy")){
                    single.targeting_mode = Moves.Single.Mode.Enemy;
                } else {
                    single.targeting_mode = Moves.Single.Mode.Ally;
                }

                return single;
            }

        }

        public ApplyInformation MakeSavingThrowEffect(string effect_information){
            string[] throws = effect_information.Split('|');

            AttributeType[] types = new AttributeType[throws.Length];

            for(int i = 0; i < throws.Length; i++){
                types[i] = main_parser.ParseAttribute(throws[i]);
            }

            return new ApplyInformation{
                Apply = (character) =>{
                    return character.MakeSavingThrow(character.GetBestAttributeFromArray(types));
                },
                ReturnType = typeof(bool)
            };
        }

        public ApplyInformation CreateRemoveEffect(string information){
            //First let's see if it is a saving throw:
            Regex saving_throw_regex = new Regex(@"savingthrow\{([^{}]+)\}");
            Match saving_throw_match = saving_throw_regex.Match(information);

            if(saving_throw_match.Success){
                #if DEBUG_INTERP
                Console.WriteLine("Added Saving Throw: "+ saving_throw_match.Groups[1].Value);
                #endif

                return MakeSavingThrowEffect(saving_throw_match.Groups[1].Value);
            }

            return new ApplyInformation{
                Apply = (character) =>{
                    return true;
                },
                ReturnType = typeof(bool)
            };
        }

        private ApplyInformation CreateChance(string information){

            //For now I am just going to assume that the information is correct - it isn't going to be
            string parts = information.Replace("%", "");

            float percent = float.Parse(parts);

            #if DEBUG_INTERP
            Console.WriteLine("Added chance: " + parts);
            #endif

            return new ApplyInformation{
                Apply = (character) => {
                    Random ran = new Random();
                    int value = ran.Next(0, 101);
                    return value > 100 * percent;
                },
                ReturnType = typeof(bool)
            };
        }

        public Effect? ParseEffect(string effect_string){

            Effect return_effect = new Effect();

            //convert everything to lower just in case
            string main_parsing_string = effect_string.ToLower();

            Regex effects_regex = new Regex(@"(?<=apply\s)(.*?)(?=\sfor)");
            Match effects_match = effects_regex.Match(main_parsing_string);

            if(effects_match.Success){
                //Console.WriteLine(effects_match.Groups[1]);
            } else {
                //we're going to throw an exception here, but we will want to handle it\
                if(effect_string.Trim() != "none"){
                    throw new ArgumentException("Invalid format for interpretation: " + effect_string);
                } else {
                    return null;
                }
            }


            string base_effect_string = effects_match.Groups[1].Value;
            if(base_effect_string.Contains("if")){
                //Console.WriteLine(base_effect_string);
                //We're going to split our if statements
                string[] conditions = base_effect_string.Split("endif");

                foreach(string effect in conditions){
                    Regex condition_regex = new Regex(@"(.*)\s+if\s+(.*)");
                    Match condition_match = condition_regex.Match(effect);


                    if(condition_match.Success){
                        ApplyInformation expression_one = ParseSubEffect(condition_match.Groups[1].Value);

                        string condition_effect = condition_match.Groups[2].Value;

                        bool reverse = condition_effect.Contains("fail");
                        if(reverse){
                            condition_effect = condition_effect.Replace("fail ", "");
                        }

                        ApplyInformation expression_two = ParseSubEffect(condition_effect);

                        if(expression_two.ReturnType != typeof(bool)){
                            throw new ArgumentException("Conditional type mismatch: Secondary effect does not have bool type. String: " + condition_effect);
                        }

                        ApplyInformation main_effect = new ApplyInformation{
                            Apply = (character) => {
                                if(reverse){
                                    if(!(bool)expression_two.Apply(character)){
                                        expression_one.Apply(character);
                                    } else {
                                        character.SetFailedExpression(expression_one);
                                    }
                                } else {
                                    if((bool)expression_two.Apply(character)){
                                        expression_one.Apply(character);
                                    } else {
                                        character.SetFailedExpression(expression_one);
                                    }
                                }
                                
                                return new UnitType();
                            },
                            ReturnType = typeof(UnitType)
                        };

                        return_effect.AddEffect(main_effect);
                    }
                }

            } else {
                //We can be this niave if there is not a condition for the effect
                string[] effects = base_effect_string.Split(' ');


                foreach(string effect in effects){
                    return_effect.AddEffect(ParseSubEffect(effect));
                }
            }

            
            //Now that we have our main effects, we can grab the target for that effect

            Regex target_regex = new Regex(@"target in\s+(\S+\{[^{}]+\})");
            Match target_match = target_regex.Match(main_parsing_string);

            return_effect.SetTargetInformation(CreateTargetInformation(target_match.Groups[1].Value.ToLower()));

            #if DEBUG_INTERP
            return_effect.WriteTargetInformation();
            #endif

            //The remove regex removes an effect/status after a given condition
            Regex remove_regex = new Regex(@"(?<=remove\s+).+$");
            Match remove_match = remove_regex.Match(main_parsing_string);
            if(remove_match.Success){
                return_effect.SetRemoveEffect(CreateRemoveEffect(remove_match.Groups[0].Value));
            }


            //Console.WriteLine("Finished Parsing: " + effects_match.Success);

            return return_effect;

        }

    }

}