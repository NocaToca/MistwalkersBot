

namespace Characters{

    public partial class Character{
        
/****************************************************************SKILLS****************************************************************************************************/

        public enum Skill{
            Acrobatics,
            Athletics,
            Deception,
            History,
            Insight,
            Intimidation,
            Investigation,
            Medicine,
            Nature,
            Perception,
            Performance,
            Persuasion,
            Religion,
            Slieght_of_Hand,
            Stealth,
            Survival,
            Aura,
            Instinct
        }

        public List<Skill> proficient_skills;

        private void InitSkills(SkillFields skill_fields){
            proficient_skills = new List<Skill>();

            if(skill_fields.acrobatics) {
                proficient_skills.Add(Skill.Acrobatics);
            }
            if(skill_fields.athletics) {
                proficient_skills.Add(Skill.Athletics);
            }
            if(skill_fields.deception) {
                proficient_skills.Add(Skill.Deception);
            }
            if(skill_fields.history) {
                proficient_skills.Add(Skill.History);
            }
            if(skill_fields.insight) {
                proficient_skills.Add(Skill.Insight);
            }
            if(skill_fields.intimidation) {
                proficient_skills.Add(Skill.Intimidation);
            }
            if(skill_fields.investigation) {
                proficient_skills.Add(Skill.Investigation);
            }
            if(skill_fields.medicine) {
                proficient_skills.Add(Skill.Medicine);
            }
            if(skill_fields.nature) {
                proficient_skills.Add(Skill.Nature);
            }
            if(skill_fields.perception) {
                proficient_skills.Add(Skill.Perception);
            }
            if(skill_fields.performance) {
                proficient_skills.Add(Skill.Performance);
            }
            if(skill_fields.persuasion) {
                proficient_skills.Add(Skill.Persuasion);
            }
            if(skill_fields.religion) {
                proficient_skills.Add(Skill.Religion);
            }
            if(skill_fields.sleight_of_hand) {
                proficient_skills.Add(Skill.Slieght_of_Hand);
            }
            if(skill_fields.stealth) {
                proficient_skills.Add(Skill.Stealth);
            }
            if(skill_fields.survival) {
                proficient_skills.Add(Skill.Survival);
            }
            if(skill_fields.aura) {
                proficient_skills.Add(Skill.Aura);
            }
            if(skill_fields.instinct) {
                proficient_skills.Add(Skill.Instinct);
            }
        }


/**************************************************************************************************************************************************************************/

    }

}