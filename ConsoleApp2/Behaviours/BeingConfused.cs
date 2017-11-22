using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Core;
using ConsoleApp2.Components;

namespace ConsoleApp2.Behaviours
{
    public class BeingConfused : IBehaviour
    {
        public int Execute(Entity owner, IContext context)
        {
            Random random = new Random();
            int randXMov = random.Next(3) - 1;
            int randYMov = random.Next(3) - 1;
            Action<Entity, IContext> randomMovementAction = Utilities.GeneralUtils.MakeMovementAction(randXMov, randYMov);
            randomMovementAction(owner, context);
            return ((BaseStats)owner.GetComponent("BaseStats")).MovementCost;
        }
    }
}
