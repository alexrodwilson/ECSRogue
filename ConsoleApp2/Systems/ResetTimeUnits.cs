using ConsoleApp2.Components;
using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    class ResetTimeUnits
    {
        public static void Execute(IContext context)
        {
            IEnumerable<Entity> schedulables = context.With("Schedulable");
            foreach(Entity e in schedulables)
            {
                Schedulable sched = (Schedulable)e.GetComponent("Schedulable");
                sched.TimeUnits = Math.Min(100, sched.TimeUnits + 100);
            }
        }
    }
}
