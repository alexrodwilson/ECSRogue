using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.MapUtils
{
    internal static class MapUtils
    {
        internal static void PlaceEntity(Entity e, Point newPosition, GameMap map)
        {
            Position pos = (Position)e.GetComponent("Position");
            if (pos.NewlyCreated)
            {
                pos.NewlyCreated = false;
                pos.x = newPosition.X;
                pos.y = newPosition.Y;
            }
            else
            {
                Cell formerCell = map.GetCell(pos.x, pos.y);
                if (formerCell.IsTransparent)
                {
                    map.SetIsWalkable(pos.x, pos.y, true);
                }
                pos.x = newPosition.X;
                pos.y = newPosition.Y;
            }

            if (e.HasComponent("Collidable"))
            {
                map.SetIsWalkable(pos.x, pos.y, false);
            }

        }
    }
}
