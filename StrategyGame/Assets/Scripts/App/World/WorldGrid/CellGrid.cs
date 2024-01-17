using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace App.World.WorldGrid
{
    public class CellGrid : MonoBehaviour
    {
        private Cell[,] grid;
        [SerializeField]
        private Vector2Int startPos;
        [SerializeField]
        private Vector2Int endPos;
        private int width;
        private int heigth;

        public Cell[,] Grid { get => grid; set => grid = value; }
        public Vector2Int StartPos { get => startPos; set => startPos = value; }
        public Vector2Int EndPos { get => endPos; set => endPos = value; }

        private void Start()
        {
            this.width = EndPos.x - StartPos.x;
            this.heigth = EndPos.y - StartPos.y;
            Grid = new Cell[width, heigth];
            for (int i = 0; i < width; i++)
            {
                for(int j = 0; j < heigth; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                }
            }
        }

        public void AddAttributeToCells(Vector2 towerPos, float towerRange, CellAttachedAttribute attribute)
        {
            Debug.Log("Adding");
            Vector2 localPos = towerPos - startPos;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    int x = i + StartPos.x;
                    int y = j + StartPos.y;
                    Debug.Log(x);
                    Debug.Log(y);
                    if (Vector2.Distance(localPos, new Vector2(i + 0.5f,j + 0.5f)) < towerRange)
                    {
                        Grid[i, j].Attributes[attribute]++;
                    }
                }
            }
        }
        public void RemoveAttributeFromCells(Vector2Int towerPos, float towerRange, CellAttachedAttribute attribute)
        {
            Vector2Int localPos = towerPos - startPos;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    if (Vector2Int.Distance(localPos, new Vector2Int(i, j)) >= towerRange)
                    {
                        if (Grid[i, j].Attributes[attribute] > 0)
                            Grid[i, j].Attributes[attribute]--;
                    }
                }
            }
        }
        private void OnDrawGizmos()
        {

            if (!Application.isPlaying)
                return;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    int x = i + StartPos.x;
                    int y = j + StartPos.y;
                    if (Grid[i, j].Attributes[CellAttachedAttribute.piercing] > 0)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(new Vector3(x, y, 0), new Vector3(x + 1, y + 1, 0));
                    }
                    if (Grid[i, j].Attributes[CellAttachedAttribute.magic] > 0)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(new Vector3(x, y + 1, 0), new Vector3(x + 1, y, 0));
                    }
                }
            }
        }
    }
}
