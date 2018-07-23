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
        int GetId();
        string GetName();
        string GetTitle();
        string GetPantheon();
        string GetInhandType();
        string GetClass();
        string GetPros();
        string GetDifficulty();
        string GetReleaseDate();
        string GetFavourCost();
        string GetGemCost();
        string GetVoiceActor();

        // Meta Setters
        void SetId(int id);
        void SetName(string name);
        void SetTitle(string title);
        void SetPantheon(string pantheon);
        void SetInhandType(string inhandType);
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
        double GetAttacksPerSecondIncreasePerLevel();

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
        void SetAttacksPerSecondIncreasePerLevel(double increase);

        // Basic Attack Stat Getters
        double GetInhandBaseDamage();
        double GetInhandBaseDamageIncreasePerLevel();
        double GetInhandScalingPercentage();
        object GetProgression();

        // Basic Attack Stat Setters
        void SetInhandBaseDamage(double damage);
        void SetInhandBaseDamageIncreasePerLevel(double increase);
        void SetInhandScalingPercentage(double scaling);
        void SetProgression(object progression);

        // Protections Stat Getters
        double GetPhysicalProtections();
        double GetMagicalProtections();

        // Protections Stat Setters
        void SetPhysicalProtections(double physicalProtections);
        void SetMagicalProtections(double magicalProtections);

        // Regen Stat Getters
        double GetHP5();
        double GetMP5();

        // Regen Stat Setters
        void SetHP5(double hp5);
        void SetMP5(double mp5);

        // Action Methods
        void Status();
        bool IsAlive();
        void Attack(IGod enemy, Ability ability);
        void Defend(IGod enemy, Ability ability);
        void Damage(double dmg);
        void Heal(int hp);
    }
}
