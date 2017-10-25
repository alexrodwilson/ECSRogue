using ConsoleApp2.Core;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Components
{
    internal class Renderable : Component
    {
        internal Renderable(char _symbol, RLColor _color)
        {
            symbol = _symbol;
            color = _color;
        }

        internal char symbol { get; set; }
        internal RLColor color { get; set; }
    }
}
