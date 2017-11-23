using ConsoleApp2.Components;
using ConsoleApp2.Core;
using Microsoft.Xna.Framework;
using RogueSharp;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    public class DebugInfo
    {
        public static void Execute(SadConsole.Console mapConsole, SadConsole.Console debugConsole, IContext context)
        {

            Microsoft.Xna.Framework.Point mousePosInCells = GetMousePosInCells(mapConsole);
            int cellX = mousePosInCells.X;
            int cellY = mousePosInCells.Y;
            debugConsole.Print(14, 0, "         ");
            debugConsole.Print(14, 0, String.Format("({0},{1})", cellX, cellY));
            mapConsole.SetBackground(5, 5, Color.Fuchsia);
            mapConsole.SetBackground(6, 6, Color.Fuchsia);
            mapConsole.SetBackground(7, 7, Color.Fuchsia);
            mapConsole.SetBackground(8, 8, Color.Fuchsia);
            mapConsole.SetBackground(9, 9, Color.Fuchsia);
            mapConsole.SetBackground(10, 10, Color.Fuchsia);
            mapConsole.SetBackground(24, 24, Color.Aquamarine);



            if (SadConsole.Global.MouseState.LeftClicked)
            {
                var map = context.GetCurrentMap();
                Entity player = context.With("UnderControl").First();
                Position playerPos = (Position)player.GetComponent("Position");
                RogueSharp.Cell playerCell = map.GetCell(playerPos.X, playerPos.Y);
                RogueSharp.PathFinder pathfinder = map.CreatePathFinder();
                try
                {
                    map.SetIsWalkable(playerPos.X, playerPos.Y, true);
                    Path path = pathfinder.ShortestPath(playerCell, map.GetCell(cellX, cellY));
                    map.SetIsWalkable(playerPos.X, playerPos.Y, false);
                    debugConsole.Print(8, 1, "                                                ");
                    var s = String.Format("A path exists between the player and ({0},{1})", cellX, cellY);
                    debugConsole.Print(8, 1, s);
                }
                catch (PathNotFoundException)
                {
                    debugConsole.Print(8, 1, "                                                ");
                    var s = String.Format("No path exists between the player and ({0},{1})", cellX, cellY);
                    debugConsole.Print(8, 1, s);
                }
            }
        }
        private static Microsoft.Xna.Framework.Point GetMousePosInCells(IConsole console)
        {
            Microsoft.Xna.Framework.Point fontSize = console.TextSurface.Font.Size;
            int fontWidth = fontSize.X;
            int fontHeight = fontSize.Y;

            Microsoft.Xna.Framework.Point pixelPosOnScreen = SadConsole.Global.MouseState.ScreenPosition;
            int xOffset = console.TextSurface.RenderArea.X;
            int yOffset = console.TextSurface.RenderArea.Y;
            int cellX = xOffset + pixelPosOnScreen.X / fontWidth;
            int cellY = yOffset + pixelPosOnScreen.Y / fontHeight;
            return new Microsoft.Xna.Framework.Point(cellX, cellY);
        }
    }
}
