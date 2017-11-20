using ConsoleApp2.Core;
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
        internal static void Act(SadConsole.Console mapConsole, IContext context)
        {
            mapConsole.Clear();
            foreach (Cell cell in context.GetCurrentMap().GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell, context);
            }
        }
        private static void SetConsoleSymbolForCell(SadConsole.Console console, Cell cell, IContext context)
        {
            //int screenX = cell.X - (view.playerX - view.xOffset);
            //int screenY = cell.Y - (view.playerY - view.yOffset);
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
                    //console.Set(sceenX, screenY, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                    console.SetGlyph(cell.X, cell.Y, '.', Colors.FloorFov, Colors.FloorBackgroundFov);
                }
                else
                {
                    //console.Set(sceenX, screenY, Colors.WallFov, Colors.WallBackgroundFov, '#');
                    console.SetGlyph(cell.X, cell.Y, '#', Colors.WallFov, Colors.FloorBackgroundFov);
                }
            }
            // When a cell is outside of the field of view draw it with darker colors
            else
            {
                if (cell.IsWalkable)
                {
                    console.SetGlyph(cell.X, cell.Y, '.', Colors.Floor, Colors.FloorBackground);
                    //console.Set(sceenX, screenY, Colors.Floor, Colors.FloorBackground, '.');
                }
                else
                {
                    console.SetGlyph(cell.X, cell.Y, '#', Colors.Wall, Colors.WallBackground);
                    //console.Set(sceenX, screenY, Colors.Wall, Colors.WallBackground, '#');
                }
            }
        }
    }
}
