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