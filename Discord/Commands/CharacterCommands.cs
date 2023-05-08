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

public class CharacterCommands : ApplicationCommandModule {

    [SlashCommand("character", "Shows information about a given character.")]
    public async Task CharacterInfo(InteractionContext ctx,
    [Option("name", "the character's name")] string name){

        try{
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(Game.GetCharacter(name).BuildEmbed()));
        }catch(Exception e){
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Error: Character does not exist or is not loaded!"));
        }


        
    }

}