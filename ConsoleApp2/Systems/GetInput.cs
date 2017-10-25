using ConsoleApp2.Core;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    internal static class GetInput
    {
        internal static RLKeyPress Act(RLKeyboard keyboard, IContext context)
        {
            return keyboard.GetKeyPress();
        }
    }




}
