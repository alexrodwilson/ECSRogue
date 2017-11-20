using ConsoleApp2.Components;
using ConsoleApp2.Core;

using RogueSharp;
using System;
using System.Collections.Generic;
using ConsoleApp2.MapUtils;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ConsoleApp2.Systems
{
    public static class MovePlayer
    {

        internal static bool Act(IContext context)
        {
            bool hasMoved = false;
            IEnumerable<Entity> entities = context.With("UnderControl");
            foreach (Entity e in entities)
            {
                Position c = (Position)e.GetComponent("Position");
                int currentX = c.x;
                int currentY = c.y;
                GameMap map = context.GetCurrentMap();

                if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad8)) currentY -= 1;
                else if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad2)) currentY += 1;
                else if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad7)) { currentY -= 1; currentX -= 1; }
                else if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad9)) { currentY -= 1; currentX += 1; }
                else if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad4)) currentX -= 1;
                else if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad6)) currentX += 1;
                else if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad1)) { System.Console.WriteLine("Input received"); currentY += 1; currentX -= 1; }
                else if (SadConsole.Global.KeyboardState.IsKeyPressed(Keys.NumPad3)) { currentY += 1; currentX += 1; } 
                
                if (map.GetCell(currentX, currentY).IsWalkable
                    || (!e.HasComponent("Collidable")))
                {
                    map.PlaceEntity(e, map.GetCell(currentX, currentY));
                    hasMoved = true;
                }

            }
            return hasMoved;
        }
    }
}
