using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RogueSharp;
using RogueSharp.Random;

namespace ConsoleApp2.MapUtils
{
    class TestPlacer : IEntityPlacer
    {
        public void Place(List<Entity> entities, IRandom random, GameMap map)
        {
            foreach(Entity e in entities)
            {
                Cell randomCell =
                    Utilities.MapUtils.GetRandomWalkableLocation(map, random);
                map.PlaceEntity(e, randomCell);
            }
        }




    }
}
