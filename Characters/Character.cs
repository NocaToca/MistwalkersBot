using System;
using Abilities;
using Moves;
using Types;

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
    public class Character{

        public enum Type{Player, NPC}

        public enum Occupation{StoreOwner, Explorer, Citizen}

        public bool essential = false;

        Type character_type;
        Occupation character_occupation;

        string name;

        

        public unsafe class Attributes{
            private int* strength;
            private int* constitution;
            private int* dexterity;
            private int* intelligence;
            private int* wisdom;
            private int* charisma;

            private Attributes(int strength, int constitution, int dexterity, int intelligence, int wisdom, int charisma){
                *this.strength = strength;
                *this.constitution = constitution;
                *this.dexterity = dexterity;
                *this.intelligence = intelligence;
                *this.wisdom = wisdom;
                *this.charisma = charisma;
            }

            private unsafe int* GetAttribute(AttributeType type){
                if(type == AttributeType.Strength){
                    return strength;
                }
                if(type == AttributeType.Constitution){
                    return constitution;
                }
                if(type == AttributeType.Dexterity){
                    return dexterity;
                }
                if(type == AttributeType.Intelligence){
                    return intelligence; 
                }
                if(type == AttributeType.Wisdom){
                    return wisdom;
                }
                if(type == AttributeType.Charisma){
                    return charisma;
                }

                throw new InvalidAttributeType();
            }

            public unsafe int GetRollBonus(AttributeType type){
                int* attribute_pntr = GetAttribute(type);
                return ((*attribute_pntr) - 10)/2;
            }

            public unsafe void RaiseState(AttributeType type, int amount){
                int* attribute_pntr = GetAttribute(type);
                int bonus = amount + *attribute_pntr;
                *attribute_pntr = bonus;
            }

            public unsafe void SetStat(AttributeType type, int amount){
                int* attribute_pntr = GetAttribute(type);
                *attribute_pntr = amount; 
            }
        }

        public Attributes attributes;

        public Character(bool stored, string name){

            //First we try to load from file, assuming the character is stored internally
            if(stored){
                //Do file stuff
            } else {
                //Grab information from google sheets
            }

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

    }

    public class InvalidAttributeType : Exception{
        public InvalidAttributeType() : base(){

        }
    }

}
