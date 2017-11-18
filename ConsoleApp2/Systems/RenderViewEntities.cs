using ConsoleApp2.Components;
using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ConsoleApp2.Systems
{
    internal static class RenderViewEntities
    {
        internal static void Act(RLConsole console, View view, IContext context)
        {
            GameMap map = context.GetCurrentMap();
            
            foreach(Entity e in context.With("Renderable"))
            {
                Renderable renderable = (Renderable)e.GetComponent("Renderable");
                Position pos = (Position)e.GetComponent("Position");
                int x = pos.x;
                int y = pos.y;
                int adjustedX = x - (view.playerX - view.xOffset);
                int adjustedY = y - (view.playerY - view.yOffset);
                Color color = renderable.color;
                char symbol = renderable.symbol;

               //if (! map.GetCell(x, y).IsExplored)
               // {
               //     return;
               // }
                // Only draw the actor with the color and symbol when they are in field-of-view
                if (map.IsInFov(x, y))
                {
                    console.Set(adjustedX, adjustedY, color, Colors.FloorBackgroundFov, symbol);
                }
                else
                {
                    // When not in field-of-view just draw a normal floor
                    console.Set(adjustedX, adjustedY, Colors.Floor, Colors.FloorBackground, '.');
                }
            }
        }
      
    }
}
        



