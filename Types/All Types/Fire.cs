using Characters;


namespace Types{
    public partial class Fire : Typing{

        public override AttributeBonuses PureBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Strength,
                    AttributeType.Dexterity
                },
                bonus = new List<int>{
                    2, //str
                    1 //dex
                }
            };
        }

        public override AttributeBonuses PrimaryBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Strength
                },
                bonus = new List<int>{
                    2 //str
                }
            };
        }

        public override AttributeBonuses SecondaryBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Dexterity
                },
                bonus = new List<int>{
                    1 //dex
                }
            };
        }

    }
}
