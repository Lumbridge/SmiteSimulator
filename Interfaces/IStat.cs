using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmiteSimulator.Interfaces
{
    interface IStat
    {
        string Name { get; set; }
        double Base { get; set; }
        double IncreasePerLevel { get; set; }

        double StatAtLevel(int level);

        void SetBaseAndScaling(double[] baseAndScaling);
    }
}
