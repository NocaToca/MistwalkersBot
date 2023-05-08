using Characters;


namespace Types{
    public partial class Poison : Typing{

        public override AttributeBonuses PureBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Intelligence,
                    AttributeType.Dexterity,
                    AttributeType.Wisdom
                },
                bonus = new List<int>{
                    1,
                    1, 
                    1 
                }
            };
        }

        public override AttributeBonuses PrimaryBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Wisdom,
                    AttributeType.Intelligence
                },
                bonus = new List<int>{
                    1,
                    1
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
