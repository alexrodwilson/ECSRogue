using ConsoleApp2.Components;
using RogueSharp;
using RogueSharp.Random;
using System;
using System.Linq;

namespace ConsoleApp2.Map_Generators
{

    internal static class MapGenerators
    {
        internal static GameMap BasicRooms(int width, int height,
  int maxRooms, int roomMaxSize, int roomMinSize, IRandom random)
        {
            GameMap map = new GameMap();
            map.Initialize(width, height);
            // Try to place as many rooms as the specified maxRooms
            // Note: Only using decrementing loop because of WordPress formatting
            for (int r = maxRooms; r > 0; r--)
            {
                // Determine the size and position of the room randomly
                int roomWidth = random.Next(roomMinSize, roomMaxSize);
                int roomHeight = random.Next(roomMinSize, roomMaxSize);
                int roomXPosition = random.Next(0, width - roomWidth - 1);
                int roomYPosition = random.Next(0, height - roomHeight - 1);

                // All of our rooms can be represented as Rectangles
                var newRoom = new Rectangle(roomXPosition, roomYPosition,
                  roomWidth, roomHeight);

                // Check to see if the room rectangle intersects with any other rooms
                bool newRoomIntersects = map.Rooms.Any(room => newRoom.Intersects(room));

                // As long as it doesn't intersect add it to the list of rooms
                if (!newRoomIntersects)
                {
                    map.Rooms.Add(newRoom);
                }
            }
            // Iterate through each room that we wanted placed 
            // call CreateRoom to make it
            foreach (Rectangle room in map.Rooms)
            {
                CreateRoom(room, map, random);
            }

            return map;
            
        }
        // Given a rectangular area on the map
        // set the cell properties for that area to true
        private static void CreateRoom(Rectangle room, GameMap map, IRandom random)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    map.SetCellProperties(x, y, true, true, false); 
                }
            }
            // Iterate through each room that was generated
            // Don't do anything with the first room, so start at r = 1 instead of r = 0
            for (int r = 1; r < map.Rooms.Count; r++)
            {
                // For all remaing rooms get the center of the room and the previous room
                int previousRoomCenterX = map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = map.Rooms[r].Center.X;
                int currentRoomCenterY = map.Rooms[r].Center.Y;

                // Give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
                if (random.Next(1, 2) == 1)
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY, map);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX, map);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX, map);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY, map);
                }
            }
        }
        // Carve a tunnel out of the map parallel to the x-axis
        private static void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition, GameMap map)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                map.SetCellProperties(x, yPosition, true, true);
            }
        }

        // Carve a tunnel out of the map parallel to the y-axis
        private static void CreateVerticalTunnel(int yStart, int yEnd, int xPosition, GameMap map)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                map.SetCellProperties(xPosition, y, true, true);
            }
        }

    }
}
