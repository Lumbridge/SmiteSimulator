using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmiteSimulator.Classes;

namespace SmiteSimulator.Interfaces
{
    interface IGod
    {
        // Meta Getters
        string GetName();
        string GetTitle();
        string GetPantheon();
        string GetInhandType1();
        string GetInhandType2();
        string GetClass();
        string GetPros();
        string GetDifficulty();
        string GetReleaseDate();
        string GetFavourCost();
        string GetGemCost();
        string GetVoiceActor();

        // Meta Setters
        void SetName(string name);
        void SetTitle(string title);
        void SetPantheon(string pantheon);
        void SetInhandType1(string inhandType1);
        void SetInhandType2(string inhandType2);
        void SetClass(string classType);
        void SetPros(string pros);
        void SetDifficulty(string difficulty);
        void SetReleaseDate(string releaseDate);
        void SetFavourCost(string favourCost);
        void SetGemCost(string gemCost);
        void SetVoiceActor(string voiceActor);

        // Base Stat Getters
        double GetHealth();
        double GetHealthIncreasePerLevel();
        double GetMana();
        double GetManaIncreasePerLevel();
        double GetSpeed();
        double GetSpeedIncreasePerLevel();
        double GetRange();
        double GetRangeIncreasePerLevel();
        double GetAttacksPerSecond();
        double GetAttacksPerSecondIncreasePerLevelPercent();

        // Base Stat Setters
        void SetHealth(double health);
        void SetHealthIncreasePerLevel(double increase);
        void SetMana(double mana);
        void SetManaIncreasePerLevel(double increase);
        void SetSpeed(double speed);
        void SetSpeedIncreasePerLevel(double increase);
        void SetRange(double range);
        void SetRangeIncreasePerLevel(double increase);
        void SetAttacksPerSecond(double attacksPerSecond);
        void SetAttacksPerSecondIncreasePerLevelPercent(double increase);

        // Basic Attack Stat Getters
        double GetInhandBaseDamage();
        double GetInhandBaseDamageIncreasePerLevel();
        double GetInhandScalingPercentage();
        List<double> GetProgressionDamageScaling();
        List<double> GetProgressionSpeedScaling();
        string GetProgressionDamageScalingString();
        string GetProgressionSpeedScalingString();

        // Basic Attack Stat Setters
        void SetInhandBaseDamage(double damage);
        void SetInhandBaseDamageIncreasePerLevel(double increase);
        void SetInhandScalingPercentage(double scaling);
        void SetProgressionDamageScaling(List<double> pScaling);
        void SetProgressionSpeedScaling(List<double> pScaling);

        // Protections Stat Getters
        double GetPhysicalProtections();
        double GetPhysicalProtectionsIncreasePerLevel();
        double GetMagicalProtections();
        double GetMagicalProtectionsIncreasePerLevel();

        // Protections Stat Setters
        void SetPhysicalProtections(double physicalProtections);
        void SetPhysicalProtectionsIncreasePerLevel(double increase);
        void SetMagicalProtections(double magicalProtections);
        void SetMagicalProtectionsIncreasePerLevel(double increase);

        // Regen Stat Getters
        double GetHP5();
        double GetHP5IncreasePerLevel();
        double GetMP5();
        double GetMP5IncreasePerLevel();

        // Regen Stat Setters
        void SetHP5(double hp5);
        void SetHP5IncreasePerLevel(double increase);
        void SetMP5(double mp5);
        void SetMP5IncreasePerLevel(double increase);

        // Action Methods
        void Status();
        bool IsAlive();
        void Attack(IGod enemy, Ability ability);
        void Damage(double dmg);
        void Heal(int hp);
    }
}
