using System;
using Characters;
#pragma warning disable
public static class Game{

    public static List<Character> active_characters;

    public static Weather current_weather;

    public static void AddCharacter(Character c){
        active_characters.Add(c);
    }

    public static int Roll(Character c, AttributeType type){
        int bonus = c.attributes.GetRollBonus(type);

        Random ran = new Random();

        int random_number = ran.Next(1, 21);

        return bonus + random_number;
    }

    public static int Roll(int max){
        Random ran = new Random();
        return ran.Next(1, max+1);
    }


}