using App;
using App.World;
using App.World.Buildings.PlaceableBuildings;
using App.World.Buildings.PlaceableBuildings.BuildingsSO;
using App.World.Buildings.PlaceableBuildings.Towers;
using App.World.WorldGrid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace App.Systems.Inputs.Builder
{
    public class BuildingState : IState
    {
        private BuildingSystem buildingSystem;
        private GameObject building;
        private BuildingData buildingData;
        private GameObject worldGrid;
        private Grid tilemap;
        private CellGrid cellGrid;
        private GameObject selectedCellBorder;
        private GameObject previewBuilding;
        private bool canBuild = true;
        private ObjectPool objectPool;
        public BuildingState(BuildingSystem buildingInteractor, ObjectPool objectPool)
        {
            this.buildingSystem = buildingInteractor;
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
            buildingData = building.GetComponent<PlaceableBuilding>().BasicData;
            selectedCellBorder.SetActive(true);
            previewBuilding.SetActive(true);
            SpriteRenderer spriteRenderer = previewBuilding.GetComponent<SpriteRenderer>();
            Debug.Log(buildingData.sprite);
            spriteRenderer.sprite = buildingData.sprite;
            spriteRenderer.color = new Color(255, 255, 255, 0.5f);
            buildingSystem.OnClick += OnMouseClicked;
            buildingSystem.OnMouseMoved += OnMouseMoved;
        }

        public void Exit()
        {
            selectedCellBorder.SetActive(false);
            previewBuilding.SetActive(false);
            buildingSystem.OnClick -= OnMouseClicked;
            buildingSystem.OnMouseMoved -= OnMouseMoved;
        }

        public void Update()
        {
        }

        private void OnMouseClicked()
        {
            if(buildingSystem.PlayerMoney.Money < buildingData.price)
            {
                buildingSystem.AudioSource.PlayOneShot(buildingSystem.WrongActionSound);
                return;
            }
            if (canBuild)
            {
                Vector2 mousePosition = buildingSystem.Camera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pos = tilemap.CellToWorld(tilemap.WorldToCell(mousePosition));

                PlaceableBuilding toBuild = objectPool.GetObjectFromPool(building.GetComponent<PlaceableBuilding>().PoolObjectID, building).GetGameObject().GetComponent<PlaceableBuilding>();

                toBuild.Init(pos, cellGrid, buildingSystem.PlayerMoney);
                toBuild.notifyGridWeightChanged += buildingSystem.OnBuildingComplete;
                buildingSystem.PlayerMoney.Money -= toBuild.BasicData.price;
                buildingSystem.OnBuildingComplete();

                var interfaceBuilding = toBuild as IToggleAttackRangeVision;
                if (interfaceBuilding != null)
                {
                    buildingSystem.BuildingsWithAttackRange.Add(interfaceBuilding);
                }

                buildingSystem.AudioSource.PlayOneShot(buildingSystem.BuildSound);
            }
            
        }
        private void OnMouseMoved()
        {

            Vector2 mousePosition = buildingSystem.Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = tilemap.CellToWorld(tilemap.WorldToCell(mousePosition));
            bool newCanBuild = true;
            

            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(pos.x + tilemap.cellSize.x * 0.5f * buildingData.size, pos.y + tilemap.cellSize.y * 0.5f * buildingData.size), 
                new Vector2(buildingData.size / 2, buildingData.size / 2), 0f, LayerMask.GetMask(new string[]{"Building","MainCastle"}));
            if (colliders.Length != 0)
            {
                newCanBuild = false;
            }
            if (OverlapRestrictedTiles(new Vector3Int((int)pos.x, (int)pos.y)))
            {
                newCanBuild = false;
            }
            if (canBuild != newCanBuild)
            {
                canBuild = newCanBuild;
                selectedCellBorder.GetComponent<CellBorder>().IndcateCanBuild(canBuild);
            }
            selectedCellBorder.transform.position = pos;
            previewBuilding.transform.position = pos;
        }

        private bool OverlapRestrictedTiles(Vector3Int leftBottomPos)
        {
            for(int i = 0; i < buildingData.size; i++)
            {
                for(int j = 0; j < buildingData.size; j++)
                {
                    Tile tile = (Tile)cellGrid.Tilemap.GetComponentInChildren<Tilemap>().GetTile(new Vector3Int(leftBottomPos.x + j, leftBottomPos.y + i));
                    if (tile == null)
                    {
                        Debug.Log("Unexistent tile");
                        return true;
                    }
                    if (cellGrid.NoBuildingTiles.Contains(tile) || cellGrid.NoBuildingAndWalkingTiles.Contains(tile))
                        return true;
                }
            }
            return false;
        }
    }
}
