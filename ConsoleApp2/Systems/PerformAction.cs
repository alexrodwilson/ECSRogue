using ConsoleApp2.Behaviours;
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
    internal class PerformAction
    {
        internal static void Act(Entity entity, RLKeyboard keyboard, IContext context)
        {
            if (entity.HasComponent("UnderControl"))
            {
                bool working = MovePlayer.Act(keyboard, context);
                int movementCost = ((BaseStats)entity.GetComponent("BaseStats")).MovementCost;
                SubtractTimeUnits(entity, movementCost);
            }
            else
            {
                IBehaviour behaviour = ((Sentient)entity.GetComponent("Sentient")).Behaviour;
                int actionCost = behaviour.Execute(entity, context);
                SubtractTimeUnits(entity, actionCost);
            }
        }

        private static void SubtractTimeUnits(Entity entity, int actionCost)
        {
            ((Schedulable)entity.GetComponent("Schedulable")).TimeUnits -= actionCost;
        }
    }
}
