using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Core
{
     internal abstract class Component
    
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        internal Component() { }


    }
}
