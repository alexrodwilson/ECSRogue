using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using ConsoleApp2.MapUtils;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    public static class MovePlayer
    {

        internal static bool Act(RLKeyboard keyboard, IContext context)
        {
            RLKeyPress kp = keyboard.GetKeyPress();
            if (kp != null)
            {
                IEnumerable<Entity> entities = context.With("UnderControl");
                foreach (Entity e in entities)
                {
                    Position c = (Position)e.GetComponent("Position");
                    int currentX = c.x;
                    int currentY = c.y;
                    GameMap map = context.GetCurrentMap();
  
                    switch (kp.Key)
                    {
                        case RLKey.Keypad8: currentY -= 1; break;
                        case RLKey.Keypad2: currentY += 1; break;
                        case RLKey.Keypad7: currentY -= 1; currentX -= 1;  break;
                        case RLKey.Keypad9: currentY -= 1; currentX += 1; break;
                        case RLKey.Keypad4: currentX -= 1; break;
                        case RLKey.Keypad6: currentX += 1; break;
                        case RLKey.Keypad1: currentY += 1; currentX -= 1; break;
                        case RLKey.Keypad3: currentY += 1; currentX += 1; break;
                    }
                    if (map.GetCell(currentX, currentY).IsWalkable
                        || (!e.HasComponent("Collidable")))
                    {
                        map.PlaceEntity(e, map.GetCell(currentX, currentY));
                    }
                    
                }
                return true;

            }
            return false;
        }
    }
}
