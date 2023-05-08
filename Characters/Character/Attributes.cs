
using Types;
using Natures;
using System.Runtime.InteropServices;

namespace Characters{

    public partial class Character{

            
/*****************************************************************************ATTRIBUTES****************************************************************************************/
        public Attributes attributes;

        public unsafe class Attributes{
            private int* strength;
            private int* constitution;
            private int* dexterity;
            private int* intelligence;
            private int* wisdom;
            private int* charisma;

            private class AttributeBonuses{
                public int strength_bonus;
                public int constitution_bonus;
                public int dexterity_bonus;
                public int intelligence_bonus;
                public int wisdom_bonus;
                public int charisma_bonus;

                public AttributeBonuses(int strength, int constitution, int dexterity, int intelligence, int wisdom, int charisma){
                    //First we will find the base bonuses:
                    strength_bonus = strength;
                    constitution_bonus = constitution;
                    dexterity_bonus = dexterity;
                    intelligence_bonus =  intelligence;
                    wisdom_bonus = wisdom;
                    charisma_bonus = charisma;
                }

                public static AttributeBonuses CreateAttributeBonusesFromCharacter(Character c){
                    int str_bonus = (*c.attributes.strength - 10)/2;
                    int con_bonus = (*c.attributes.constitution - 10)/2;
                    int dex_bonus = (*c.attributes.dexterity - 10)/2;
                    int int_bonus = (*c.attributes.intelligence - 10)/2;
                    int wis_bonus = (*c.attributes.wisdom - 10)/2;
                    int chr_bonus = (*c.attributes.charisma - 10)/2;

                    void ApplyBonuses(Types.AttributeBonuses bonuses){
                        for(int i = 0; i < bonuses.type.Count; i++){
                            AttributeType type = bonuses.type[i];
                            if(type == AttributeType.Strength){
                                str_bonus += bonuses.bonus[i];
                            } else 
                            if(type == AttributeType.Constitution){
                                con_bonus += bonuses.bonus[i];
                            } else
                            if(type == AttributeType.Dexterity){
                                dex_bonus += bonuses.bonus[i];
                            } else  
                            if(type == AttributeType.Intelligence){
                                int_bonus += bonuses.bonus[i];
                            } else 
                            if(type == AttributeType.Wisdom){
                                wis_bonus += bonuses.bonus[i];
                            } else 
                            if(type == AttributeType.Charisma){
                                chr_bonus += bonuses.bonus[i];
                            }
                        }
                    }

                    //Now we grab the typing rules:
                    if(c.secondary_type == null){
                        if(c.primary_type.Equals(Typing.Normal)){
                            //uhh well I don't know how to deal with this so I'm just going to assume it is narcis
                            chr_bonus += 1;
                            con_bonus += 2;
                        } else {
                            ApplyBonuses(c.primary_type.PureBonuses());
                        }
                    } else {
                        if(c.primary_type.Equals(Typing.Normal)){
                            //idk what to do for this situation rn but it probably will be an extra field in input
                        } else {
                            ApplyBonuses(c.primary_type.PrimaryBonuses());
                        }
                        if(c.secondary_type.Equals(Typing.Normal)){

                        } else {
                            ApplyBonuses(c.secondary_type.PrimaryBonuses());
                        }
                    }

                    Tuple<AttributeType, AttributeType> nature = Nature.GrabNatureAttributes(c.nature);
                    ApplyBonuses(new Types.AttributeBonuses(new List<AttributeType>{
                            nature.Item1
                        },
                        new List<int>{
                            2
                        }));
                    ApplyBonuses(new Types.AttributeBonuses(new List<AttributeType>{
                            nature.Item2
                        },
                        new List<int>{
                            -1
                        }));

                    return new AttributeBonuses(str_bonus, con_bonus, dex_bonus, int_bonus, wis_bonus, chr_bonus);
                }
            }

            private AttributeBonuses attribute_bonuses;

            public Attributes(int strength, int constitution, int dexterity, int intelligence, int wisdom, int charisma){
                int* str = (int*)Marshal.AllocHGlobal(sizeof(int));
                int* con = (int*)Marshal.AllocHGlobal(sizeof(int));
                int* dex = (int*)Marshal.AllocHGlobal(sizeof(int));
                int* intel = (int*)Marshal.AllocHGlobal(sizeof(int));
                int* wis = (int*)Marshal.AllocHGlobal(sizeof(int));
                int* chr = (int*)Marshal.AllocHGlobal(sizeof(int));

                *str = strength;
                *con = constitution;
                *dex = dexterity;
                *intel = intelligence;
                *wis = wisdom;
                *chr = charisma;

                this.strength = str;
                this.constitution = con;
                this.dexterity = dex;
                this.intelligence = intel;
                this.wisdom = wis;
                this.charisma = chr;
                
            }

            ~Attributes(){
                Marshal.FreeHGlobal((IntPtr)strength);
                Marshal.FreeHGlobal((IntPtr)constitution);
                Marshal.FreeHGlobal((IntPtr)dexterity);
                Marshal.FreeHGlobal((IntPtr)intelligence);
                Marshal.FreeHGlobal((IntPtr)wisdom);
                Marshal.FreeHGlobal((IntPtr)charisma);
            }
        

            public void CreateBonuses(Character c){
                attribute_bonuses = AttributeBonuses.CreateAttributeBonusesFromCharacter(c);
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

            public unsafe void RaiseState(AttributeType type, int amount){
                int* attribute_pntr = GetAttribute(type);
                int bonus = amount + *attribute_pntr;
                *attribute_pntr = bonus;
            }

            public unsafe void SetStat(AttributeType type, int amount){
                int* attribute_pntr = GetAttribute(type);
                *attribute_pntr = amount; 
            }

            public unsafe int GetValue(AttributeType type){
                return *GetAttribute(type);
            }

            public int GetRollBonus(AttributeType type){
                if(type == AttributeType.Strength){
                    return attribute_bonuses.strength_bonus;
                } else 
                if(type == AttributeType.Constitution){
                    return attribute_bonuses.constitution_bonus;
                } else
                if(type == AttributeType.Dexterity){
                    return attribute_bonuses.dexterity_bonus;
                } else 
                if(type == AttributeType.Intelligence){
                    return attribute_bonuses.intelligence_bonus;
                } else 
                if(type == AttributeType.Wisdom){
                    return attribute_bonuses.wisdom_bonus;
                } else 
                if(type == AttributeType.Charisma){
                    return attribute_bonuses.charisma_bonus;
                }

                throw new ArgumentException();
            }
        }

        private void InitAttributes(AttributeFields attribute_fields){
            this.attributes = new Character.Attributes(attribute_fields.strength, attribute_fields.constitution, attribute_fields.dexterity, attribute_fields.intelligence, attribute_fields.wisdom, attribute_fields.charisma);
            attributes.CreateBonuses(this);
        }

        public bool MakeSavingThrow(AttributeType saving_throw_type){
            Random ran = new Random();

            float roll = ran.Next(0, 21);
            roll += attributes.GetRollBonus(saving_throw_type);

            return roll > 10;
        }

        public AttributeType GetBestAttributeFromArray(AttributeType[] types){
            int max = 0;
            AttributeType base_type = types[0];

            foreach(AttributeType type in types){
                if(attributes.GetValue(type) > max){
                    max = attributes.GetValue(type);
                    base_type = type;
                }
            }

            return base_type;
        }

/******************************************************************************************************************************************************************************************/

    }

}