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

public class MoveCommands : ApplicationCommandModule {

    [SlashCommand("moveinfo", "Shows information about a given move.")]
    public async Task MoveInfo(InteractionContext ctx,
    [Option("name", "the move name")] string name){

        name = name.Replace("_", " ");

        try{
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(Game.FindMove(name).BuildEmbed()));
        }catch(Exception e){
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Error: Given move does not exist or is not loaded!\nError information: " + e.Message));
        }

    }

    [SlashCommand("movelist", "Displays the list of moves starting at <page>")]
    public async Task MoveList(InteractionContext ctx,
    [Option("page", "the page to start listing moves")] long page){

        page = page - 1;
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(Game.GetMoveList((int)page)));

    }

}