using System;
using System.Collections.Generic;
using System.Linq;

using SmiteSimulator.Interfaces;

namespace SmiteSimulator.Classes
{
    class EntityManager
    {
        public IEntity CreateSingleEntity(Type type)
        {
            return (IEntity)Activator.CreateInstance(type);
        }

        public IEntity CreateSingleEntity(Type type, object[] args)
        {
            return (IEntity)Activator.CreateInstance(type, args);
        }

        public List<IEntity> CreateEntityList(Type type)
        {
            Console.WriteLine("Entity Manager: Creating list of " + type.Name + ".");
            return new List<IEntity>();
        }

        public IEntity GetEntityByName(string name, List<IEntity> entities)
        {
            return entities.FirstOrDefault(x => x.Name == name);
        }
    }
}
