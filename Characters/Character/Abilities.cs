
using Abilities;

namespace Characters{

    public partial class Character{
/**************************************************************ABILITIES**************************************************************************************************/
        public List<Ability> abilities;
        private void InitAbilities(AbilityFields ability_fields){
            abilities = ability_fields.abilities;
        }

/**************************************************************************************************************************************************************************/

    }

}