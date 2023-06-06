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

public class CombatCommands : ApplicationCommandModule {

    
    [SlashCommand("usemove", "Uses a move against a target in a scenario.")]
    public async Task UseMove(InteractionContext ctx,
    [Option("move", "The name of the move")] string move,
    [Option("target", "The name of the target in the scenario")] string target){
        Character target_character = Game.GetCharacter(target);
        Character using_character = Game.GetCharacter(ctx.User.Id);
        DiscordEmbed embed = using_character.UseMove(move, target_character);

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed));
    }

      
}