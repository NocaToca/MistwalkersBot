

namespace Characters{

    public partial class Character{
/**************************************************************SPECIAL*****************************************************************************************/
        public Dictionary<string, int> special_environmnet;
        private void InitSpecial(){
            special_environmnet = new Dictionary<string, int>();
        }

        public int RemoveSpecial(string keyword, int amount){
            if(!special_environmnet.ContainsKey(keyword)){
                throw new SpecialParameterDoesNotExist("Parameter: " + keyword + " does not currently exist");
            }

            int current_amount = -1;
            special_environmnet.TryGetValue(keyword, out current_amount);

            if(current_amount == -1){
                throw new Exception("Unknown Error in RemoveSpecial. Key should exist!");
            }

            if(current_amount - amount < 0){
                throw new IndexOutOfRangeException("Amount is greater than current value.");
            }

            special_environmnet[keyword] = current_amount - amount;
            return amount;
        }

        public int RemoveAllSpecial(string keyword){
            // Console.WriteLine("amount");

            if(!special_environmnet.ContainsKey(keyword)){
                throw new SpecialParameterDoesNotExist("Parameter: " + keyword + " does not currently exist");
            }

            return RemoveSpecial(keyword, special_environmnet[keyword]);
        }

        public int AddSpecial(string keyword, int amount){
            if(!special_environmnet.ContainsKey(keyword)){
                throw new SpecialParameterDoesNotExist("Parameter: " + keyword + " does not currently exist");
            }
            
            special_environmnet[keyword] += amount;
            return amount;
        }

        public bool QueuerySpecial(string keyword, Func<int, bool> queuery){
            return queuery(special_environmnet[keyword]);
        }

        public void CreateSpecial(string keyword){
            special_environmnet.Add(keyword, 0);
        }

        public void PrintSpecial(string keyword){
            Console.WriteLine(special_environmnet[keyword].ToString());
        }



/**************************************************************************************************************************************************************/


    }

}