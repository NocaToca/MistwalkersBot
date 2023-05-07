using Characters;

#pragma warning disable

namespace Moves{

    public class Target{

    }
    public class Ranged : Target{
        public enum Mode{
            All,
            Enemy,
            Allies
        }
        public Mode targeting_mode;

        public int range = 1;

        public override string ToString()
        {
            return "Ranged. Mode: " + targeting_mode.ToString() + " For range: " + range.ToString() + " tiles.";
        }
    }
    public class Single : Target{
        public enum Mode{
            User,
            Ally,
            Enemy
        }
        public Mode targeting_mode;

        public override string ToString()
        {
            return "Single target. Mode: " + targeting_mode.ToString();
        }
    }

    public class Effect{

        public Target targeting_information;

        public List<ApplyInformation> applications;

        public ApplyInformation remove_effect;

        public Effect(){
            applications = new List<ApplyInformation>();
            remove_effect = new ApplyInformation{
                Apply = (character) =>{
                    return true;
                },
                ReturnType = typeof(bool)
            };
        }

        public override string ToString(){
            string s = "Amount of effects present: " + applications.Count;
            s += "\n";
            return s + targeting_information.ToString();
        }

        public void ApplyEffects(Character user, Character? target){
            if(targeting_information is Ranged){
                throw new ArgumentException("Wrong Function call arguements; range requires multiple targets");
            }
            Single base_target = (Single)targeting_information;

            if(base_target.targeting_mode == Single.Mode.User){
                //Target is going to be null
                foreach(ApplyInformation action in applications){
                    action.Apply(user);
                }

            } else {
                //We're using it on the target
                foreach(ApplyInformation action in applications){
                    action.Apply(target);
                }
            }
        }

        public void AddEffect(ApplyInformation action){
            applications.Add(action);
        }

        public void SetTargetInformation(Target target){
            targeting_information = target;
        }

        public void WriteTargetInformation(){
            Console.WriteLine(targeting_information.ToString());
        }

        public void SetRemoveEffect(ApplyInformation effect){
            if(effect.ReturnType != typeof(bool)){
                throw new ArgumentException("Remove effect needs to have type of bool");
            }
            this.remove_effect = effect;
        }

    }
}