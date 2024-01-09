using App;
using App.World.Buildings.BuildingsSO;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class BuildingState : IState
    {
        private BuildingInteractor buildingInteractor;
        private BuildingKindSO building;
        private Grid grid;
        private GameObject selectedCellBorder;
        private bool canBuild = true;
        public BuildingState(BuildingInteractor buildingInteractor)
        {
            this.buildingInteractor = buildingInteractor;
            this.grid = buildingInteractor.WorldGrid;
            this.selectedCellBorder = buildingInteractor.SelectedCellBorder;
        }

        public BuildingKindSO Building { get => building; set => building = value; }

        public void Enter()
        {
            selectedCellBorder.SetActive(true);
            buildingInteractor.OnClick += OnMouseClicked;
            buildingInteractor.OnMouseMoved += OnMouseMoved;
        }

        public void Exit()
        {
            selectedCellBorder.SetActive(false);
            buildingInteractor.OnClick -= OnMouseClicked;
            buildingInteractor.OnMouseMoved -= OnMouseMoved;
        }

        public void Update()
        {
        }

        private void OnMouseClicked()
        {
            if (canBuild)
            {
                Vector2 mousePosition = buildingInteractor.Camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = grid.CellToWorld(grid.WorldToCell(mousePosition));
                GameObject instantiatedBuilding = GameObject.Instantiate(Building.prefab);
                instantiatedBuilding.transform.position = pos;
            }
            
        }
        private void OnMouseMoved()
        {
            if (selectedCellBorder.activeSelf)
            {
                Vector2 mousePosition = buildingInteractor.Camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = grid.CellToWorld(grid.WorldToCell(mousePosition));
                bool newCanBuild = true;
                if(building.size == 2) // TODO implement normally!!!
                {
                   newCanBuild = !Physics2D.BoxCast(new Vector2(pos.x + grid.cellSize.x, pos.y + grid.cellSize.y), new Vector2(1.5f, 1.5f), 0f, Vector2.zero);
                }
                else if(building.size == 3)
                {
                   newCanBuild = !Physics2D.BoxCast(new Vector2(pos.x + grid.cellSize.x * 1.5f, pos.y + grid.cellSize.y * 1.5f), new Vector2(2.5f, 2.5f), 0f, Vector2.zero);
                }
                 
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
