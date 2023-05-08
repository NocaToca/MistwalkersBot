
using DSharpPlus.Entities;

namespace Characters{

    public partial class Character{
/***************************************************************DISPLAY**********************************************************************************/

        public override string ToString(){
            return moves[0].ToString();
        }

        public string GrabMoveString(){
            string s = "";
            for(int i = 0; i < moves.Count; i++){
                s += moves[i].name;
                if(i != moves.Count - 1){
                    s += " | ";
                }
            }
            return s;
        }

        public string GrabInventoryString(){
            string s = "";
            for(int i = 0; i < items.Count; i++){
                s += items[i].name;
                if(i != items.Count - 1){
                    s += " | ";
                }
            }
            return s;
        }

        public string GrabAbilityString(){
            string s = "";

            for(int i = 0; i < abilities.Count; i++){
                s += abilities[i].name;
                if(i != abilities.Count - 1){
                    s += " | ";
                }
            }
           
            return s;
        }

        //temp
        public string GrabThumbnail(){
            switch(name.ToLower()){
                case "kindle":
                    return "https://cdn.discordapp.com/attachments/1105005807596224562/1105005835106652191/thumb.png";
                case "narcis":
                    return "https://cdn.discordapp.com/attachments/1105005807596224562/1105016516287676437/thumb.png";
                case "ryoji":
                    return "https://cdn.discordapp.com/attachments/1105005807596224562/1105016636618063882/thumb.png";
                case "volo":
                    return "https://cdn.discordapp.com/attachments/1105005807596224562/1105016761847402566/thumb.png";
                default:
                    throw new ArgumentException();
            }
        }

        public unsafe DiscordEmbed BuildEmbed(){
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder{
                Title = name,
                Description = "Pokemon: " + pokemon_name + " | Type one: " + primary_type.ToString() + " | Type two: " + ((secondary_type == null) ? "None" :  secondary_type.ToString())
            };

            embed.AddField("Current Status: ", "Health Points: " + (*main_points.current_health_points).ToString() + "/" + main_points.max_health_points.ToString()+
                "   \nBelly Points: " + (*main_points.current_belly_points).ToString() + "/" + main_points.max_belly_points.ToString()+
                "   \nPower Points: " + (*main_points.current_power_points).ToString() + "/" + main_points.max_power_points.ToString());

            string attribute_values = "Strength: " + attributes.GetValue(AttributeType.Strength).ToString() + "(" + ((attributes.GetRollBonus(AttributeType.Strength) > 0) ? "+" 
            + (attributes.GetRollBonus(AttributeType.Strength)) : (attributes.GetRollBonus(AttributeType.Strength))) + ")" + "   " + 
            "Dexterity: " + attributes.GetValue(AttributeType.Dexterity).ToString() + "(" + ((attributes.GetRollBonus(AttributeType.Dexterity) > 0) ? "+" 
            + (attributes.GetRollBonus(AttributeType.Dexterity)) : (attributes.GetRollBonus(AttributeType.Dexterity))) + ")" + "   " + 
            "Constitution: " + attributes.GetValue(AttributeType.Constitution).ToString() + "(" + ((attributes.GetRollBonus(AttributeType.Constitution) > 0) ? "+" 
            + (attributes.GetRollBonus(AttributeType.Constitution)) : (attributes.GetRollBonus(AttributeType.Constitution))) + ")" + "   \n" + 
            "Intelligence: " + attributes.GetValue(AttributeType.Intelligence).ToString() + "(" + ((attributes.GetRollBonus(AttributeType.Intelligence) > 0) ? "+" 
            + (attributes.GetRollBonus(AttributeType.Intelligence)) : (attributes.GetRollBonus(AttributeType.Intelligence))) + ")" + "   " + 
            "Wisdom: " + attributes.GetValue(AttributeType.Wisdom).ToString() + "(" + ((attributes.GetRollBonus(AttributeType.Wisdom) > 0) ? "+" 
            + (attributes.GetRollBonus(AttributeType.Wisdom)) : (attributes.GetRollBonus(AttributeType.Wisdom))) + ")" + "   " + 
            "Charisma: " + attributes.GetValue(AttributeType.Charisma).ToString() + "(" + ((attributes.GetRollBonus(AttributeType.Charisma) > 0) ? "+" 
            + (attributes.GetRollBonus(AttributeType.Charisma)) : (attributes.GetRollBonus(AttributeType.Charisma))) + ")";

            embed.AddField("Leveling: ", "Level: " + level.ToString() + " | Experience: " + experience.ToString());
            
            embed.AddField("Attribute Values: ", attribute_values);

            embed.AddField("Abilities: ", GrabAbilityString());
            embed.AddField("Moves: ", GrabMoveString());
            embed.AddField("Inventory: ", GrabInventoryString());

            embed.WithThumbnail(GrabThumbnail());

            return embed.Build();
        }


        public Embed ShowAttributes(){
            return null;
        }

        public Embed ShowMoves(){
            return null;
        }

        public Embed ShowAbilities(){
            return null;
        }

        public Embed ShowInventory(){
            return null;
        }
/*******************************************************************************************************************************************************/

    }

}