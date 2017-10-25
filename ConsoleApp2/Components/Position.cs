using ConsoleApp2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Components
{

    class Position : Component
    {
        internal Position(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public int x
        { get; set; }
        public int y
        { get; set; }
    }
    
}
