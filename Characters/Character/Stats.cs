
namespace Characters{

    public partial class Character{
        /*******************************************************STATS*******************************************************************************************************************/
        public enum StatType{
            Special_Attack,
            Special_Defense,
            Speed,
            Attack,
            Health,
            Defense,
            Accuracy,
            Evasion
        }

        public struct Stats{
            public float defense;
            public float special_defense;
            public float attack;
            public float special_attack;
            public float speed;
            public float health;
            public Stats(){
                defense = 0.0f;
                special_defense = 0.0f;
                attack = 0.0f;
                special_attack = 0.0f;
                speed = 0.0f;
                health = 0.0f;
            }
            public Stats(float defense, float special_defense, float attack, float special_attack, float speed, float health){
                this.defense = defense;
                this.special_attack = special_attack;
                this.special_defense = special_defense;
                this.attack = attack;
                this.speed = speed;
                this.health = health;
            }

            public Stats Copy(){
                return new Stats(defense, special_defense, attack, special_attack, speed, health);
            }
        }

        public struct StatLevel{
            public int defense;
            public int special_defense;
            public int special_attack;
            public int attack;
            public int speed;
            public int accuracy;
            public int evasion;

            public StatLevel(){
                defense = 0;
                special_defense = 0;
                special_attack = 0;
                attack = 0;
                speed = 0;
                accuracy = 0;
                evasion = 0;
            }
        }

        public Stats max_stats;
        public Stats effective_stats;
        public StatLevel stat_level;

        private void InitStats(){
            stat_level = new StatLevel();
            max_stats = new Stats();
            effective_stats = max_stats.Copy();
        }

        private void InitStats(StatFields stat_fields){
            stat_level = new StatLevel();
            max_stats = new Stats(stat_fields.defense, stat_fields.special_defense, stat_fields.attack, stat_fields.special_attack, stat_fields.speed, 0.0f);
            effective_stats = max_stats.Copy();
        }

        public void LowerStat(StatType type){
            //We're going to have to find the amount based off of the stat which is a bit tricky
            //I'm just going to hard code this for now
            //+6/-6
            int value = 0;
            if(type == StatType.Attack){
                stat_level.attack -= 1;
                if(stat_level.attack < -6){
                    stat_level.attack = -6;
                }

                value = stat_level.attack;
            } else
            if(type == StatType.Defense){
                stat_level.defense -= 1;
                if(stat_level.defense < -6){
                    stat_level.defense = -6;
                }

                value = stat_level.defense;
            } else
            if(type == StatType.Special_Attack){
                stat_level.special_attack -= 1;
                if(stat_level.special_attack < -6){
                    stat_level.special_attack = -6;
                }

                value = stat_level.special_attack;
            } else
            if(type == StatType.Special_Defense){
                stat_level.special_defense -= 1;
                if(stat_level.special_defense < -6){
                    stat_level.special_defense = -6;
                }

                value = stat_level.special_defense;
            } else
            if(type ==StatType.Speed){
                stat_level.speed -= 1;
                if(stat_level.speed < -6){
                    stat_level.speed = -6;
                }

                value = stat_level.speed;
            }

            float amount = GrabStatAmountFromStage(value);
            

            ChangeStat(type, amount);
        }

        public void RaiseStat(StatType type){
            int value = 0;
            if(type == StatType.Attack){
                stat_level.attack += 1;
                if(stat_level.attack > 6){
                    stat_level.attack = 6;
                }

                value = stat_level.attack;
            } else
            if(type == StatType.Defense){
                stat_level.defense += 1;
                if(stat_level.defense > 6){
                    stat_level.defense = 6;
                }

                value = stat_level.defense;
            } else
            if(type == StatType.Special_Attack){
                stat_level.special_attack += 1;
                if(stat_level.special_attack > 6){
                    stat_level.special_attack = 6;
                }

                value = stat_level.special_attack;
            } else
            if(type == StatType.Special_Defense){
                stat_level.special_defense += 1;
                if(stat_level.special_defense > 6){
                    stat_level.special_defense = 6;
                }

                value = stat_level.special_defense;
            } else
            if(type ==StatType.Speed){
                stat_level.speed += 1;
                if(stat_level.speed > 6){
                    stat_level.speed = 6;
                }

                value = stat_level.speed;
            } else
            if(type ==StatType.Accuracy){
                stat_level.accuracy += 1;
                if(stat_level.accuracy > 6){
                    stat_level.accuracy = 6;
                }

                value = stat_level.accuracy;
            } else
            if(type ==StatType.Evasion){
                stat_level.evasion += 1;
                if(stat_level.evasion > 6){
                    stat_level.evasion = 6;
                }

                value = stat_level.evasion;
            }

            float amount = GrabStatAmountFromStage(value);
            

            ChangeStat(type, amount);
        }

        public float GrabStatAmountFromStage(int value){
            float amount = 0.0f;
            if(value == 6){
                amount = 4.0f;
            } else
            if(value == 5){
                amount = 3.5f;
            } else 
            if(value == 4){
                amount = 3.0f;
            } else 
            if(value == 3){
                amount = 2.5f;
            } else 
            if(value == 2){
                amount = 2.0f;
            } else 
            if(value == 1){
                amount = 1.5f;
            } else 
            if(value == 0){
                amount = 1.0f;
            } else
            if(value == -1){
                amount = 0.66f;
            } else 
            if(value == -2){
                amount = 0.5f;
            } else
            if(value == -3){
                amount = 0.4f;
            } else
            if(value == -4){
                amount = 0.33f;
            } else 
            if(value == -5){
                amount = 0.285f;
            } else 
            if(value == -6){
                amount = 0.25f;
            }
            return amount;
        }

        public void ChangeStat(StatType type, float amount /*amount is a percent*/){
            if(type == StatType.Attack){
                effective_stats.attack = max_stats.attack * amount;
            } else
            if(type == StatType.Defense){
                effective_stats.defense = max_stats.defense * amount;
            } else
            if(type == StatType.Special_Attack){
                effective_stats.special_attack = max_stats.special_attack * amount;
            } else
            if(type == StatType.Special_Defense){
                effective_stats.special_defense = max_stats.special_defense * amount;
            } else
            if(type ==StatType.Speed){
                effective_stats.speed = max_stats.speed * amount;
            }
        }

/*********************************************************************************************************************************************************************************/

    }

}