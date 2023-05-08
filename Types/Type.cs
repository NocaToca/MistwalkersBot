using System;
using Characters;

#pragma warning disable

namespace Types{

    public struct AttributeBonuses{
        public List<AttributeType> type;
        public List<int> bonus;

        public AttributeBonuses(List<AttributeType> type, List<int> bonus){
            this.type = type;
            this.bonus = bonus;
        }
    }

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

        public static float RESIST = 0.5f;
        public static float IMMUNE = 0.0f;
        public static float WEAK = 2.0f;
        public static float BASE = 1.0f;


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

        //Using a move, applies a bonus AGAINST the type based off of the chart table
        //I.E the move_type is the type of the move and the ApplyBonus is the pokemon, theoritically
        public abstract float ApplyBonus(Typing move_type);

        public abstract AttributeBonuses PureBonuses();
        public abstract AttributeBonuses PrimaryBonuses();
        public abstract AttributeBonuses SecondaryBonuses();

        public override string ToString(){
            return "N/A";
        }

        public override bool Equals(object? obj){
            if(obj == null) return false;
            return this.GetType() == obj.GetType();
        }

    }

    public partial class Normal : Typing{

        public override float ApplyBonus(Typing move_type){
            if(move_type is Fighting){
                return WEAK;
            }

            if(move_type is Ghost){
                return IMMUNE;
            }

            return BASE;
        }

        public override string ToString(){
            return "Normal";
        }
    }
    public partial class Water : Typing{

        public override float ApplyBonus(Typing move_type){
            if(move_type is Fire){
                return RESIST;
            }

            if(move_type is Water){
                return RESIST;
            }

            if(move_type is Grass){
                return WEAK;
            }

            if(move_type is Electric){
                return WEAK;
            }

            if(move_type is Ice){
                return RESIST;
            }

            if(move_type is Steel){
                return RESIST;
            }

            return BASE;
        }

        public override string ToString(){
            return "Water";
        }
    }
    public partial class Fire : Typing{

        public override float ApplyBonus(Typing move_type){
            if(move_type is Fire){
                return RESIST;
            }

            if(move_type is Water){
                return WEAK;
            }

            if(move_type is Grass){
                return RESIST;
            }

            if(move_type is Ice){
                return RESIST;
            }

            if(move_type is Ground){
               return WEAK;
            }

            if(move_type is Bug){
                return RESIST;
            }

            if(move_type is Rock){
                return WEAK;
            }

            if(move_type is Steel){
                return RESIST;
            }

            if(move_type is Fairy){
                return RESIST;
            }

            return BASE;

        }

        public override string ToString(){
            return "Fire";
        }
    }
    public partial class Grass : Typing{
        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Fire){
                return WEAK;
            }

            if(move_type is Water){
                return RESIST;
            }

            if(move_type is Grass){
                return RESIST;
            }

            if(move_type is Electric){
                return RESIST;
            }

            if(move_type is Ice){
                return WEAK;
            }

            if(move_type is Poison){
                return WEAK;
            }

            if(move_type is Ground){
                return RESIST;
            }

            if(move_type is Flying){
                return WEAK;
            }

            if(move_type is Bug){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Grass";
        }
    }
    public partial class Electric : Typing{
        public override float ApplyBonus(Typing move_type){
            if(move_type is Electric){
                return RESIST;
            }

            if(move_type is Ground){
                return WEAK;
            }

            if(move_type is Flying){
                return RESIST;
            }

            if(move_type is Steel){
                return RESIST;
            }

            return BASE;
        }

        public override string ToString(){
            return "Electric";
        }
    }
    public partial class Ice : Typing{

        public override float ApplyBonus(Typing move_type){
            if(move_type is Fire){
                return WEAK;
            }

            if(move_type is Ice){
                return RESIST;
            }

            if(move_type is Fighting){
                return WEAK;
            }

            if(move_type is Rock){
                return WEAK;
            }

            if(move_type is Steel){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Ice";
        }
    }
    public partial class Fighting : Typing{

        public override float ApplyBonus(Typing move_type){
            if(move_type is Flying){
                return WEAK;
            }

            if(move_type is Psychic){
                return WEAK;
            }

            if(move_type is Bug){
                return RESIST;
            }

            if(move_type is Rock){
                return RESIST;
            }

            if(move_type is Dark){
                return RESIST;
            }

            if(move_type is Fairy){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Fighting";
        }
    }
    public partial class Poison : Typing{
        public override float ApplyBonus(Typing move_type){

            if(move_type is Grass){
                return RESIST;
            }

            if(move_type is Fighting){
                return RESIST;
            }

            if(move_type is Poison){
                return RESIST;
            }

            if(move_type is Ground){
                return WEAK;
            }

            if(move_type is Psychic){
                return WEAK;
            }

            if(move_type is Bug){
                return RESIST;
            }

            if(move_type is Fairy){
                return RESIST;
            }

            return BASE;
        }

        public override string ToString(){
            return "Poison";
        }
    }
    public partial class Ground : Typing{
        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Water){
                return WEAK;
            }

            if(move_type is Grass){
                return WEAK;
            }

            if(move_type is Electric){
                return IMMUNE;
            }

            if(move_type is Ice){
                return WEAK;
            }

            if(move_type is Poison){
                return RESIST;
            }

            if(move_type is Rock){
                return RESIST;
            }

            return BASE;
        }

        public override string ToString(){
            return "Ground";
        }
    }
    public partial class Flying : Typing{

        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Grass){
                return RESIST;
            }

            if(move_type is Electric){
                return WEAK;
            }

            if(move_type is Ice){
                return WEAK;
            }

            if(move_type is Fighting){
                return RESIST;
            }

            if(move_type is Ground){
                return IMMUNE;
            }

            if(move_type is Bug){
                return RESIST;
            }

            if(move_type is Rock){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Flying";
        }
    }
    public partial class Psychic : Typing{
        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Fighting){
                return RESIST;
            }

            if(move_type is Psychic){
                return RESIST;
            }

            if(move_type is Bug){
                return WEAK;
            }

            if(move_type is Ghost){
                return WEAK;
            }

            if(move_type is Dark){
                return WEAK;
            }

            return BASE;

        }

        public override string ToString(){
            return "Psychic";
        }
    }
    public partial class Bug : Typing{

        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Fire){
                return WEAK;
            }

            if(move_type is Grass){
                return RESIST;
            }

            if(move_type is Fighting){
                return RESIST;
            }

            if(move_type is Ground){
                return RESIST;
            }

            if(move_type is Flying){
                return WEAK;
            }

            if(move_type is Rock){
                return WEAK;
            }

            return BASE;

        }

        public override string ToString(){
            return "Bug";
        }
    }
    public partial class Rock : Typing{

        public override float ApplyBonus(Typing move_type){
            if(move_type is Normal){
                return RESIST;
            }

            if(move_type is Fire){
                return RESIST;
            }

            if(move_type is Water){
                return WEAK;
            }

            if(move_type is Grass){
                return WEAK;
            }

            if(move_type is Fighting){
                return WEAK;
            }

            if(move_type is Poison){
                return RESIST;
            }

            if(move_type is Ground){
                return WEAK;
            }

            if(move_type is Flying){
                return RESIST;
            }

            if(move_type is Steel){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Rock";
        }
    }
    public partial class Ghost : Typing{

        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Normal){
                return IMMUNE;
            }

            if(move_type is Fighting){
                return IMMUNE;
            }

            if(move_type is Poison){
                return RESIST;
            }

            if(move_type is Ghost){
                return WEAK;
            }

            if(move_type is Dark){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Ghost";
        }
    }
    public partial class Dark : Typing{
        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Fighting){
                return WEAK;
            }

            if(move_type is Psychic){
                return IMMUNE;
            }

            if(move_type is Bug){
                return WEAK;
            }

            if(move_type is Ghost){
                return RESIST;
            }

            if(move_type is Dark){
                return RESIST;
            }

            if(move_type is Fairy){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Dark";
        }
    }
    public partial class Dragon : Typing{

        public override float ApplyBonus(Typing move_type){
            if(move_type is Fire){
                return RESIST;
            }

            if(move_type is Water){
                return RESIST;
            }

            if(move_type is Grass){
                return RESIST;
            }

            if(move_type is Electric){
                return RESIST;
            }

            if(move_type is Ice){
                return WEAK;
            }

            if(move_type is Dragon){
                return WEAK;
            }

            if(move_type is Fairy){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Dragon";
        }
    }
    public partial class Steel : Typing{

        public override float ApplyBonus(Typing move_type){
            
            if(move_type is Normal){
                return RESIST;
            }

            if(move_type is Fire){
                return WEAK;
            }

            if(move_type is Grass){
                return RESIST;
            }

            if(move_type is Ice){
                return RESIST;
            }

            if(move_type is Fighting){
                return WEAK;
            }

            if(move_type is Poison){
                return IMMUNE;
            }

            if(move_type is Ground){
                return WEAK;
            }

            if(move_type is Flying){
                return RESIST;
            }

            if(move_type is Psychic){
                return RESIST;
            }

            if(move_type is Bug){
                return RESIST;
            }

            if(move_type is Rock){
                return RESIST;
            }

            if(move_type is Rock){
                return RESIST;
            }

            if(move_type is Dragon){
                return RESIST;
            }

            if(move_type is Steel){
                return RESIST;
            }

            if(move_type is Fairy){
                return RESIST;
            }

            return BASE;

        }

        public override string ToString(){
            return "Steel";
        }
    }
    public partial class Fairy : Typing{
        public override float ApplyBonus(Typing move_type){
            if(move_type is Fighting){
                return RESIST;
            }

            if(move_type is Poison){
                return WEAK;
            }

            if(move_type is Bug){
                return RESIST;
            }

            if(move_type is Dragon){
                return IMMUNE;
            }

            if(move_type is Dark){
                return RESIST;
            }

            if(move_type is Steel){
                return WEAK;
            }

            return BASE;
        }

        public override string ToString(){
            return "Fairy";
        }
    }
    
}
