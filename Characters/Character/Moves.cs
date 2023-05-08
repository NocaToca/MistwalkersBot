
using Moves;

namespace Characters{

    public partial class Character{
/******************************************************************MOVES*************************************************************************************/

        public List<Move> moves;
        
        private void InitMoves(){
            moves = new List<Move>();
        }

        private void InitMoves(MoveFields move_fields){
            moves = move_fields.moves;
        }

        public void AddMove(Move move){
            moves.Add(move);
        }

        public void UseMove(string name, Character target){
            //First we will look up the move and see if it exists
            Move? move_being_used = null;

            foreach(Move move in moves){
                if(move.name.ToLower() == name.ToLower()){
                    move_being_used = move;
                    break;
                }
            }

            if(move_being_used == null){
                throw new InvalidMove("User does not have " + name + " or it was spelled wrong!");
            }

            float base_damage = move_being_used.Roll(this);

            // We apply effects to make sure we do type changes
            move_being_used.move_effect?.ApplyEffects(this, target);

            float modifier = target.GetModifier(move_being_used.type);

            float main_damage = target.GetDamageEval(base_damage, move_being_used.ability_type) * modifier;

            target.ApplyDamage(main_damage);

            DamageEvent?.Invoke(main_damage, target);
            KillEvent?.Invoke(target);
            FlushDamageEvent();
            FlushKillEvent();
            FlushFailedExpression();
        }

/**************************************************************************************************************************************************************/

    }

}