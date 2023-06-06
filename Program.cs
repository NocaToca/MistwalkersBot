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
        InitProgram();
        RunBot();
    }

    static void InitProgram(){
        CharacterParser cp = new CharacterParser();

        Game.Initialize();
        cp.ReadCharacters(@"D:\Discord\MistwalkersBot\Player Characters Sheet.xlsx", 4);
    }

    static void TestMove(){
        Parser parser = new Parser();
        string effect_string = "The user bites the target with fiery fangs, which may leave the target burned. The target must make a Dexterity saving throw or take ongoing fire damage until the end of their next turn.";
        Move move = parser.InterpretMove("Fire Fange", "Lesser Physical", 0, 1, "2d6 + 0.5 STR fire damage", effect_string);
        parser.ParseEffect(effect_string);
    }

    static void TestReadingCharacterFile(){
        CharacterParser cp = new CharacterParser();

        Game.Initialize();

        //Character c = cp.ReadCharacter(@"D:\Discord\MistwalkersBot\Player Characters Sheet.xlsx");

        //Console.WriteLine(c.moves[0].ToString());
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
        // c.AddMove(swallow);
        // c.AddMove(stockpile);
        // c.UseMove("stockpile", null);
        // c.UseMove("stockpile", null);
        // c.PrintSpecial("stockpile");
        // c.UseMove("swallow", null);
        // c.PrintSpecial("stockpile");
    }

    

    static void RunBot(){
        // //The bot declaration
        Bot bot = new Bot();
        
        // //Runs the actual bot
        bot.RunBotAsync().GetAwaiter().GetResult();
    }

}