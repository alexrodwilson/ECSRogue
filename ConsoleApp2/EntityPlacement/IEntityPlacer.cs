using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.MapUtils
{
    internal interface IEntityPlacer
    {
        void Place(List<Entity> entities, IRandom random, GameMap map);
    }
}
