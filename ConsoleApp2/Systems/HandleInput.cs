using ConsoleApp2.Core;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Systems
{
    public static class HandleInput
    {

        public static Command Execute(IEnumerable<Command> commands)
        {
            foreach(Command c in commands)
            {
                if (SadConsole.Global.KeyboardState.IsKeyPressed(c.Key))
                {
                    return c;
                }
            }
            return null;
        }
    }
}
