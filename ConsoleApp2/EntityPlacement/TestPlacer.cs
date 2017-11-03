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
                Cell randomCell = getRandomWalkableLocation(map, random);
                map.PlaceEntity(e, randomCell);
            }
        }



        private static Cell getRandomWalkableLocation(GameMap map, IRandom random)
        {

            IEnumerable<Cell> shuffledCells = Shuffle<Cell>(map.GetAllCells().ToList(), random);
            foreach (Cell cell in shuffledCells)
            {
                if (cell.IsWalkable)
                {
                    return cell;
                }
            }
            throw new ArgumentException("The gameMap has no walkable cells");
        }

        private static List<T> Shuffle<T>(IList<T> list, IRandom rng)
        {
            List<T> newList = new List<T>();
            foreach(T t in list)
            {
                newList.Add(t);
            }
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = newList[k];
                newList[k] = newList[n];
                newList[n] = value;
            }
            return newList;
        }
    }
}
