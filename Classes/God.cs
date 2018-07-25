using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SmiteSimulator.Interfaces;
using static SmiteSimulator.Helpers.ConsoleHelper;

namespace SmiteSimulator.Classes
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    class God : IEntity
    {
        // God Meta Info
        public string Name { get; set; }
        public string Title { get; set; }
        public string Pantheon { get; set; }
        public string InhandType1 { get; set; }
        public string InhandType2 { get; set; }
        public string Class { get; set; }
        public string Pros { get; set; }
        public string Difficulty { get; set; }
        public string ReleaseDate { get; set; }
        public string FavourCost { get; set; }
        public string GemCost { get; set; }
        public string VoiceActor { get; set; }

        // Base God Stats
        public Stat Health { get; set; } = new Stat("Health", 0, 0);
        public Stat Mana { get; set; } = new Stat("Mana", 0, 0);
        public Stat Speed { get; set; } = new Stat("Speed", 0, 0);
        public Stat Range { get; set; } = new Stat("Range", 0, 0);
        public PercentageScalingStat AttacksPerSecond { get; set; } = new PercentageScalingStat("Attacks Per Second", 0, 0);

        // Basic Attack Power Stats
        public Stat InhandDamage { get; set; } = new Stat("Inhand Damage", 0, 0);
        public double InhandScalingPercentage => InhandType2 == "Physical" ? 100.0 : 20.0;

        // inhand attack chain
        public List<double> ProgressionDamageScaling { get; set; }
        public List<double> ProgressionSpeedScaling { get; set; }

        // string variants of the attack chain
        public string ProgressionDamageScalingString {
            get 
            {
                string p = "";
                for (var i = 0; i < ProgressionDamageScaling.Count; i++)
                {
                    p += $"{ProgressionDamageScaling[i]}";
                    if (i + 1 != ProgressionDamageScaling.Count)
                        p += '/';
                }
                return p;
            }
            set { }
        }
        public string ProgressionSpeedScalingString {
            get
            {
                string p = "";
                for (var i = 0; i < ProgressionSpeedScaling.Count; i++)
                {
                    p += $"{ProgressionSpeedScaling[i]}";
                    if (i + 1 != ProgressionSpeedScaling.Count)
                        p += '/';
                }
                return p;
            }
            set { }
        }

        // Protections Stats
        public Stat PhysicalProtections { get; set; } = new Stat("Physical Protections", 0, 0);
        public Stat MagicalProtections { get; set; } = new Stat("Magical Protections", 0, 0);

        // Regen Stats
        public Stat HP5 { get; set; } = new Stat("HP5", 0, 0);
        public Stat MP5 { get; set; } = new Stat("MP5", 0, 0);

        // Constructors
        public God(string name)
        {
            Name = name;
        }

        // Action Methods
        public void Status()
        {
            foreach (var m in this.GetType().GetProperties().Where(x => x.DeclaringType == typeof(God)))
            {
                Console.WriteLine(m.Name + ": " + m.GetValue(this));
            }
        }
    }
}
