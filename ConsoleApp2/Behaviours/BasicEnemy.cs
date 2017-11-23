using ConsoleApp2.Components;
using ConsoleApp2.Core;
using RogueSharp;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Behaviours
{
    public class BasicEnemy : IBehaviour
    {
        private Path wanderingPath;
        private State currentState;
        private IRandom random;

        private enum State
        {
            Wandering, Hunting, Fleeing
        }


        public BasicEnemy()
        {
            currentState = State.Wandering;
            random = new RogueSharp.Random.DotNetRandom();
        }

        public int Execute(Entity owner, IContext context)
        {
            Start:
            Entity player = context.With("UnderControl").First();
            Position playerPos = (Position)player.GetComponent("Position");
            Position ownerPos = (Position)owner.GetComponent("Position");
            GameMap map = context.GetCurrentMap();
            PathFinder pathFinder = map.CreatePathFinder();

            if (currentState == State.Hunting)
            {
                // Before we find a path, make sure to make the monster and player Cells walkable
                map.SetIsWalkable(ownerPos.X, ownerPos.Y, true);
                map.SetIsWalkable(playerPos.X, playerPos.Y, true);
                Path huntingPath = null;
                try
                {
                    huntingPath = pathFinder.ShortestPath(
                    map.GetCell(ownerPos.X, ownerPos.Y),
                    map.GetCell(playerPos.X, playerPos.Y));
                }
                catch (PathNotFoundException)
                {
                    map.SetIsWalkable(ownerPos.X, ownerPos.Y, false);
                    map.SetIsWalkable(playerPos.X, playerPos.Y, false);
                    return 100;
                    // The monster can see the player, but cannot find a path to him
                    // This could be due to other monsters blocking the way
                    // Add a message to the message log that the monster is waiting
                    // Game.MessageLog.Add($"{monster.Name} waits for a turn");
                }

                // Don't forget to set the walkable status back to false
                map.SetIsWalkable(ownerPos.X, ownerPos.Y, false);
                map.SetIsWalkable(playerPos.X, playerPos.Y, false);

                // In the case that there was a path, tell the CommandSystem to move the monster

                try
                {
                    // TODO: This should be path.StepForward() but there is a bug in RogueSharp V3
                    // The bug is that a Path returned from a PathFinder does not include the source Cell
                    map.PlaceEntity(owner, huntingPath.Steps.First());
                    return ((BaseStats)owner.GetComponent("BaseStats")).MovementCost;
                }
                catch (NoMoreStepsException)
                {
                    throw new NoMoreStepsException();
                    //   Game.MessageLog.Add($"{monster.Name} growls in frustration");
                }
            }
            // If the monster has not been alerted, compute a field-of-view 
            // Use the monster's Awareness value for the distance in the FoV check
            // If the player is in the monster's FoV then alert it
            // Add a message to the MessageLog regarding this alerted status
            else if (currentState == State.Wandering)
            {
                if (map.IsInFov(ownerPos.X, ownerPos.Y))
                {
                    //Game.MessageLog.Add($"{monster.Name} is eager to fight {player.Name}");
                    currentState = State.Hunting;
                    goto Start;
                }

                else
                {
                    if (wanderingPath == null)
                    {
                        map.SetIsWalkable(ownerPos.X, ownerPos.Y, true);
                        wanderingPath = pathFinder.ShortestPath(
                        map.GetCell(ownerPos.X, ownerPos.Y),
                        Utilities.MapUtils.GetRandomWalkableLocation(map, random));
                    }

                    map.PlaceEntity(owner, wanderingPath.Steps.First());
                    return ((BaseStats)owner.GetComponent("BaseStats")).MovementCost;

                }
            }
            else throw new InvalidOperationException("The BasicEnemy execute operation was not successful");
        }

    }
}
