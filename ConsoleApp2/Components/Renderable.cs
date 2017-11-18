using ConsoleApp2.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ConsoleApp2.Components
{
    internal class Renderable : Component
    {
        internal Renderable(char _symbol, Color _color)
        {
            symbol = _symbol;
            color = _color;
        }

        internal char symbol { get; set; }
        internal Color color { get; set; }
    }
}
