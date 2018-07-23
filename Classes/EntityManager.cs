using System;
using System.Collections.Generic;
using System.Linq;

using SmiteSimulator.Interfaces;

namespace SmiteSimulator.Classes
{
    class EntityManager
    {
        public IGod CreateSingleEntity(Type type)
        {
            return (IGod)Activator.CreateInstance(type);
        }

        public IGod CreateSingleEntity(Type type, object[] args)
        {
            return (IGod)Activator.CreateInstance(type, args);
        }

        public List<IGod> CreateEntityList(Type type)
        {
            Console.WriteLine("Entity Manager: Creating list of " + type.Name + ".");
            return new List<IGod>();
        }

        public IGod GetEntityByName(string name, List<IGod> entities)
        {
            return entities.FirstOrDefault(x => x.GetName() == name);
        }
    }
}
