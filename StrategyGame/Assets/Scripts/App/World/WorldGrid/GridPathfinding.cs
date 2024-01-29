using App.Utilities;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GridPathfinding : MonoBehaviour
{
    private CellGrid cellGrid;
    private PQueue<Cell> openCells;
    private HashSet<Cell> closedCells;
    private bool canDraw = false;

    public void Init(CellGrid grid)
    {
        this.cellGrid = grid;
    }

    public Stack<Vector2> ProceedPathfinding(Vector2Int start, Vector2Int end)
    {
        openCells = new PQueue<Cell>();
        closedCells = new HashSet<Cell>();
        start -= cellGrid.StartPos; 
        end -= cellGrid.StartPos;
        
        Cell firstOpen = cellGrid.GetCellAt(start.x, start.y);
        Cell finish = cellGrid.GetCellAt(end.x, end.y);
        

        firstOpen.H = ShortestDistance(firstOpen, finish);
        firstOpen.G = 0;

        openCells.Enqueue(firstOpen);

        while(!openCells.IsEmpty())
        {
            //Debug.Log("----------");
            //for (int i = 0; i < openCells.Count; i++)
            //{

            //    Debug.Log(openCells.container[i + 1].F);

            //}
            //Debug.Log("----------");
            //Debug.Log(openCells);
            Cell cell = openCells.Dequeue();
            if(cell == finish) 
            {
                canDraw = true;
                return BuildPath(cell);
            }
            closedCells.Add(cell);
            List<Cell> surroundingCells = SurroundingCells(cell, finish);
            if(surroundingCells.Count != 8)
            {
                Debug.Log("ERROR");
            }
            foreach(Cell neighbour in surroundingCells)
            {
                if (neighbour == null || closedCells.Contains(neighbour))
                {
                    continue;
                }
                int newG = ShortestDistance(neighbour, cell) + cell.G;
                foreach (DamageAttribute attribute in neighbour.Attributes.Keys)
                {
                    newG += neighbour.Attributes[attribute] * 15; // TODO change
                }
                if(newG < neighbour.G)
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
                        Debug.Log("dead");
                    }
                }
            }
        }
        return null;
    }
    private Stack<Vector2> BuildPath(Cell finish)
    {
        var path = new Stack<Vector2>();
        Cell current = finish;
        while(current.ParentCell != null)
        {
            //Debug.Log(current.F);
            Vector2 position = cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + cellGrid.StartPos.x,current.Y + cellGrid.StartPos.y, 0));
            position.x += cellGrid.Tilemap.cellSize.x * 0.5f;
            position.y += cellGrid.Tilemap.cellSize.y * 0.5f;
            path.Push(position);
            current = current.ParentCell;
        }
        return path;
    } 
    private int ShortestDistance(Cell cell1, Cell cell2)
    {
        int distanceX = Mathf.Abs(cell1.X - cell2.X);
        int distanceY = Mathf.Abs(cell1.Y - cell2.Y);
        return 10 * Mathf.Abs(distanceX - distanceY) + 14 * Mathf.Min(distanceX, distanceY);
    }

    private List<Cell> SurroundingCells(Cell cell, Cell finish)
    {
        List<Cell> surroundingCells = new List<Cell>();
        for(int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                Cell neighbour = cellGrid.GetCellAt(cell.X + i, cell.Y + j);
                if(neighbour == null || neighbour == cell)
                {
                    continue;
                }

                surroundingCells.Add(neighbour);
            }
        }
        return surroundingCells;
    }
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        if (canDraw)
        {
            foreach (Cell current in closedCells)
            {
                Vector2 position = cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + cellGrid.StartPos.x, current.Y + cellGrid.StartPos.y + 1, 5));
                //position.x += cellGrid.Tilemap.cellSize.x * 0.5f;
                //position.y += cellGrid.Tilemap.cellSize.y * 0.5f;
                Handles.Label(position, current.F.ToString());
            }
            for(int i = 0; i < openCells.Count; i++)
            {
                Cell current = openCells.container[i + 1];
                Vector2 position = cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + cellGrid.StartPos.x, current.Y + cellGrid.StartPos.y + 1, 5));
                //position.x += cellGrid.Tilemap.cellSize.x * 0.5f;
                //position.y += cellGrid.Tilemap.cellSize.y * 0.5f;
                Handles.Label(position, current.F.ToString());
            }
        }
        
        
    }
}
