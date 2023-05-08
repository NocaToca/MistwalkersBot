using Characters;


namespace Types{
    public partial class Fairy : Typing{

        public override AttributeBonuses PureBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Charisma,
                    AttributeType.Intelligence
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
                    AttributeType.Charisma
                },
                bonus = new List<int>{
                    2 
                }
            };
        }

        public override AttributeBonuses SecondaryBonuses(){
            return new AttributeBonuses{
                type = new List<AttributeType>{
                    AttributeType.Intelligence
                },
                bonus = new List<int>{
                    1 
                }
            };
        }

    }
}
