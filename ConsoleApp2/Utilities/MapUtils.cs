using ConsoleApp2.Components;
using RogueSharp;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utilities
{
    class MapUtils
    {
        public static Cell GetRandomWalkableLocation(GameMap map, IRandom random)
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
            foreach (T t in list)
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
