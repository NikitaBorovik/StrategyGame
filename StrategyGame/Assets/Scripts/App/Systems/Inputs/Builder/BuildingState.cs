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
            buildingData = building.GetComponent<PlaceableBuilding>().BasicData;
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

                PlaceableBuilding toBuild = objectPool.GetObjectFromPool(building.GetComponent<PlaceableBuilding>().PoolObjectID, building).GetGameObject().GetComponent<PlaceableBuilding>();
                
                toBuild.Init(pos, cellGrid, buildingInteractor.PlayerMoney);
                toBuild.notifyGridWeightChanged += buildingInteractor.OnBuildingComplete;
                buildingInteractor.PlayerMoney.Money -= toBuild.BasicData.price;
                buildingInteractor.OnBuildingComplete();

                var interfaceBuilding = toBuild as IToggleAttackRangeVision;
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
                    if (cellGrid.RestrictedTiles.Contains(tile))
                        return true;
                }
            }
            return false;
        }
    }
}
