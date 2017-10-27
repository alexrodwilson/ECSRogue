using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Components
{
    internal class Health : Component
    {
        internal Health(int v)
        {
            value = v;
        }
        internal int value { get; set; }
    }
}
