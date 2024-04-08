using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace App.World.WorldGrid
{
    public class CellGrid : MonoBehaviour
    {
        private Cell[,] grid;
        [SerializeField]
        private Vector3Int startPos;
        [SerializeField]
        private Vector3Int endPos;
        [SerializeField]
        private Grid tilemap;
        [SerializeField]
        private List<Tile> noBuildingTiles;
        [SerializeField]
        private List<Tile> noBuildingAndWalkingTiles;
        private int width;
        private int heigth;

        public Cell[,] Grid { get => grid; set => grid = value; }
        public Vector3Int StartPos { get => startPos; set => startPos = value; }
        public Vector3Int EndPos { get => endPos; set => endPos = value; }
        public Grid Tilemap { get => tilemap;}
        public List<Tile> NoBuildingTiles { get => noBuildingTiles;}
        public List<Tile> NoBuildingAndWalkingTiles { get => noBuildingAndWalkingTiles;}

        private void Start()
        {
            this.width = EndPos.x - StartPos.x;
            this.heigth = EndPos.y - StartPos.y;
            Grid = new Cell[width, heigth];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    Grid[i, j] = new Cell(i, j);
                }
            }
        }
        public void ResetData()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    Grid[i, j].ResetValues();
                }
            }
        }
        
        public Cell GetCellAt(int x, int y)
        {
            if(x < 0 || y < 0) return null;
            if (x >= width || y >= heigth) return null;
          
            return Grid[x, y];
        }
        public void AddAttributeToCells(Vector3 towerPos, float towerRange, DamageAttribute attribute)
        {
            Vector3 localPos = towerPos - startPos;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    int x = i + StartPos.x;
                    int y = j + StartPos.y;
                    if (Vector2.Distance(localPos, new Vector2(i + 0.5f,j + 0.5f)) < towerRange)
                    {
                        Grid[i, j].Attributes[attribute]++;
                    }
                }
            }
        }
        public void RemoveAttributeFromCells(Vector3 towerPos, float towerRange, DamageAttribute attribute)
        {
            Vector3 localPos = towerPos - startPos;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    if (Vector2.Distance(localPos, new Vector2(i + 0.5f, j + 0.5f)) <= towerRange)
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
                    if (Grid[i, j].Attributes[DamageAttribute.piercing] > 0)
                    {
                        //Debug.Log("Red");
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(new Vector3(x, y, 0), new Vector3(x + 1, y + 1, 0));
                    }
                    if (Grid[i, j].Attributes[DamageAttribute.magic] > 0)
                    {
                        //Debug.Log("Blue");
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(new Vector3(x, y + 1, 0), new Vector3(x + 1, y, 0));
                    }
                    if (Grid[i, j].Attributes[DamageAttribute.fortified] > 0)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(new Vector3(x , y + 0.5f, 0), new Vector3(x + 1, y + 0.5f, 0));
                    }

                }
            }
        }
    }
}
