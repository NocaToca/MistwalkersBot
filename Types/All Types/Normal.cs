using Characters;


namespace Types{
    public partial class Normal : Typing{

        public override AttributeBonuses PureBonuses(){
            throw new Exception("Normal Types should deal with bonuses specially");
        }

        public override AttributeBonuses PrimaryBonuses(){
            throw new Exception("Normal Types should deal with bonuses specially");
        }

        public override AttributeBonuses SecondaryBonuses(){
            throw new Exception("Normal Types should deal with bonuses specially");
        }

    }
}
