using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Core
{
    public class Command
    {
        public Keys Key { get; set; }
        Action<Entity, IContext> _action;
        
        public Command(Keys key, Action<Entity, IContext> action)
        {
            Key = key;
            _action = action;
        }

        public void Execute(Entity entity, IContext context)
        {
            _action.Invoke(entity, context);
        }
    }
}
