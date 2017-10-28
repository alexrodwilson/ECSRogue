using ConsoleApp2.Core;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    internal static class RenderViewMap
    {
        internal static void Act(RLConsole mapConsole, View view, IContext context)
        {
            mapConsole.Clear();
            foreach (Cell cell in view.visibleCells)
            {
                SetConsoleSymbolForCell(mapConsole, cell, view, context);
            }
        }
        private static void SetConsoleSymbolForCell(RLConsole console, Cell cell, View view, IContext context)
        {
            int sceenX = cell.X - (view.playerX - view.xOffset);
            int screenY = cell.Y - (view.playerY - view.yOffset);
            // When we haven't explored a cell yet, we don't want to draw anything
            if (!cell.IsExplored)
            {
                return;
            }

            // When a cell is currently in the field-of-view it should be drawn with ligher colors
            if (context.GetCurrentMap().IsInFov(cell.X, cell.Y))
            {

                // Choose the symbol to draw based on if the cell is walkable or not
                // '.' for floor and '#' for walls
                if (cell.IsWalkable)
                {
                    console.Set(sceenX, screenY, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                }
                else
                {
                    console.Set(sceenX, screenY, Colors.WallFov, Colors.WallBackgroundFov, '#');
                }
            }
            // When a cell is outside of the field of view draw it with darker colors
            else
            {
                if (cell.IsWalkable)
                {
                    console.Set(sceenX, screenY, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.Set(sceenX, screenY, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }
    }
}
