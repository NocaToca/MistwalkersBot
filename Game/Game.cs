using System;
using Moves;
using Parsing;
using Interpreter;
using FileHandling;
using Characters;
using Items;
using Abilities;
using DSharpPlus.Entities;

#pragma warning disable
public static class Game{

    public static Dictionary<string, Character> active_characters = new Dictionary<string, Character>();

    public static Dictionary<string, Move> moves = new Dictionary<string, Move>();

    public static Weather current_weather;

    public static Scenario? current_scenario;

    static bool initialized = false;

    public static void AddCharacter(Character c){
        active_characters.Add(c.name.ToLower(), c);
    }

    public static bool InScenario(){
        return current_scenario == null;
    }

    public static void VoidScenario(){
        current_scenario = null;
    }

    public static void SetNewScenario(Scenario scenario){
        current_scenario = scenario;
    }

    public static Character GetCharacter(ulong discord_id){
        if(discord_id == 215975377770774528){
            return GetCharacter("Kindle");
        }
        if(discord_id == 178505518938193920){
            return GetCharacter("Kindle");
        }
        if(discord_id == 148414279182319616){
            return GetCharacter("Narcis");
        }
        if(discord_id == 291199236781899776){
            return GetCharacter("Ryoji");
        }
        if(discord_id == 396480177770594315){
            return GetCharacter("Volo");
        }

        throw new ArgumentException("Unknown discord user");
    }

    public static Character GetCharacter(string name){
        return active_characters[name.ToLower()];
    }

    public static void Initialize(){
        if(initialized){
            throw new Exception();
        }

        ReadMoves();
    }

    public static DiscordEmbed GetMoveList(int page){
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder{
            Title = "Move List | Page " + page.ToString() + " of " + ((moves.Count/25) + 1).ToString()
        };

        string desc = "";
        for(int i = page * 25; i < moves.Count && i < (page*25)+25; i++){
            desc += moves.ElementAt(i).Value.name + "\n";
        }
        embed.Description = desc;
        return embed.Build();
    }

    static void ReadMoves(){
        MoveInterpreter mi = new MoveInterpreter();
        FileReader fr = new FileReader();
        string filepath = @"D:\Discord\MistwalkersBot\Move list.xlsx";
        string main_information = fr.ReadFile(filepath);

        string[] rows = main_information.Split('\n');
        string[] moves = new string[rows.Length-1];
        for(int i = 0; i < moves.Length; i++){
            moves[i] = rows[i+1];
        }

        int failures = 0;
        using(FileWriter fw = new FileWriter(@"D:\Discord\MistwalkersBot\MoveInformation.txt")){

            foreach(string move_string in moves){
                string[] moves_string_information = move_string.Split(",-=-,");

                try{
                    Move move = mi.InterpretMove(moves_string_information);
                    Game.moves.Add(move.name.ToLower(), move);
                }catch(Exception e){
                    failures++;
                    Move.num++;
                }
            }
        }

        Console.WriteLine("Failures: " + failures);
    }

    public static Item GetItem(string name){
        return TempGetItem(name);
    } 

    public static Ability GetAbility(string name){
        return TempGetAbility(name);
    }

    private static Ability TempGetAbility(string name){
        return new Ability(Ability.Type.Individual){
            name = name
        };
    }

    public static void AddMoveToEnvironment(Move move){
        moves.Add(move.name.ToLower(), move);
    }

    public static void PrintMoves(){
        foreach(KeyValuePair<string, Move> pair in moves){
            Console.WriteLine(pair.Key);
        }
    }

    public static Move FindMove(string move_name){
        try{
            return moves[move_name.ToLower()];
        }catch(Exception e){
            throw new ArgumentException("Move " + move_name + " does not exist in move list");
        }
    }

    private static Item TempGetItem(string name){
        return new Item{
            name = name
        };
    }

    public static int Roll(Character c, AttributeType type){
        int bonus = c.attributes.GetRollBonus(type);

        Random ran = new Random();

        int random_number = ran.Next(1, 21);

        return bonus + random_number;
    }

    public static int Roll(int max){
        Random ran = new Random();
        return ran.Next(1, max+1);
    }

    public static ulong gm_id = 215975377770774528;

    public static bool IsGm(ulong id){return id == gm_id;}

    public static List<Character> GrabCharacters(string characters){
        string[] split_string = characters.Split(",");
        List<Character> _characters = new List<Character>();

        foreach(string characater in split_string){
            try{
                Character c = Game.GetCharacter(characater.ToLower().Trim());
                _characters.Add(c);
                Debug.WriteToDebugFile(c.name);
            }catch(Exception e){
                throw new ArgumentException("Could not find character: " + characater);
            }
        }

        return _characters;
    }



}