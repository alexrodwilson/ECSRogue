using ConsoleApp2.Behaviours;
using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Components
{
    internal class Sentient : Component
    {
        internal IBehaviour Behaviour { get; set; }
  
        internal Sentient(IBehaviour behaviour)
        {
            Behaviour= behaviour;
        }

    }
}
 