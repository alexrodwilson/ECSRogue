using ConsoleApp2.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Core
{
    public interface IContext
    {
        IEnumerable<Entity> With(string componentName);
        IEnumerable<Entity> Without(string componentName);
        GameMap GetCurrentMap();
    }
}
