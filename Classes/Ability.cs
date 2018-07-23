using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmiteSimulator.Classes
{
    class Ability
    {
        public Ability(string name, double cooldownTimer, double baseDamage, double statModifier)
        {
            Name = name;
            CooldownTimer = cooldownTimer;
            BaseDamage = baseDamage;
            StatModifier = statModifier;
        }

        public string Name { get; set; }

        public double CooldownTimer { get; set; }
        public double BaseDamage { get; set; }
        public double StatModifier { get; set; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
