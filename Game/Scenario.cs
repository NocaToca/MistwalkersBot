using Characters;
using DSharpPlus.Entities;

public class Scenario{

    public Dictionary<string, Team> teams;
    public List<Character> characters_in_scene;

    public Scenario(){
        teams = new Dictionary<string, Team>();
        characters_in_scene = new List<Character>();
    }

    public void AddCharacter(Character c){
        characters_in_scene.Add(c);
    }

    public void SetTeam(Character c, string team_name){
        if(characters_in_scene.Contains(c)){
            try{
                teams[team_name].AddToTeam(c);
            }catch(Exception e){
                AddTeam(team_name);
                teams[team_name].AddToTeam(c);
            }
        } else {
            throw new ArgumentException("Character not in scene");
        }
    }

    public void AddTeam(string team_name){
        teams.Add(team_name, new Team());
    }

    public void AddToTeam(Character c, string team_name){
        try{
            teams[team_name].AddToTeam(c);
        }catch(Exception e){
            AddTeam(team_name);
            teams[team_name].AddToTeam(c);
        }
        characters_in_scene.Add(c);
    }

    public bool IsCharacterInScene(Character c){
        return characters_in_scene.Contains(c);
    }
    
    public DiscordEmbed DisplayScenario(){
        List<Character> characters_displayed = new List<Character>();

        DiscordEmbedBuilder embed = new DiscordEmbedBuilder(){
            Title = "Current Scenario"
        };

        foreach(KeyValuePair<string, Team> team in teams){
            embed.AddField(team.Key, team.Value.ListCharacters());
            characters_displayed.AddRange(team.Value.characters);
        }

        string unlisted_characters = "";
        foreach(Character c in characters_in_scene){
            if(!characters_displayed.Contains(c)){
                unlisted_characters += c.name + ", ";
            }
        }

        try{
            unlisted_characters = unlisted_characters.Trim().Substring(0, unlisted_characters.Length-2);

        }catch (Exception e){
            unlisted_characters = "None";
        }
        embed.AddField("Characters not in a Team: ", unlisted_characters);

        return embed.Build();
    }

}

public class Team{

    public List<Character> characters;

    public Team(){
        characters = new List<Character>();
    }

    public void AddToTeam(Character c){
        characters.Add(c);
    }

    public bool InTeam(Character c){
        return characters.Contains(c);
    }

    public string ListCharacters(){
        string s = "";
        for(int i = 0; i <characters.Count; i++){
            s += characters[i].name;
            if(i != characters.Count - 1){
                s += ", ";
            }
        }

        return s;
    }
}