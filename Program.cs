using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SmiteSimulator.Classes;
using SmiteSimulator.Helpers;
using static SmiteSimulator.Helpers.ConsoleHelper;

namespace SmiteSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            //EntityManager entityManager = new EntityManager();

            //var entities = entityManager.CreateEntityList(typeof(God));

            //var g1 = entityManager.CreateSingleEntity(typeof(God), new object[] { God.GodCount, 1500, "Hou Yi" }); // args id, hp, name
            //var g2 = entityManager.CreateSingleEntity(typeof(God), new object[] { God.GodCount, 1500, "Xbalanque" }); // args id, hp, name

            //entities.Add(g1);
            //entities.Add(g2);

            //g1.Attack(g2, new Ability("Ricochet", 8.0, 100.0, 1.2, God.DamageType.Physical));

            var s = new StatsGrabber();

            var godList = s.GetAllGodNames();

            //foreach (var god in godList)
            //{
            //    w(god.GetId() + " " + god.GetName());
            //}

            s.GetGodInfo(godList.First(x => x.GetName() == "Agni"));

            godList.First(x=>x.GetName() == "Agni").Status();

            Console.ReadLine();
        }
    }
}
