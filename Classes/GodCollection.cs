using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmiteSimulator.Interfaces;

namespace SmiteSimulator.Classes
{
    class GodCollection
    {
        public GodCollection(IEnumerable<IEntity> collection)
        {
            GodList.AddRange(collection);
        }

        public List<IEntity> GodList { get; set; } = new List<IEntity>();

        // Define the indexer to allow client code to use [] notation.
        public God this[string godName]
        {
            get { return (God)GodList.FirstOrDefault(t => t.Name == godName); }
        }
    }
}
