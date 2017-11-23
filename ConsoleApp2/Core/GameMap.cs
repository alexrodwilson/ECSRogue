using ConsoleApp2.Core;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Components
{
    public class GameMap : Component
    {
        private Map _map;
        internal 
            List<Rectangle> Rooms { get; set; }

        public GameMap()
        {
            _map = new Map();
            Rooms = new List<Rectangle>();
        }

        public void Initialize(int width, int height)
        {
            _map.Initialize(width, height);
        }

        public int GetWidth()
        {
            return _map.Width;
        }

        public int GetHeight()
        {
            return _map.Height;
        }
        public IEnumerable<Cell> GetAllCells()
        {
            return _map.GetAllCells();
        }

        internal void ComputeFov(int x, int y, int awareness, bool v)
        {
            _map.ComputeFov(x, y, awareness, v);
        }

        internal void AppendFov(int x, int y, int lightRadius, bool lightWalls)
        {
            _map.AppendFov(x, y, lightRadius, lightWalls);
        }

        internal void SetCellProperties(int x, int y, bool isTransparent, bool isWalkable, bool isExplored)
        {
            _map.SetCellProperties(x, y, isTransparent, isWalkable, isExplored);
        }

        internal IEnumerable<Cell> GetCellsInRows(params int[] rowNumbers)
        {
            return _map.GetCellsInRows(rowNumbers);
        }

        internal Cell GetCell(int x, int y)
        {
            return _map.GetCell(x, y);
        }

        internal IEnumerable<Cell> GetCellsInColumns(params int[] columnNumbers)
        {
            return _map.GetCellsInColumns(columnNumbers);
        }

        internal bool IsInFov(int x, int y)
        {
            return _map.IsInFov(x, y);
        }

        internal void SetIsWalkable(int x, int y, bool v)
        {
            _map.SetCellProperties(x, y, _map.IsTransparent(x, y), v);
        }

        internal void SetCellProperties(int x, int y, bool isTransparent, bool isWalkable)
        {
            _map.SetCellProperties(x, y, isTransparent, isWalkable);
        }
        public PathFinder CreatePathFinder()
        {
            return new PathFinder(_map);
        }
        public void PlaceEntity(Entity e, Cell newCell)
        {
            Position pos = (Position)e.GetComponent("Position");
            Cell formerCell = GetCell(pos.X, pos.Y);
            pos.X = newCell.X;
            pos.Y = newCell.Y;
            if (pos.NewlyCreated)
            {
                pos.NewlyCreated = false;

            }
            else
            {
                if (formerCell.IsTransparent)
                {
                    SetIsWalkable(formerCell.X, formerCell.Y, true);
                }
            }

            if (e.HasComponent("Collidable"))
            {
                SetIsWalkable(pos.X, pos.Y, false);
            }

        }
    }
}
