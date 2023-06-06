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

    [SlashCommand("rollskill", "Rolls the specific die that relates to the skill on your character.")]
    public async Task RollSkill(InteractionContext ctx,
    [Option("Skill", "The skill to roll for")] Character.Skill type){
        DiscordEmbedBuilder embed = new DiscordEmbedBuilder{
            Title = "Roll for " + type.ToString()
        };
        string roll_information = "";

        Character c = Game.GetCharacter(ctx.User.Id);

        int bonus = c.RollSkill(type);
        int roll = Roll.RollDie();
        roll_information = roll.ToString() + " " + ((bonus > 0) ? "+ (" + bonus.ToString() : "- (" + Math.Abs(bonus).ToString()) + ") = " + (roll + bonus).ToString();
        embed.Description = roll_information;
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed.Build()));
    }

    [SlashCommand("rollattribute", "Rolls the specific die that relates to the attribute on your character")]
    public async Task RollAttribute(InteractionContext ctx,
    [Option("Attribute", "The attribute to roll for")] AttributeType type){
            Debug.WriteToDebugFile("Rolling");


        DiscordEmbedBuilder embed = new DiscordEmbedBuilder{
            Title = "Roll for " + type.ToString()
        };
        string roll_information = "";

        Character c = Game.GetCharacter(ctx.User.Id);

        int bonus = c.attributes.GetRollBonus(type);
        int roll = Roll.RollDie();
        roll_information = roll.ToString() + " " + ((bonus > 0) ? "+ (" + bonus.ToString() : "- (" + Math.Abs(bonus).ToString()) + ") = " + (roll + bonus).ToString();
        embed.Description = roll_information;
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(embed.Build()));
    }



    

}