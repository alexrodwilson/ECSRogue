using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Core
{
    internal class View
    {
        internal IEnumerable<Cell> visibleCells {get;set;}
        internal int xOffset { get; set; }
        internal int yOffset { get; set; }
        internal int playerX { get; set; }
        internal int playerY { get; set; }
        internal View(int mapWidth, int mapHeight)
        {
            xOffset = mapWidth / 2;
            yOffset = mapHeight / 2;
        }
        
 
    }
}
