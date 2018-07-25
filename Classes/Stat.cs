using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using SmiteSimulator.Interfaces;

namespace SmiteSimulator.Classes
{
    class Stat : IStat
    {
        public string Name { get; set; }
        public double Base { get; set; }
        public double IncreasePerLevel { get; set; }

        public Stat(string name, double @base, double increasePerLevel)
        {
            Name = name;
            Base = @base;
            IncreasePerLevel = increasePerLevel;
        }

        public Stat(string name, double[] baseAndScaling)
        {
            Name = name;
            Base = baseAndScaling[0];
            IncreasePerLevel = baseAndScaling[1];
        }

        public double StatAtLevel(int level)
        {
            return Base + IncreasePerLevel * level;
        }

        public void SetBaseAndScaling(double[] baseAndScaling)
        {
            Base = baseAndScaling[0];
            IncreasePerLevel = baseAndScaling[1];
        }
    }
}
