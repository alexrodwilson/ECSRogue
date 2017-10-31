using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Components
{
    internal class Schedulable : Component
    {
        internal int TimeUnits { get; set; }
        internal Schedulable()
        {
            TimeUnits = 100;
        }
        
    }
}
