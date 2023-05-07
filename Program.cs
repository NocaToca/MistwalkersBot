// See https://aka.ms/new-console-template for more information
using System;
using Moves;
using Parsing;
using Interpreter;
using FileHandling;
using Characters;

#pragma warning disable

internal class Program{

    //The main function that starts the bot and instantiates all the Creature class needs in order to run
    static void Main(string[] args){
        TestMoveInterp();
    }

    static void TestMove(){
        Parser parser = new Parser();
        string effect_string = "The user bites the target with fiery fangs, which may leave the target burned. The target must make a Dexterity saving throw or take ongoing fire damage until the end of their next turn.";
        Move move = parser.InterpretMove("Fire Fange", "Lesser Physical", 0, 1, "2d6 + 0.5 STR fire damage", effect_string);
        parser.ParseEffect(effect_string);
    }

    static void TestSwallow(){
        MoveInterpreter mi = new MoveInterpreter();

        string[] move_cell = new string[]{
            "Swallow",
            "Lesser Special",
            "-2",
            "4 Tiles",
            "1d8 + 0.5 INT ground damage",
            "Effect Description",
            "apply restore{hp:add{25:mul{25:special{remove:stockpile:all}}}} if special{exists:stockpile:>0} for target in single{User}"
        };

        string[] move_cell2 = new string[]{
            "Stockpile",
            "Lesser Special",
            "-2",
            "4 Tiles",
            "1d8 + 0.5 INT ground damage",
            "Effect Description",
            "apply special{add:stockpile:1} for target in single{User}"
        };

        Move swallow = mi.InterpretMove(move_cell);
        Move stockpile = mi.InterpretMove(move_cell2);

        Character c = new Character(true, "test");
        c.AddMove(swallow);
        c.AddMove(stockpile);
        c.UseMove("stockpile", null);
        c.UseMove("stockpile", null);
        c.PrintSpecial("stockpile");
        c.UseMove("swallow", null);
        c.PrintSpecial("stockpile");
    }

    static void TestMoveInterp(){
        MoveInterpreter mi = new MoveInterpreter();
        //We are going to test building our moves and see what we have so far
        //To do this I am going to make a file reader and writer class
        //But first, let's test it on a single one

        // string[] move_cell = new string[]{
        //     "Sand Tomb",
        //     "Lesser Special",
        //     "-2",
        //     "4 Tiles",
        //     "1d8 + 0.5 INT ground damage",
        //     "Effect Description",
        //     "apply status{DoT:2.5%} for target in single{Enemy} remove savingthrow{Dex|Str}"
        // };

        // Move move = mi.InterpretMove(move_cell);
        // Console.WriteLine(move.ToString());

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
                    fw.WriteLine(move.ToString());
                    fw.WriteLine("");
                    fw.WriteLine("");
                    fw.WriteLine("");
                }catch(Exception e){
                    fw.WriteLine("Failed writing move: " + moves_string_information[0]);
                    fw.WriteLine("Reason: "+ e.Message);
                    string readable_string = move_string.Replace(",-=-,", " | ");
                    fw.WriteLine("String: " + readable_string);
                    fw.WriteLine("");
                    failures++;
                    Move.num++;
                }
            }
        }

        Console.WriteLine("Failures: " + failures);
    }

    static void RunBot(){
        // //The bot declaration
        Bot bot = new Bot();
        
        // //Runs the actual bot
        bot.RunBotAsync().GetAwaiter().GetResult();
    }

}