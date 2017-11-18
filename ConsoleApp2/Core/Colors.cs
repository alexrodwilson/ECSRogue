
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ConsoleApp2.Core
{
    internal static class Colors
    {
        public static Color DoorBackground = Palette.ComplimentDarkest;
        public static Color Door = Palette.ComplimentLighter;
        public static Color DoorBackgroundFov = Palette.ComplimentDarker;
        public static Color DoorFov = Palette.ComplimentLightest;
        public static Color FloorBackground = Color.Black;
        public static Color Floor = Palette.AlternateDarkest;
        public static Color FloorBackgroundFov = Palette.DbDark;
        public static Color FloorFov = Palette.Alternate;
        public static Color WallBackground = Palette.SecondaryDarkest;
        public static Color Wall = Palette.Secondary;
        public static Color WallBackgroundFov = Palette.SecondaryDarker;
        public static Color WallFov = Palette.SecondaryLighter;
        public static Color GoblinColor = Color.Green;
        public static Color KoboldColor = new Color(255, 165, 0);
        public static Color OozeColor = new Color(102, 205, 170);
        public static Color Player = Palette.DbLight;
        public static Color InventoryHeading = Palette.DbLight;
    }
}
