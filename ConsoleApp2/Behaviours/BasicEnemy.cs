using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Behaviours
{
    public class BasicEnemy : IBehaviour
    {
        private enum State
        {
            Wandering, Hunting, Fleeing
        }

        State currentState;
        public int Execute(Entity owner, IContext context)
        {
            Entity hostile = context.With("UnderControl").First();
            Position hostilePos = (Position)hostile.GetComponent("Position");
            Position entityPos = (Position)owner.GetComponent("Position");
            int entityX = entityPos.x;
            int entityY = entityPos.y;
            GameMap map = context.GetCurrentMap();

            // If the monster has not been alerted, compute a field-of-view 
            // Use the monster's Awareness value for the distance in the FoV check
            // If the player is in the monster's FoV then alert it
            // Add a message to the MessageLog regarding this alerted status
            if (currentState == State.Wandering)
            {
                if (map.IsInFov(entityX, entityY))
                {
                    //Game.MessageLog.Add($"{monster.Name} is eager to fight {player.Name}");
                    currentState = State.Hunting;
                }
            }

                // Before we find a path, make sure to make the monster and player Cells walkable
                map.SetIsWalkable(entityX, entityY, true);
                map.SetIsWalkable(hostilePos.x, hostilePos.y, true);

                PathFinder pathFinder = map.CreatePathFinder();
                Path path = null;
                try
                {
                    path = pathFinder.ShortestPath(
                    map.GetCell(entityX, entityY),
                    map.GetCell(hostilePos.x, hostilePos.y));
                }
                catch (PathNotFoundException)
                {
                    return 0;
                    // The monster can see the player, but cannot find a path to him
                    // This could be due to other monsters blocking the way
                    // Add a message to the message log that the monster is waiting
                   // Game.MessageLog.Add($"{monster.Name} waits for a turn");
                }

                // Don't forget to set the walkable status back to false
                map.SetIsWalkable(entityX, entityY, false);
                map.SetIsWalkable(hostilePos.x, hostilePos.y, false);

                // In the case that there was a path, tell the CommandSystem to move the monster

                    try
                    {
                        // TODO: This should be path.StepForward() but there is a bug in RogueSharp V3
                        // The bug is that a Path returned from a PathFinder does not include the source Cell
                        map.PlaceEntity(owner, path.StepForward());
                        return ((BaseStats)owner.GetComponent("BaseStats")).MovementCost;
                    }
                    catch (NoMoreStepsException)
                    {
                        throw new NoMoreStepsException();
                     //   Game.MessageLog.Add($"{monster.Name} growls in frustration");
                    }

        }
    }
}
