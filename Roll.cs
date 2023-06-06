using System;
using Characters;

public class Roll{

    public AttributeType[] dependent_rolls;
    public float[] weights;

    public int[] dice;

    public Roll(float[] weights, int[] dice, AttributeType[] dependent_rolls){
        this.weights = weights;
        this.dice = dice;
        this.dependent_rolls = dependent_rolls;
    }

    //We assume we are rolling a d20
    public static int RollDie(){
        return RollDie(20);
    }

    public static int RollDie(int die){
        Random ran = new Random();
        return ran.Next(1, die+1);
    }

    public float MakeRoll(Character c){
        float sum = 0.0f;

        for(int i = 0; i < weights.Length; i++){
            sum += weights[i] + c.attributes.GetValue(dependent_rolls[i]);
        }


        foreach(int die in dice){
            sum += RollDie(die);
        }

        return sum;
    }

    public override string ToString()
    {
        string s = "\nNumber of Dice: " + dice.Length + ". Dice Size: " + dice[0] + ".";
        
        int i = 0;
        foreach(AttributeType t in dependent_rolls){
            s += "\nRoll:\nDamage Addition: " + t.ToString() + ". Weight: " + weights[i];
            i++;
        }

        return s;
    }

}