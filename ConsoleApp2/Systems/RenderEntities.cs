using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    internal static class RenderEntities
    {
        internal static void Act(RLConsole console, IContext context)
        {
            GameMap map = context.GetCurrentMap();
            
            foreach(Entity e in context.With("Renderable"))
            {
                Renderable renderable = (Renderable)e.GetComponent("Renderable");
                Position pos = (Position)e.GetComponent("Position");
                int x = pos.x;
                int y = pos.y;
                RLColor color = renderable.color;
                char symbol = renderable.symbol;

               //if (! map.GetCell(x, y).IsExplored)
               // {
               //     return;
               // }
                // Only draw the actor with the color and symbol when they are in field-of-view
                if (map.IsInFov(x, y))
                {
                    console.Set(x, y, color, Colors.FloorBackgroundFov, symbol);
                }
                else
                {
                    // When not in field-of-view just draw a normal floor
                    console.Set(x, y, Colors.Floor, Colors.FloorBackground, '.');
                }
            }
        }
      
    }
}
        



