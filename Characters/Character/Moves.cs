
using Moves;
using DSharpPlus.Entities;

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

        public unsafe DiscordEmbed UseMove(string name, Character? target = null){
            

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

            DiscordEmbedBuilder embded = new DiscordEmbedBuilder{
                Title = this.name + " used " + move_being_used?.name + ((target == null) ? " against " + target?.name + "!" : "" )
            };

            #pragma warning disable 8602
            float base_damage = move_being_used.Roll(this); //can't be null bc of our null check
            #pragma warning restore 

            // We apply effects to make sure we do type changes
            move_being_used.move_effect?.ApplyEffects(this, target);

            float modifier = target.GetModifier(move_being_used.type);

            float main_damage = target.GetDamageEval(base_damage, move_being_used.ability_type) * modifier;

            // Debug.WriteToDebugFile(main_damage.ToString());

            target.ApplyDamage(main_damage);

            //Event calls
            DamageEvent?.Invoke(main_damage, target);
            KillEvent?.Invoke(target);
            FlushDamageEvent();
            FlushKillEvent();
            FlushFailedExpression(); //This is for the KillEvent

            //Now we can finally assemble our embed
            if(target != null){
                embded.AddField("Damage Inforation:", this.name + " dealt " + ((int)main_damage).ToString() + " (" + ((int)modifier).ToString()+"x effectiveness) to " + target?.name);
                embded.AddField("Target Information: ", "Current HP: " + (*target.main_points.current_health_points).ToString() + "/" + target.main_points.max_health_points.ToString() +
                " | Status Effects: " + ((target.status_stack.Count == 0) ? " None." : target.ReturnCurrentStatus().ToString()));
            }

            return embded.Build();
        }

/**************************************************************************************************************************************************************/

    }

}