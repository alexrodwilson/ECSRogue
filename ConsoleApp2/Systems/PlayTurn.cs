using ConsoleApp2.Components;
using ConsoleApp2.Core;
using ConsoleApp2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    internal static class PlayTurn
    {
        //tiebreaker is dexterity
        private class TimeUnitsThenDexComparer : IComparer<Entity>
        {
            public int Compare(Entity e1, Entity e2)
            {
                int timeUnitsLeft1 = ((Schedulable)e1.GetComponent("Schedulable")).TimeUnits;
                int timeUnitsLeft2 = ((Schedulable)e2.GetComponent("Schedulable")).TimeUnits;
                int difference = timeUnitsLeft2 - timeUnitsLeft1;
                if (difference != 0)
                {
                    return difference;
                }
                else
                {
                    return ((BaseStats)e2.GetComponent("BaseStats")).Dexterity
                          - ((BaseStats)e1.GetComponent("BaseStats")).Dexterity;
                }
            }
        }


        internal static void Act(IContext context)
        {
            SortedQueue<Entity> schedulables = new SortedQueue<Entity>(context.With("Schedulable"), new TimeUnitsThenDexComparer());
            while(schedulables.Exists(
                e => {
                return ((Schedulable)e.GetComponent("Schedulable")).TimeUnits > 0;}
                ))
            {
                Entity EntityWithMostTime = schedulables.Dequeue();
                PerformAction.Act(EntityWithMostTime, context);
                schedulables.Enqueue(EntityWithMostTime);
            }


        }
    }
}
