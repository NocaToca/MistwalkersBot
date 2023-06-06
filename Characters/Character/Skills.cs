using DSharpPlus.SlashCommands;

namespace Characters{

    public partial class Character{
        
/****************************************************************SKILLS****************************************************************************************************/

        public enum Skill{
            [ChoiceName("Acrobatics")]
            Acrobatics,
            [ChoiceName("Athletics")]
            Athletics,
            [ChoiceName("Deception")]
            Deception,
            [ChoiceName("History")]
            History,
            [ChoiceName("Insight")]
            Insight,
            [ChoiceName("Intimidation")]
            Intimidation,
            [ChoiceName("Investigation")]
            Investigation,
            [ChoiceName("Medicine")]
            Medicine,
            [ChoiceName("Nature")]
            Nature,
            [ChoiceName("Perception")]
            Perception,
            [ChoiceName("Performance")]
            Performance,
            [ChoiceName("Persuasion")]
            Persuasion,
            [ChoiceName("Religion")]
            Religion,
            [ChoiceName("Sleight of Hand")]
            Slieght_of_Hand,
            [ChoiceName("Stealth")]
            Stealth,
            [ChoiceName("Survival")]
            Survival,
            [ChoiceName("Aura")]
            Aura,
            [ChoiceName("Instinct")]
            Instinct
        }

        public List<Skill> proficient_skills;

        public int RollSkill(Skill skill){
            AttributeType type = GrabRelatedAttribute(skill);
            int bonus = attributes.GetRollBonus(type);

            return bonus + ((proficient_skills.Contains(skill)) ? 2 : 0);
        }

        public AttributeType GrabRelatedAttribute(Skill skill){
            switch (skill){
                case Skill.Acrobatics:
                case Skill.Slieght_of_Hand:
                case Skill.Stealth:
                    return AttributeType.Dexterity;

                case Skill.Athletics:
                    return AttributeType.Strength;

                case Skill.Deception:
                case Skill.Intimidation:
                case Skill.Performance:
                case Skill.Persuasion:
                    return AttributeType.Charisma;

                case Skill.History:
                case Skill.Insight:
                case Skill.Investigation:
                case Skill.Medicine:
                case Skill.Nature:
                case Skill.Religion:
                case Skill.Survival:
                    return AttributeType.Wisdom;

                case Skill.Aura:
                case Skill.Instinct:
                case Skill.Perception:
                    return AttributeType.Intelligence;

                default:
                    throw new ArgumentException("Invalid skill.");
            }
        }

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