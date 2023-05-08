using Characters;


namespace Types{
    public partial class Rock : Typing{

        public override AttributeBonuses PureBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Constitution
                },
                bonus = new List<int>{
                    3
                }
            };
        }

        public override AttributeBonuses PrimaryBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Strength,
                    AttributeType.Constitution
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
                    AttributeType.Strength
                },
                bonus = new List<int>{
                    1 
                }
            };
        }

    }
}
