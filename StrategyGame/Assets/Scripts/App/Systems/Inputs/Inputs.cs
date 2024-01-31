using App.Systems.Inputs.Builder;
using App.World.WorldGrid;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Systems.Inputs
{
    public class Inputs : MonoBehaviour, IMouseOptionHandler
    {
        private BuildingInteractor processor;
        [SerializeField]
        private MouseOptionSelector mouseSelector;
        [SerializeField]
        private GameObject waypoint;
        private GridPathfinding pathfinding;

        ////TMP TEST
        //private Vector2 pos1;
        //private Vector2 pos2;
        //private GameObject worldGrid;

        public void Init(GameObject worldGrid, Camera camera, GameObject selectedCellBorder, GameObject previewBuilding)
        {
            processor = new BuildingInteractor(worldGrid, camera, selectedCellBorder,previewBuilding);
            mouseSelector.MouseInputHandler = this;
            //pathfinding = GetComponent<GridPathfinding>();
            //pathfinding.Init(worldGrid.GetComponent<CellGrid>());
        }
        void Update()
        {
            ProceedMouseInputPress();
            ProcessKeyboardInput();
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    Grid grid = worldGrid.GetComponent<Grid>();
            //    pos1 = grid.CellToWorld(grid.WorldToCell(mousePosition));
            //    GameObject instantiatedPoint = GameObject.Instantiate(waypoint);
            //    instantiatedPoint.transform.position = new Vector3(pos1.x + 0.5f, pos1.y + 0.5f);
            //}
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    Grid grid = worldGrid.GetComponent<Grid>();
            //    pos2 = grid.CellToWorld(grid.WorldToCell(mousePosition));
            //    GameObject instantiatedPoint = GameObject.Instantiate(waypoint);
            //    instantiatedPoint.transform.position = new Vector3(pos2.x + 0.5f, pos2.y + 0.5f);
            //    if (pos1 != null)
            //    {
            //        Stack<Vector2> st = pathfinding.ProceedPathfinding(new Vector2Int((int)pos1.x, (int)pos1.y), new Vector2Int((int)pos2.x, (int)pos2.y));
            //        //Debug.Log(st);
            //        while (st.Count > 0) 
            //        {
            //            GameObject go = GameObject.Instantiate(waypoint);
            //            go.transform.position = st.Pop();
            //        }
            //    }
            //}

        }

        private void ProceedMouseInputPress()
        {
            processor.OnMouseMove();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                processor.OnLeftButton();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                mouseSelector.SelectedMouseOption = MouseOption.Idle;
                ProceedMouseOption(mouseSelector.SelectedMouseOption);
            }
        }

        private void ProcessKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Debug.Log("Alt");
                processor.OnAltButtonHold();
            }

        }

        public void ProceedMouseOption(MouseOption option)
        {
            switch (option)
            {
                case MouseOption.Building:
                    processor.BuildingState(mouseSelector.SelectedBuilding);
                    break;
                case MouseOption.Upgrading:
                    processor.UpgradingState();
                    break;
                case MouseOption.Destroying:
                    processor.DestroyingState();
                    break;
                case MouseOption.Idle:
                    processor.IdleState();
                    break;
                default: 
                    break;
            }
        }

        

    }
}

