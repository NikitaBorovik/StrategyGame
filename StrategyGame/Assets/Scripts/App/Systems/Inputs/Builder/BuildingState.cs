using App;
using App.World;
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
        private GameObject building;
        private BuildingData buildingData;
        private GameObject worldGrid;
        private Grid tilemap;
        private CellGrid cellGrid;
        private GameObject selectedCellBorder;
        private GameObject previewBuilding;
        private bool canBuild = true;
        private ObjectPool objectPool;
        public BuildingState(BuildingInteractor buildingInteractor, ObjectPool objectPool)
        {
            this.buildingInteractor = buildingInteractor;
            this.worldGrid = buildingInteractor.WorldGrid;
            this.selectedCellBorder = buildingInteractor.SelectedCellBorder;
            this.previewBuilding = buildingInteractor.PreviewBuilding;
            this.objectPool = objectPool;
            this.tilemap = worldGrid.GetComponent<Grid>();
            this.cellGrid = worldGrid.GetComponent<CellGrid>();
        }

        public GameObject Building { get => building; set => building = value; }

        public void Enter()
        {
            buildingData = building.GetComponent<Building>().BasicData;
            selectedCellBorder.SetActive(true);
            previewBuilding.SetActive(true);
            SpriteRenderer spriteRenderer = previewBuilding.GetComponent<SpriteRenderer>();
            Debug.Log(buildingData.sprite);
            spriteRenderer.sprite = buildingData.sprite;
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
            if(buildingInteractor.PlayerMoney.Money < buildingData.price)
            {
                //TODO PLAY SOUND
                return;
            }
            if (canBuild)
            {
                Vector2 mousePosition = buildingInteractor.Camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = tilemap.CellToWorld(tilemap.WorldToCell(mousePosition));

                GameObject toBuild = objectPool.GetObjectFromPool(building.GetComponent<Building>().PoolObjectID, building, pos).GetGameObject();
                
                var buildingScript = toBuild.GetComponent<Building>();
                toBuild.GetComponent<Building>().Init(pos, cellGrid, buildingInteractor.PlayerMoney);

                buildingInteractor.PlayerMoney.Money -= buildingScript.BasicData.price;

                var interfaceBuilding = buildingScript as IToggleAttackRangeVision;
                if (interfaceBuilding != null)
                {
                    buildingInteractor.BuildingsWithAttackRange.Add(interfaceBuilding);
                }
            }
            
        }
        private void OnMouseMoved()
        {

            Vector2 mousePosition = buildingInteractor.Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = tilemap.CellToWorld(tilemap.WorldToCell(mousePosition));
            bool newCanBuild = true;
            

            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(pos.x + tilemap.cellSize.x * 0.5f * buildingData.size, pos.y + tilemap.cellSize.y * 0.5f * buildingData.size), new Vector2(buildingData.size / 2, buildingData.size / 2), 0f, LayerMask.GetMask("Building"));
            foreach(Collider2D collider in colliders)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Building"))
                {
                    newCanBuild = false;
                    break;
                }
            }

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
