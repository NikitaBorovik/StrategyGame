using App;
using App.World.Buildings.BuildingsSO;
using App.World.WorldGrid;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Systems.Inputs.Builder
{
    public class BuildingState : IState
    {
        private BuildingInteractor buildingInteractor;
        private BuildingKindSO building;
        private GameObject worldGrid;
        private Grid tilemap;
        private CellGrid cellGrid;
        private GameObject selectedCellBorder;
        private GameObject previewBuilding;
        private bool canBuild = true;
        public BuildingState(BuildingInteractor buildingInteractor)
        {
            this.buildingInteractor = buildingInteractor;
            this.worldGrid = buildingInteractor.WorldGrid;
            this.selectedCellBorder = buildingInteractor.SelectedCellBorder;
            this.previewBuilding = buildingInteractor.PreviewBuilding;
            this.tilemap = worldGrid.GetComponent<Grid>();
            this.cellGrid = worldGrid.GetComponent<CellGrid>();
        }

        public BuildingKindSO Building { get => building; set => building = value; }

        public void Enter()
        {
            selectedCellBorder.SetActive(true);
            previewBuilding.SetActive(true);
            SpriteRenderer spriteRenderer = previewBuilding.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = building.sprite;
            spriteRenderer.color = new Color(255, 255, 255, 0.5f);
            buildingInteractor.OnClick += OnMouseClicked;
            buildingInteractor.OnMouseMoved += OnMouseMoved;
        }

        public void Exit()
        {
            selectedCellBorder.SetActive(false);
            previewBuilding.SetActive(false);
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
                Vector3 pos = tilemap.CellToWorld(tilemap.WorldToCell(mousePosition));
                GameObject instantiatedBuilding = GameObject.Instantiate(Building.prefab);
                instantiatedBuilding.transform.position = pos;
                cellGrid.AddAttributeToCells(new Vector2((pos.x + 0.5f * building.size), (pos.y + 0.5f * building.size) ), building.attributeRange, building.attribute);
            }
            
        }
        private void OnMouseMoved()
        {

            Vector2 mousePosition = buildingInteractor.Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = tilemap.CellToWorld(tilemap.WorldToCell(mousePosition));
            bool newCanBuild = !Physics2D.BoxCast(new Vector2(pos.x + tilemap.cellSize.x * 0.5f * building.size, pos.y + tilemap.cellSize.y * 0.5f * building.size), new Vector2(building.size / 2, building.size / 2), 0f, Vector2.zero, LayerMask.GetMask("Building"));

            if (canBuild != newCanBuild)
            {
                canBuild = newCanBuild;
                selectedCellBorder.GetComponent<CellBorder>().IndcateCanBuild(canBuild);
            }
            selectedCellBorder.transform.position = pos;
            previewBuilding.transform.position = pos;
        }
    }
}
