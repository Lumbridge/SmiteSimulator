using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
            var s = new StatsGrabber();

            var godList = s.GetAllGodNames();

            foreach (var god in godList)
            {
                try
                {
                    s.GetGodInfo(godList.First(x => x.GetName() == god.GetName()));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error loading {god.GetName()} - ({e.Message})");
                }
            }

            using (Stream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, godList);
                Console.WriteLine($"Loaded {godList.Count} gods and {stream.Length / 1024f} kilobytes of data.");
            }
            
            //foreach (var god in godList.OrderByDescending(x => x.GetInhandBaseDamage()))
            //{
            //    w(god.GetName() + " " + god.GetInhandBaseDamage());
            //}

            Console.ReadLine();
        }
    }
}
