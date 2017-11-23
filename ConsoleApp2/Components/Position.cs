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
            X = _x;
            Y = _y;
            NewlyCreated = true;
        }
        public int X
        { get; set; }
        public int Y
        { get; set; }
        internal bool NewlyCreated
        { get; set; }
    }
    
}
