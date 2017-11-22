using ConsoleApp2.Components;
using ConsoleApp2.Core;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Utilities
{
    public static class GeneralUtils
    {
        public static IEnumerable<Command> ProvideBasicCommands()

        {
            var commands = new List<Command>();
            commands.Add(new Command(Keys.NumPad1, MakeMovementAction(-1, 1)));
            commands.Add(new Command(Keys.NumPad2, MakeMovementAction(0, 1)));
            commands.Add(new Command(Keys.NumPad3, MakeMovementAction(1, 1)));
            commands.Add(new Command(Keys.NumPad4, MakeMovementAction(-1, 0)));
            commands.Add(new Command(Keys.NumPad6, MakeMovementAction(1, 0)));
            commands.Add(new Command(Keys.NumPad7, MakeMovementAction(-1, -1)));
            commands.Add(new Command(Keys.NumPad8, MakeMovementAction(0, -1)));
            commands.Add(new Command(Keys.NumPad9, MakeMovementAction(1, -1)));
            return commands;
        }

        public static Action<Entity, IContext> MakeMovementAction(int xmov, int ymov)
        {
            return (Entity e, IContext context) =>
            {
                Position pos = (Position)e.GetComponent("Position");
                int newX = pos.x + xmov;
                int newY = pos.y + ymov;
                GameMap map = context.GetCurrentMap();
                if (map.GetCell(newX, newY).IsWalkable
                   || (!e.HasComponent("Collidable")))
                {
                    map.PlaceEntity(e, map.GetCell(newX, newY));
                }

            };

        }
    }
}
