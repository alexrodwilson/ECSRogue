using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Behaviours
{
    interface IBehaviour
    {
         int Execute(Entity owner, IContext context);
    }
    
}
