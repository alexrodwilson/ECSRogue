using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    
    internal static class CountSteps
    {
        private static int steps = 0;

        internal static int Get()
        {
            return steps;
        }
        internal static void Increment()
        {
            steps += 1;
        }

    }
}
