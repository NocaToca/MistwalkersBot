using Characters;


namespace Types{
    public partial class Water : Typing{

        public override AttributeBonuses PureBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Dexterity,
                    AttributeType.Wisdom
                },
                bonus = new List<int>{
                    2, 
                    1 
                }
            };
        }

        public override AttributeBonuses PrimaryBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Dexterity
                },
                bonus = new List<int>{
                    2 
                }
            };
        }

        public override AttributeBonuses SecondaryBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Wisdom
                },
                bonus = new List<int>{
                    1 
                }
            };
        }

    }
}
