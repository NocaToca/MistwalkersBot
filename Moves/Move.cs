using System;
using Types;

namespace Moves{

    public class Move{

        public Typing type;

        public string name;
        public string ability_type;

        public int accuracy;
        
        public Roll damage;

        public Move(){
            
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