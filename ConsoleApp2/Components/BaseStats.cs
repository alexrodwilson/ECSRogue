using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Components
{
    internal class BaseStats : Component
    {
        internal int Strength
        { get; set; }
        internal int Dexterity
        { get; set; }
        internal int MovementCost
        { get; set; }
        internal BaseStats(int strength, int dexterity, int movementCost)
        {
            Strength = strength;
            Dexterity = dexterity;
            MovementCost = movementCost;
        }

        
    }
}
