using System;
using Abilities;
using Moves;
using Types;

#pragma warning disable

namespace Characters{

    public enum AttributeType{
        Strength,
        Constitution,
        Dexterity,
        Intelligence,
        Wisdom,
        Charisma
    }
    
    //This is a huge class to handle all character stuff.
    public class Character{

        public enum Type{Player, NPC}

        public enum Occupation{StoreOwner, Explorer, Citizen}

        public bool essential = false;

        Type character_type;
        Occupation character_occupation;

        string name;
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
/****************************************************************************POINTS**********************************************************************************************/

        public enum PointType{
            PowerPoints,
            BellyPoints,
            HealthPoints
        }
        public unsafe class Points{
            public float max_power_points {get; private set;}
            public float* current_power_points;

            public float max_belly_points {get; private set;}
            public float* current_belly_points;

            public float max_health_points {get; private set;}
            public float* current_health_points;

            public Points(float pp, float bp, float hp){
                max_belly_points = bp;
                current_belly_points = &bp;

                max_health_points = hp;
                current_health_points = &hp;

                max_power_points = pp;
                current_power_points = &pp;
            }

            public int Restore(PointType type, float amount, bool percentage){

                float max = 0.0f;
                if(type == PointType.BellyPoints){
                    max = max_belly_points;
                } else
                if(type == PointType.PowerPoints){
                    max = max_power_points;
                } else {
                    max = max_health_points;
                }

                float* current = GrabCurrent(type);

                if(percentage){
                    float value = *current + (max * amount);
                    *current = (value >= max) ? max : value;
                } else {
                    *current = (*current + amount >= max) ? max : *current + amount;
                }

                return (int)(amount - (max - *current));
            }

            public float* GrabCurrent(PointType type){
                if(type == PointType.BellyPoints){
                    return current_belly_points;
                } else
                if(type == PointType.PowerPoints){
                    return current_power_points;
                } else {
                    return current_health_points;
                }
            }

        }

        public Points main_points;

        private void InitPoints(){
            main_points = new Points(10.0f, 10.0f, 10.0f);
        }
/*********************************************************************************************************************************************************************************/

/*****************************************************************************ATTRIBUTES****************************************************************************************/
        public Attributes attributes;

        public unsafe class Attributes{
            private int* strength;
            private int* constitution;
            private int* dexterity;
            private int* intelligence;
            private int* wisdom;
            private int* charisma;

            private Attributes(int strength, int constitution, int dexterity, int intelligence, int wisdom, int charisma){
                *this.strength = strength;
                *this.constitution = constitution;
                *this.dexterity = dexterity;
                *this.intelligence = intelligence;
                *this.wisdom = wisdom;
                *this.charisma = charisma;
            }

            private unsafe int* GetAttribute(AttributeType type){
                if(type == AttributeType.Strength){
                    return strength;
                }
                if(type == AttributeType.Constitution){
                    return constitution;
                }
                if(type == AttributeType.Dexterity){
                    return dexterity;
                }
                if(type == AttributeType.Intelligence){
                    return intelligence; 
                }
                if(type == AttributeType.Wisdom){
                    return wisdom;
                }
                if(type == AttributeType.Charisma){
                    return charisma;
                }

                throw new InvalidAttributeType();
            }

            public unsafe int GetRollBonus(AttributeType type){
                int* attribute_pntr = GetAttribute(type);
                return ((*attribute_pntr) - 10)/2;
            }

            public unsafe void RaiseState(AttributeType type, int amount){
                int* attribute_pntr = GetAttribute(type);
                int bonus = amount + *attribute_pntr;
                *attribute_pntr = bonus;
            }

            public unsafe void SetStat(AttributeType type, int amount){
                int* attribute_pntr = GetAttribute(type);
                *attribute_pntr = amount; 
            }

            public unsafe int GetValue(AttributeType type){
                return *GetAttribute(type);
            }
        }

        public bool MakeSavingThrow(AttributeType saving_throw_type){
            Random ran = new Random();

            float roll = ran.Next(0, 21);
            roll += attributes.GetRollBonus(saving_throw_type);

            return roll > 10;
        }

        public AttributeType GetBestAttributeFromArray(AttributeType[] types){
            int max = 0;
            AttributeType base_type = types[0];

            foreach(AttributeType type in types){
                if(attributes.GetValue(type) > max){
                    max = attributes.GetValue(type);
                    base_type = type;
                }
            }

            return base_type;
        }

/******************************************************************************************************************************************************************************************/

        public Character(bool stored, string name){

            InitStatusSection();
            InitSpecial();
            InitMoves();
            InitPoints();

            //First we try to load from file, assuming the character is stored internally
            if(stored){
                //Do file stuff
            } else {
                //Grab information from google sheets
            }

        }

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
        public Embed ShowAttributes(){
            return null;
        }

        public Embed ShowMoves(){
            return null;
        }

        public Embed ShowAbilities(){
            return null;
        }

        public Embed ShowInventory(){
            return null;
        }
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

        public void ApplyDamage(float damage){
            effective_stats.health -= damage;

            if(effective_stats.health <= 0){
                //we're going to die but we don't have anything here to do that yet
                dead = true;
            }
        }

        public float GetDamageEval(float damage, Move.MoveType type){
            if(type == Move.MoveType.Special){
                return damage - (attributes.GetValue(AttributeType.Wisdom))/4;
            }

            if(type == Move.MoveType.Physical){
                return damage - (attributes.GetValue(AttributeType.Constitution))/4;
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
/******************************************************************MOVES*************************************************************************************/

        public List<Move> moves;
        
        private void InitMoves(){
            moves = new List<Move>();
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

    public class InvalidAttributeType : Exception{
        public InvalidAttributeType() : base(){

        }
    }

    public class InvalidMove : Exception{
        public InvalidMove(string message) : base(message){

        }
    }

    public class SpecialParameterDoesNotExist : Exception{
        public SpecialParameterDoesNotExist(string message) : base(message){

        }
    }

}
