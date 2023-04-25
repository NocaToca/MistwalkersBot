using System;
using Characters;

namespace Types{

    public abstract class Typing{

        public enum Order{Primary, Secondary};

        public AttributeType[] primary_bonuses;
        public AttributeType[] secondary_bonuses;

        public static Normal Normal {get{return new Normal();}}
        public static Fire Fire {get{return new Fire();}}
        public static Water Water {get{return new Water();}}
        public static Grass Grass {get{return new Grass();}}
        public static Electric Electric {get{return new Electric();}}
        public static Ice Ice {get{return new Ice();}}
        public static Fighting Fighting {get{return new Fighting();}}
        public static Poison Poison {get{return new Poison();}}
        public static Ground Ground {get{return new Ground();}}
        public static Flying Flying {get{return new Flying();}}
        public static Psychic Psychic {get{return new Psychic();}}
        public static Bug Bug {get{return new Bug();}}
        public static Rock Rock {get{return new Rock();}}
        public static Ghost Ghost {get{return new Ghost();}}
        public static Dark Dark {get{return new Dark();}}
        public static Dragon Dragon {get{return new Dragon();}}
        public static Steel Steel {get{return new Steel();}}
        public static Fairy Fairy {get{return new Fairy();}}


        public Typing(){

        }

        public Typing(int len_primary, int len_secondary){

            primary_bonuses = new AttributeType[len_primary];
            secondary_bonuses = new AttributeType[len_secondary]; 

        }

        public virtual Func<AttributeType, int> GetBonus(){
            return delegate(AttributeType type){
                return 0;
            };
        }

        public override string ToString(){
            return "N/A";
        }

    }

    public partial class Normal : Typing{
        public override string ToString(){
            return "Normal";
        }
    }
    public partial class Water : Typing{
        public override string ToString(){
            return "Water";
        }
    }
    public partial class Fire : Typing{
        public override string ToString(){
            return "Fire";
        }
    }
    public partial class Grass : Typing{
        public override string ToString(){
            return "Grass";
        }
    }
    public partial class Electric : Typing{
        public override string ToString(){
            return "Electric";
        }
    }
    public partial class Ice : Typing{
        public override string ToString(){
            return "Ice";
        }
    }
    public partial class Fighting : Typing{
        public override string ToString(){
            return "Fighting";
        }
    }
    public partial class Poison : Typing{
        public override string ToString(){
            return "Poison";
        }
    }
    public partial class Ground : Typing{
        public override string ToString(){
            return "Ground";
        }
    }
    public partial class Flying : Typing{
        public override string ToString(){
            return "Flying";
        }
    }
    public partial class Psychic : Typing{
        public override string ToString(){
            return "Psychic";
        }
    }
    public partial class Bug : Typing{
        public override string ToString(){
            return "Bug";
        }
    }
    public partial class Rock : Typing{
        public override string ToString(){
            return "Rock";
        }
    }
    public partial class Ghost : Typing{
        public override string ToString(){
            return "Ghost";
        }
    }
    public partial class Dark : Typing{
        public override string ToString(){
            return "Dark";
        }
    }
    public partial class Dragon : Typing{
        public override string ToString(){
            return "Dragon";
        }
    }
    public partial class Steel : Typing{
        public override string ToString(){
            return "Steel";
        }
    }
    public partial class Fairy : Typing{
        public override string ToString(){
            return "Fairy";
        }
    }
    
}
