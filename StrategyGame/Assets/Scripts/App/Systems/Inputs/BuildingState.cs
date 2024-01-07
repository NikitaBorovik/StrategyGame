using App;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class BuildingState : IState
    {
        private BuildingInteractor mouseProcessor;
        private GameObject building;
        private Grid grid;
        private GameObject selectedCellBorder;
        private bool canBuild = true;
        public BuildingState(BuildingInteractor buildingInteractor, GameObject building)
        {
            this.mouseProcessor = buildingInteractor;
            this.building = building;
            this.grid = buildingInteractor.WorldGrid;
            this.selectedCellBorder = buildingInteractor.SelectedCellBorder;
        }

        public void Enter()
        {
            selectedCellBorder.SetActive(true);
            mouseProcessor.OnClick += OnMouseClicked;
            mouseProcessor.OnMouseMoved += OnMouseMoved;
        }

        public void Exit()
        {
            selectedCellBorder.SetActive(false);
            mouseProcessor.OnClick -= OnMouseClicked;
            mouseProcessor.OnMouseMoved -= OnMouseMoved;
        }

        public void Update()
        {
        }

        private void OnMouseClicked()
        {
            if (canBuild)
            {
                Vector2 mousePosition = mouseProcessor.Camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = grid.CellToWorld(grid.WorldToCell(mousePosition));
                GameObject instantiatedBuilding = GameObject.Instantiate(building);
                instantiatedBuilding.transform.position = pos;
            }
            
        }
        private void OnMouseMoved()
        {
            if (selectedCellBorder.activeSelf)
            {
                Vector2 mousePosition = mouseProcessor.Camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = grid.CellToWorld(grid.WorldToCell(mousePosition));
                bool newCanBuild = !Physics2D.BoxCast(new Vector2(pos.x + grid.cellSize.x ,pos.y + grid.cellSize.y),new Vector2(1.5f, 1.5f), 0f, Vector2.zero); // TODO extend for different building sizes
                if(canBuild != newCanBuild)
                {
                    canBuild = newCanBuild;
                    selectedCellBorder.GetComponent<CellBorder>().IndcateCanBuild(canBuild);
                }
                selectedCellBorder.transform.position = pos;
            }
        }
    }
}
