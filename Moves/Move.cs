using System;
using Types;
using Characters;

#pragma warning disable

namespace Moves{

    public class Move{

        public enum MoveType{
            Special,
            Physical,
            Status,
            Weather
        }

        public Typing type;

        public string name;
        public MoveType ability_type;

        public Range range;

        public int accuracy;
        
        public Roll? damage;

        public Effect? move_effect;

        public string description;

        public static int num = 0;
        public int id;

        public Move(){
            
        }

        public Move(string name, MoveType type, int accuracy, Range range, Roll? damage_roll, Typing move_type, string description, Effect? move_effect){
            this.name = name;
            this.ability_type = type;
            this.accuracy = accuracy;
            this.range = range;
            this.damage = damage_roll;
            this.type = move_type;
            this.description = description;
            this.move_effect = move_effect;
            this.id = num;
            num++;
        }

        public float Roll(Character c){
            return damage.MakeRoll(c);
        }

        public override string ToString(){
            //We're going to cut this up:

            string name = this.name;

            string ability_string = ability_type.ToString();

            string accuracy = (this.accuracy > 0) ? "+"+this.accuracy.ToString() : this.accuracy.ToString();

            string range = this.range.ToString();

            string damage_string = "";
            if(damage == null){
                damage_string += "None";
            } else {
                damage_string += damage.ToString();
            }

            string type = this.type.ToString();

            string description = this.description;

            string effect_information = "";
            if(move_effect == null){
                effect_information = "No effect information";
            } else {
                effect_information = move_effect.ToString();
            }

            string line_break = "\n__________________________________________________________________________________\n";
            string new_line = "\n";

            //Now we format:
            string s = "Name: " + name + " | Internal ID: " + id.ToString();
            s += line_break;
            s += "Type: " + type + new_line;
            s += "Damage Type: " + ability_string + new_line + new_line;
            s += "Damage Information: " + new_line + damage_string + new_line + new_line;
            s += "Range: " + range +new_line + new_line;
            s += "Description: " + new_line + description + new_line;
            s += line_break;
            s += "Effect Information: " + new_line + effect_information;

            return s;
        }

        public string GetInfo(){
            string s = "Name: " + name;
            s += "\n\n";
            s += "Type: " + type.ToString();
            s += "\n\n";
            s += damage.ToString();

            return s;
        }

    }


}