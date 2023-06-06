
using Moves;
using Types;

namespace Characters{

    public partial class Character{

/***************************************************************DAMAGE EVENTS FROM PARSER********************************************************************************/

        //This is used to re-call expressions if a condition has changed 
        public ApplyInformation? failedExpression;

        public void SetFailedExpression(ApplyInformation failedExpression){
            this.failedExpression = failedExpression;
        }

        public void FlushFailedExpression(){
            failedExpression = null;
        }

        public void SetAbsorb(int amount){
            //Amount is percentage wise
            //We're going to add an event listener to our damage
            AddDamageEventListener((damage_amount, target) => {
                main_points.Restore(PointType.HealthPoints, damage_amount * amount/100.0f, false);
            });
        }

        public void ApplyExtraDamage(int amount){
            AddDamageEventListener((damage_amount, target) => {
                target.ApplyDamage(amount); //this damage is true
            });
        }

        //It's a bit of a scuffed work around, but we're going to re-run the kill event after we input the target as dead
        //Interestingly enough, even though this is written like a typical if statement it isn't called the same way
        //This is required since it is after the effect - it is the same way we do split damage
        public void SetKillEvent(ApplyInformation? kill_information){
            AddKillEventListener((target) => {
                if(target.dead){
                    kill_information?.Apply(this);
                }
            }
            );
        }

        

/*******************************************************************************************************************************************************/
/**********************************************************************DAMAGING********************************************************************************/

        public Typing primary_type;
        public Typing? secondary_type;

        public delegate void DamageEventHandler(float damage, Character target);
        public delegate void KillEventHandler(Character target);

        public event DamageEventHandler? DamageEvent;
        public event KillEventHandler? KillEvent;

        public bool dead = false;

        public enum IgnoreType{
            Defense,
            Attack,
            Special_Defense,
            Special_Attack,
            Speed
        }

        public void AddIgnore(IgnoreType type){

        }

        private void FlushDamageEvent(){
            DamageEvent = null;
        }

        public void AddDamageEventListener(DamageEventHandler event_action){
            DamageEvent += event_action;
        }

        private void FlushKillEvent(){
            KillEvent = null;
        }

        public void AddKillEventListener(KillEventHandler event_action){
            KillEvent += event_action;
        }

        public unsafe void ApplyDamage(float damage){
            *main_points.current_health_points -= damage;

            if(*main_points.current_health_points <= 0){
                //we're going to die but we don't have anything here to do that yet
                dead = true;
            }
        }

        public float GetDamageEval(float damage, Move.MoveType type){
            Debug.WriteToDebugFile(damage.ToString());
            if(type == Move.MoveType.Special){
                return damage - ((attributes.GetValue(AttributeType.Wisdom))/4);
            }

            if(type == Move.MoveType.Physical){
                Debug.WriteToDebugFile(attributes.GetValue(AttributeType.Constitution).ToString());
                return damage - ((attributes.GetValue(AttributeType.Constitution))/4);
            }

            throw new ArgumentException("Why are we applying damage for a status effect");
        }

        public float GetModifier(Typing move_type){
            float value = primary_type.ApplyBonus(move_type);

            if(secondary_type != null){
                value *= secondary_type.ApplyBonus(move_type);
            }

            return value;
        }

/**************************************************************************************************************************************************************/
    }

}