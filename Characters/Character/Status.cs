

namespace Characters{

    public partial class Character{
/*******************************************************************STATUS******************************************************************************/
        public Stack<Status> status_stack;

        public void InitStatusSection(){
            status_stack = new Stack<Status>();
        }

        public bool AddStatus(Status status){
            if(status_stack.Contains(status)){
                return false;
            }
            status_stack.Push(status);
            return true;
        }

        public Status ReturnCurrentStatus(){
            return status_stack.Peek();
        }
/*******************************************************************************************************************************************************/

    }

}