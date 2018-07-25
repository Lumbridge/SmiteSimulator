using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using SmiteSimulator.Classes;
using SmiteSimulator.Helpers;
using SmiteSimulator.Interfaces;
using static SmiteSimulator.Helpers.ConsoleHelper;

namespace SmiteSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new StatsGrabber();

            var ymir = new God("Ymir");
            var cabrakan = new God("Cabrakan");

            s.GetGodInfo(ymir);
            s.GetGodInfo(cabrakan);
            
            w($"Ymir has {ymir.Health.StatAtLevel(20)} health at level 20.");
            w($"Cabrakan has {cabrakan.Health.StatAtLevel(20)} health at level 20.\n");

            var ymirBasicAtkDmgLvl20 = ymir.InhandDamage.StatAtLevel(20);
            var ymirAtksPerSec = ymir.AttacksPerSecond.StatAtLevel(20);
            var ymirDPS = ymirBasicAtkDmgLvl20 * ymirAtksPerSec;
            var ymirKillCabraSeconds = cabrakan.Health.StatAtLevel(20) / ymirDPS;

            var cabrakanBasicAtkDmgLvl20 = cabrakan.InhandDamage.StatAtLevel(20);
            var cabrakanAtksPerSec = cabrakan.AttacksPerSecond.StatAtLevel(20);
            var cabrakanDPS = cabrakanBasicAtkDmgLvl20 * cabrakanAtksPerSec;
            var cabraKillYmirSeconds = ymir.Health.StatAtLevel(20) / cabrakanDPS;

            w($"Ymir basic attack damage: {ymirBasicAtkDmgLvl20}, attacks per second: {ymirAtksPerSec} (DPS: {ymirDPS})");
            w($"Cabrakan basic attack damage: {cabrakanBasicAtkDmgLvl20}, attacks per second: {cabrakanAtksPerSec} (DPS: {cabrakanDPS})\n");

            w($"Ymir would kill cabra in {ymirKillCabraSeconds} seconds.");
            w($"Cabrakan would kill cabra in {cabraKillYmirSeconds} seconds.");

            Console.ReadLine();
        }

        public static List<IEntity> GetAllGodsAndStatsBenchmarkParallel()
        {
            // Create instance of stats grabber
            var s = new StatsGrabber();

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            var godList = s.GetAllSmiteGodsAndStats();

            // Stop timing.
            stopwatch.Stop();

            using (Stream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, godList);
                Console.WriteLine($"Parallel Method Loaded {godList.Count} gods and {stream.Length / 1024f} kilobytes of data in {stopwatch.Elapsed.Seconds} seconds.\n");
            }

            return godList;
        }

        public static List<IEntity> GetAllGodsAndStatsBenchmarkSequential()
        {
            // Create instance of stats grabber
            var s = new StatsGrabber();

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            var godList = s.GetAllGodNames();

            foreach (var god in godList)
            {
                try
                {
                    s.GetGodInfo(godList.First(x => x.Name == god.Name));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading {god.Name} - ({e.Message})");
                }
            }

            // Stop timing.
            stopwatch.Stop();

            using (Stream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, godList);
                Console.WriteLine($"Sequential Method Loaded {godList.Count} gods and {stream.Length / 1024f} kilobytes of data in {stopwatch.Elapsed.Seconds} seconds.");
            }

            return godList;
        }
    }
}
