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
    internal static class UpdateView
    {
        internal static void Act(View view, IContext context)
        {
            IEnumerable<Entity> listOf1 = context.With("FollowedByCamera");
            if(listOf1.Count() != 1)
            {
                throw new InvalidOperationException("There must be 1 entity with a FollowedByCamera component");
            }
            Position pos = (Position)listOf1.First().GetComponent("Position");
            view.playerX = pos.x;
            view.playerY = pos.y;
            int leftMostX = pos.x - view.xOffset;
            int rightMostX = pos.x + view.xOffset;
            int topMostY = pos.y - view.yOffset;
            int bottomMostY = pos.y + view.yOffset;
            //(List<Cell>)context.GetCurrentMap().GetAllCells().FindAll(cell => (cell.))
            view.visibleCells = context.GetCurrentMap().GetAllCells().Where(
                cell => ((cell.X >= leftMostX) && (cell.X <= rightMostX))
                && ((cell.Y >= topMostY) && (cell.Y <= bottomMostY))).ToList(); 
        }
    }
}
