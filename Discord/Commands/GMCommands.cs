using System;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.EventArgs;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Characters;

public class GMCommands : ApplicationCommandModule {

    
    [SlashCommand("createscene", "Creates a scene.")]
    public async Task CreateScene(InteractionContext ctx,
    [Option("characters", "Seperate characters by a comma")] string characters){
        if(!Game.IsGm(ctx.User.Id)){
            Errors.NotGmError(ctx);
            return;
        } else {
            Debug.WriteToDebugFile("Hi Noca!");
        }

        try{
            List<Character> main_characters = Game.GrabCharacters(characters);
            Scenario scenario = new Scenario();
            foreach(Character c in main_characters){
                scenario.AddCharacter(c);
            }

            Game.current_scenario = scenario;

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(scenario.DisplayScenario()));
        }catch(ArgumentException e){
            Errors.DisplayException(ctx, e);
        }
    }

        [SlashCommand("setteam", "Sets the team of <characters> to <team>")]
        public async Task SetTeam(InteractionContext ctx,
        [Option("characters", "Seperate characters by a comma")] string characters,
        [Option("team", "Put the teamname here")] string team){
            if(!Game.IsGm(ctx.User.Id)){
                Errors.NotGmError(ctx);
                return;
            }

            try{
                List<Character> main_characters = Game.GrabCharacters(characters);
                if(Game.current_scenario == null){
                    throw new ArgumentException("Scenario not created");
                }
                Scenario scenario = Game.current_scenario;
                foreach(Character c in main_characters){
                    scenario.SetTeam(c, team);
                }

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(scenario.DisplayScenario()));
            }catch(ArgumentException e){
                Errors.DisplayException(ctx, e);
            }

        }

}