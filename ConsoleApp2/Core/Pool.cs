using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Components;

namespace ConsoleApp2.Core
{
    class Pool : IContext
    {
        private List<Entity> _entities;
        private GameMap _map;

        internal Pool(GameMap map, List<Entity> entities)
        {
            _map = map;
            _entities = entities;
        }

        public GameMap GetCurrentMap()
        {
            return _map;
        }

        public IEnumerable<Entity> With (string componentName)
        {
            return _entities.FindAll(e => e.HasComponent(componentName));
        }
        public IEnumerable<Entity> Without (string componentName)
        {
            return _entities.Where(e => !e.HasComponent(componentName));
        }
    }
}
