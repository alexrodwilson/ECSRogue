using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    internal static class UpdatePlayerFov
    {
        private static readonly int light_radius = 40;
        internal static void Execute(IContext context)
        {
            List<Entity> controlledEntities = (List<Entity>)context.With("UnderControl");
            if (!controlledEntities.Any())
            {
                throw new InvalidOperationException("There are no existing player characters on the current map");
            }
            else
            {
                Entity e = (Entity)controlledEntities.First();
                Position firstPos = (Position) e.GetComponent("Position");
                context.GetCurrentMap().ComputeFov(firstPos.X, firstPos.Y, light_radius, true);
            }
        
            
            foreach (Entity e in controlledEntities)
            {
                Position pos = (Position)e.GetComponent("Position");
                context.GetCurrentMap().AppendFov(pos.X, pos.Y, light_radius, true);
            }
            // Compute the field-of-view based on the player's location and awareness
           
            // Mark all cells in field-of-view as having been explored
            foreach (Cell cell in context.GetCurrentMap().GetAllCells())
            {
                if (context.GetCurrentMap().IsInFov(cell.X, cell.Y))
                {
                    context.GetCurrentMap().SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
        
    }
}
