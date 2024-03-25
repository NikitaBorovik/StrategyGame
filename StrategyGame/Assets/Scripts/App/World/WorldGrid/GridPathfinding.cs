using App.Utilities;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace App.World.WorldGrid
{
    
    public class GridPathfinding 
    {
        private CellGrid cellGrid;
        private PQueue<Cell> openCells;
        private HashSet<Cell> closedCells;
        private List<AttributeResistance> attachedResistances;
        private bool canDraw = false;
        private const int BASIC_ATTRIBUTE_WEIGHT = 1;

        public GridPathfinding(CellGrid grid)
        {
            this.cellGrid = grid;
        }

        public Stack<Vector3> ProceedPathfinding(Vector3 startF, Vector3 endF, List<AttributeResistance> resistances)
        {
            if (cellGrid == null)
                return null;
            Debug.Log("Processing");
            cellGrid.ResetData();
            attachedResistances = new List<AttributeResistance>(resistances);
            openCells = new PQueue<Cell>();
            closedCells = new HashSet<Cell>();

            Vector3Int start = cellGrid.Tilemap.WorldToCell(startF);
            Vector3Int end = cellGrid.Tilemap.WorldToCell(endF);

            start -= cellGrid.StartPos;
            end -= cellGrid.StartPos;

            Cell firstOpen = cellGrid.GetCellAt(start.x, start.y);
            Cell finish = cellGrid.GetCellAt(end.x, end.y);


            firstOpen.H = ShortestDistance(firstOpen, finish);
            firstOpen.G = 0;

            openCells.Enqueue(firstOpen);

            while (!openCells.IsEmpty())
            {
                Cell cell = openCells.Dequeue();
                if (cell == finish)
                {
                    canDraw = true;
                    return BuildPath(cell);
                }
                closedCells.Add(cell);
                List<Cell> surroundingCells = SurroundingCells(cell, finish);
                foreach (Cell neighbour in surroundingCells)
                {
                    if (neighbour == null || closedCells.Contains(neighbour))
                    {
                        continue;
                    }
                    float newG = ShortestDistance(neighbour, cell) + cell.G;
                    
                    newG = AddWeightsFromCell(newG, neighbour);
                    if (newG < neighbour.G)
                    {

                        if (!openCells.Contains(neighbour))
                        {
                            neighbour.G = newG;
                            neighbour.ParentCell = cell;
                            neighbour.H = ShortestDistance(neighbour, finish);
                            openCells.Enqueue(neighbour);
                        }
                        else
                        {
                            //Debug.Log("dead");
                        }
                    }
                }
            }
            return null;
        }
        private float AddWeightsFromCell(float to, Cell neighbour)
        {
            foreach (DamageAttribute attribute in neighbour.Attributes.Keys)
            {
                float multiplier = 1f;
                foreach(AttributeResistance attributeResistance in attachedResistances)
                {
                    if (attributeResistance.attribute == attribute)
                        multiplier -= attributeResistance.resistance;
                }
                to += neighbour.Attributes[attribute] * BASIC_ATTRIBUTE_WEIGHT * multiplier;
            }
            return to;
        }
        private Stack<Vector3> BuildPath(Cell finish)
        {
            var path = new Stack<Vector3>();
            Cell current = finish;
            while (current.ParentCell != null)
            {
                Vector3 position = cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + cellGrid.StartPos.x, current.Y + cellGrid.StartPos.y, 0));
                position.x += cellGrid.Tilemap.cellSize.x * 0.5f;
                position.y += cellGrid.Tilemap.cellSize.y * 0.5f;
                path.Push(position);
                current = current.ParentCell;
            }
            return path;
        }
        private float ShortestDistance(Cell cell1, Cell cell2)
        {
            int distanceX = Mathf.Abs(cell1.X - cell2.X);
            int distanceY = Mathf.Abs(cell1.Y - cell2.Y);
            return Mathf.Abs(distanceX - distanceY) + 1.4f * Mathf.Min(distanceX, distanceY);
        }

        private List<Cell> SurroundingCells(Cell cell, Cell finish)
        {
            List<Cell> surroundingCells = new List<Cell>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    Cell neighbour = cellGrid.GetCellAt(cell.X + i, cell.Y + j);
                    if (neighbour == null || neighbour == cell)
                        continue;
                    
                    Tile tile = (Tile)cellGrid.Tilemap.GetComponentInChildren<Tilemap>().GetTile(new Vector3Int(cell.X + i + cellGrid.StartPos.x, cell.Y + j + cellGrid.StartPos.y));
                    if (tile != null && cellGrid.RestrictedTiles.Contains(tile))
                        continue;

                    if(i == j || i == -j)
                    {
                        if (!CanMoveDiagonally(cell, i, j))
                            continue;
                    }

                    surroundingCells.Add(neighbour);
                }
            }
            return surroundingCells;
        }

        private bool CanMoveDiagonally(Cell cell ,int i, int j)
        {
            Tile neighbour1 = (Tile)cellGrid.Tilemap.GetComponentInChildren<Tilemap>().GetTile(new Vector3Int(cell.X + i + cellGrid.StartPos.x, cell.Y + cellGrid.StartPos.y));
            Tile neighbour2 = (Tile)cellGrid.Tilemap.GetComponentInChildren<Tilemap>().GetTile(new Vector3Int(cell.X + cellGrid.StartPos.x, cell.Y + j + cellGrid.StartPos.y));
            if (neighbour1 != null && cellGrid.RestrictedTiles.Contains(neighbour1))
                return false;
            if (neighbour2 != null && cellGrid.RestrictedTiles.Contains(neighbour2))
                return false;
            return true;
        }
        //void OnDrawGizmos()
        //{
        //    if (!Application.isPlaying)
        //        return;
        //    if (canDraw)
        //    {
        //        foreach (Cell current in closedCells)
        //        {
        //            Vector2 position = cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + cellGrid.StartPos.x, current.Y + cellGrid.StartPos.y + 1, 5));
        //            Handles.Label(position, current.F.ToString());
        //        }
        //        for (int i = 0; i < openCells.Count; i++)
        //        {
        //            Cell current = openCells.container[i + 1];
        //            Vector2 position = cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + cellGrid.StartPos.x, current.Y + cellGrid.StartPos.y + 1, 5));
        //            Handles.Label(position, current.F.ToString());
        //        }
        //    }


        //}
    }

}
