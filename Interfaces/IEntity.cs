using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmiteSimulator.Classes;

namespace SmiteSimulator.Interfaces
{
    interface IEntity
    {
        // Meta Properties
        string Name { get; set; }
        string Title { get; set; }
        string Pantheon { get; set; }
        string InhandType1 { get; set; }
        string InhandType2 { get; set; }
        string Class { get; set; }
        string Pros { get; set; }
        string Difficulty { get; set; }
        string ReleaseDate { get; set; }
        string FavourCost { get; set; }
        string GemCost { get; set; }
        string VoiceActor { get;set; }

        // Base Stat Properties
        Stat Health { get; set; }
        Stat Mana { get; set; }
        Stat Speed { get; set; }
        Stat Range { get; set; }
        PercentageScalingStat AttacksPerSecond { get; set; }

        // Basic Attack Properties
        Stat InhandDamage { get; set; }
        double InhandScalingPercentage { get; }
        List<double> ProgressionDamageScaling { get; set; }
        List<double> ProgressionSpeedScaling { get; set; }

        // inhand attack chain
        string ProgressionDamageScalingString { get; set; }
        string ProgressionSpeedScalingString { get; set; }

        // Protections Stat Properties
        Stat PhysicalProtections { get; set; }
        Stat MagicalProtections { get; set; }

        // Regen Stat Properties
        Stat HP5 { get; set; }
        Stat MP5 { get; set; }

        // Action Methods
        void Status();
    }
}
