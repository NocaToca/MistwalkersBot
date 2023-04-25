// See https://aka.ms/new-console-template for more information
using System;
using Moves;
using Parsing;

internal class Program{

    //The main function that starts the bot and instantiates all the Creature class needs in order to run
    static void Main(string[] args){
        TestMove();
    }

    static void TestMove(){
        Parser parser = new Parser();
        string effect_string = "The user bites the target with fiery fangs, which may leave the target burned. The target must make a Dexterity saving throw or take ongoing fire damage until the end of their next turn.";
        Move move = parser.InterpretMove("Fire Fange", "Lesser Physical", 0, 1, "2d6 + 0.5 STR fire damage", effect_string);
        parser.ParseEffect(effect_string);
    }

    static void RunBot(){
        // //The bot declaration
        Bot bot = new Bot();
        
        // //Runs the actual bot
        bot.RunBotAsync().GetAwaiter().GetResult();
    }

}