using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SmiteSimulator.Interfaces;
using static SmiteSimulator.Helpers.ConsoleHelper;

namespace SmiteSimulator.Classes
{
    class God : IGod
    {
        // Global God Identifier (Incremented with every instantiated God)
        internal static int GodCount = 1;
        
        // God Meta Info
        private int _id { get; set; }
        private string _name { get; set; }
        private string _title { get; set; }
        private string _pantheon { get; set; }
        private string _inhandType { get; set; }
        private string _class { get; set; }
        private string _pros { get; set; }
        private string _difficulty { get; set; }
        private string _releaseDate { get; set; }
        private string _favourCost { get; set; }
        private string _gemCost { get; set; }
        private string _voiceActor { get; set; }

        // Base God Stats
        private double _health { get; set; }
        private double _healthIncreasePerLevel { get; set; }
        private double _mana { get; set; }
        private double _manaIncreasePerLevel { get; set; }
        private double _speed { get; set; }
        private double _speedIncreasePerLevel { get; set; }
        private double _range { get; set; }
        private double _rangeIncreasePerLevel { get; set; }
        private double _attacksPerSecond { get; set; }
        private double _attacksPerSecondIncreasePerLevel { get; set; }

        // Basic Attack Power Stats
        private double _inhandBaseDamage { get; set; }
        private double _inhandBaseDamageIncreasePerLevel { get; set; }
        private double _inhandScalingPercentage { get; set; }
        private object _progression { get; set; } // e.g. basic attack chain erlang shen = { 0.75, 0.75, 0.75, 1.1, 0.9 }

        // Protections Stats
        private double _physicalProtections { get; set; }
        private double _magicalProtections { get; set; }

        // Regen Stats
        private double _HP5 { get; set; }
        private double _MP5 { get; set; }

        // Constructors
        public God(string name)
        {
            _id = GodCount;
            _name = name;

            Interlocked.Increment(ref GodCount);
        }
        public God(
            double health,
            double mana,
            double speed,
            double range,
            double attacksPerSecond,
            double damage,
            object progression,
            double physicalProtections,
            double magicalProtections,
            double hp5,
            double mp5)
        {
            _id = GodCount;
            _health = health;
            _mana = mana;
            _speed = speed;
            _range = range;
            _attacksPerSecond = attacksPerSecond;
            _inhandBaseDamage = damage;
            _progression = progression;
            _physicalProtections = physicalProtections;
            _magicalProtections = magicalProtections;
            _HP5 = hp5;
            _MP5 = mp5;

            Interlocked.Increment(ref GodCount);
        }

        // Deconstructor
        ~God()
        {
            Interlocked.Decrement(ref GodCount);
        }

