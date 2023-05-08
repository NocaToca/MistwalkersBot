
using Natures;

#pragma warning disable

namespace Characters{

    public enum AttributeType{
        Strength,
        Constitution,
        Dexterity,
        Intelligence,
        Wisdom,
        Charisma
    }
    
    //This is a huge class to handle all character stuff.
    public partial class Character{

        public enum Type{Player, NPC}

        public enum Occupation{StoreOwner, Explorer, Citizen}

        public string pokemon_name;

        public bool essential = false;

        string character_occupation;

        public string name;
        string background;
        string notes;

/**********************************************************CONSTRUCTION****************************************************************************************************/


        public Character(CharacterFields fields){
            InitStatusSection();

            InitMain(fields.main_fields);
            InitPoints(fields.point_fields);
            InitAttributes(fields.attribute_fields);
            InitSkills(fields.skill_fields);
            InitStats(fields.stat_fields);
            InitInventory(fields.inventory_fields);
            InitMoves(fields.move_fields);
            InitAbilities(fields.ability_fields);
        }

        private void InitMain(MainFields main_fields){
            this.name = main_fields.name;
            this.primary_type = main_fields.type_one;
            this.secondary_type = main_fields.type_two;
            this.character_occupation = main_fields.occupation;
            this.nature = main_fields.nature;
            this.level = main_fields.level;
            this.experience = main_fields.experience;
            this.pokemon_name = main_fields.pokemon;
            this.background = main_fields.background;
            this.notes = main_fields.notes;
        }


/**************************************************************************************************************************************************************************/


/****************************************************************NATURE**************************************************************************************************/

        public NatureType nature;

/**************************************************************************************************************************************************************************/
/*************************************************************LEVELING***************************************************************************************************/
        public int level;
        public int experience;

/**************************************************************************************************************************************************************************/



        public Character(bool stored, string name){

            InitStatusSection();
            InitSpecial();
            InitMoves();
            InitPoints();

            //First we try to load from file, assuming the character is stored internally
            if(stored){
                //Do file stuff
            } else {
                //Grab information from google sheets
            }

        }



    }

    public class InvalidAttributeType : Exception{
        public InvalidAttributeType() : base(){

        }
    }

    public class InvalidMove : Exception{
        public InvalidMove(string message) : base(message){

        }
    }

    public class SpecialParameterDoesNotExist : Exception{
        public SpecialParameterDoesNotExist(string message) : base(message){

        }
    }

}
