using ConsoleApp2.Behaviours;
using ConsoleApp2.Components;
using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    internal class PerformAction
    {
        internal static void Execute(Entity entity, Command playerCommand, IContext context)
        {
            if (entity.HasComponent("UnderControl"))
            {
                int movementCost = ((BaseStats)entity.GetComponent("BaseStats")).MovementCost;
                playerCommand.Execute(entity, context);
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