        // Meta Getters
        public int GetId()
        {
            return _id;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetTitle()
        {
            return _title;
        }
        public string GetPantheon()
        {
            return _pantheon;
        }
        public string GetInhandType()
        {
            return _inhandType;
        }
        public string GetClass()
        {
            return _class;
        }
        public string GetPros()
        {
            return _pros;
        }
        public string GetDifficulty()
        {
            return _difficulty;
        }
        public string GetReleaseDate()
        {
            return _releaseDate;
        }
        public string GetFavourCost()
        {
            return _favourCost;
        }
        public string GetGemCost()
        {
            return _gemCost;
        }
        public string GetVoiceActor()
        {
            return _voiceActor;
        }

        // Meta Setters
        public void SetId(int id)
        {
            _id = id;
        }
        public void SetName(string name)
        {
            _name = name;
        }
        public void SetTitle(string title)
        {
            _title = title;
        }
        public void SetPantheon(string pantheon)
        {
            _pantheon = pantheon;
        }
        public void SetInhandType(string inhandType)
        {
            _inhandType = inhandType;
        }
        public void SetClass(string classType)
        {
            _class = classType;
        }
        public void SetPros(string pros)
        {
            _pros = pros;
        }
        public void SetDifficulty(string difficulty)
        {
            _difficulty = difficulty;
        }
        public void SetReleaseDate(string releaseDate)
        {
            _releaseDate = releaseDate;
        }
        public void SetFavourCost(string favourCost)
        {
            _favourCost = favourCost;
        }
        public void SetGemCost(string gemCost)
        {
            _gemCost = gemCost;
        }
        public void SetVoiceActor(string voiceActor)
        {
            _voiceActor = voiceActor;
        }

        // Base Stat Getters
        public double GetHealth()
        {
            return _health;
        }
        public double GetHealthIncreasePerLevel()
        {
            return _healthIncreasePerLevel;
        }
        public double GetMana()
        {
            return _mana;
        }
        public double GetManaIncreasePerLevel()
        {
            return _manaIncreasePerLevel;
        }
        public double GetSpeed()
        {
            return _speed;
        }
        public double GetSpeedIncreasePerLevel()
        {
            return _speedIncreasePerLevel;
        }
        public double GetRange()
        {
            return _range;
        }
        public double GetRangeIncreasePerLevel()
        {
            return _rangeIncreasePerLevel;
        }
        public double GetAttacksPerSecond()
        {
            return _attacksPerSecond;
        }
        public double GetAttacksPerSecondIncreasePerLevel()
        {
            return _attacksPerSecondIncreasePerLevel;
        }

        // Base Stat Setters
        public void SetHealth(double health)
        {
            _health = health;
        }
        public void SetHealthIncreasePerLevel(double increase)
        {
            _healthIncreasePerLevel = increase;
        }
        public void SetMana(double mana)
        {
            _mana = mana;
        }
        public void SetManaIncreasePerLevel(double increase)
        {
            _manaIncreasePerLevel = increase;
        }
        public void SetSpeed(double speed)
        {
            _speed = speed;
        }
        public void SetSpeedIncreasePerLevel(double increase)
        {
            _speedIncreasePerLevel = increase;
        }
        public void SetRange(double range)
        {
            _range = range;
        }
        public void SetRangeIncreasePerLevel(double increase)
        {
            _rangeIncreasePerLevel = increase;
        }
        public void SetAttacksPerSecond(double attacksPerSecond)
        {
            _attacksPerSecond = attacksPerSecond;
        }
        public void SetAttacksPerSecondIncreasePerLevel(double increase)
        {
            _attacksPerSecondIncreasePerLevel = increase;
        }

        // Basic Attack Stat Getters
        public double GetInhandBaseDamage()
        {
            return _inhandBaseDamage;
        }
        public double GetInhandBaseDamageIncreasePerLevel()
        {
            return _inhandBaseDamageIncreasePerLevel;
        }
        public object GetProgression()
        {
            return _progression;
        }
        public double GetInhandScalingPercentage()
        {
            return _inhandScalingPercentage;
        }

        // Basic Attack Stat Setters
        public void SetInhandBaseDamage(double damage)
        {
            _inhandBaseDamage = damage;
        }
        public void SetInhandBaseDamageIncreasePerLevel(double increase)
        {
            _inhandBaseDamageIncreasePerLevel = increase;
        }
        public void SetProgression(object progression)
        {
            _progression = progression;
        }
        public void SetInhandScalingPercentage(double scaling)
        {
            _inhandScalingPercentage = scaling;
        }

        // Protections Stat Getters
        public double GetPhysicalProtections()
        {
            return _physicalProtections;
        }
        public double GetMagicalProtections()
        {
            return _magicalProtections;
        }

        // Protections Stat Setters
        public void SetPhysicalProtections(double physicalProtections)
        {
            _physicalProtections = physicalProtections;
        }
        public void SetMagicalProtections(double magicalProtections)
        {
            _magicalProtections = magicalProtections;
        }

        // Regen Stat Getters
        public double GetHP5()
        {
            return _HP5;
        }
        public double GetMP5()
        {
            return _MP5;
        }

        // Regen Stat Setters
        public void SetHP5(double hp5)
        {
            _HP5 = hp5;
        }
        public void SetMP5(double mp5)
        {
            _MP5 = mp5;
        }

        // Action Methods
        public void Status()
        {
            foreach (var m in this.GetType().GetMethods().Where(x => x.DeclaringType == typeof(God)))
            {
                if (m.ReturnType != typeof(void) && m.ReturnType != typeof(IGod))
                    Console.WriteLine(m.Name + ": " + m.Invoke(this, new object[] { }));
            }
        }
        public bool IsAlive()
        {
            return _health > 0;
        }
        public void Attack(IGod enemy, Ability ability)
        {
            var totalDmg = ability.BaseDamage + GetInhandBaseDamage() * ability.StatModifier;
            totalDmg -= enemy.GetPhysicalProtections();
            enemy.Damage(totalDmg);
        }
        public void Defend(IGod enemy, Ability ability)
        {
            throw new NotImplementedException();
        }
        public void Damage(double dmg)
        {
            _health -= dmg;
        }
        public void Heal(int hp)
        {
            _health += hp;
        }
    }
}
