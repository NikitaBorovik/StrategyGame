using App;
using App.Systems.Inputs.Builder;
using App.World;
using App.World.Buildings.PlaceableBuildings;
using App.World.Buildings.PlaceableBuildings.Towers;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DestroyingState : IState
{
    private BuildingInteractor buildingInteractor;
    private Grid grid;
    private GameObject selectedCellBorder;
    private GameObject selectedBuilding;
    private ObjectPool objectPool;
    RaycastHit2D raycastHit;
    public DestroyingState(BuildingInteractor buildingInteractor, ObjectPool objectPool)
    {
        this.buildingInteractor = buildingInteractor;
        this.grid = buildingInteractor.WorldGrid.GetComponent<Grid>();
        this.selectedCellBorder = buildingInteractor.SelectedCellBorder;
        this.objectPool = objectPool;
    }

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
        if(selectedBuilding != null)
        {
            var interfaceBuilding = selectedBuilding.GetComponent<PlaceableBuilding>() as IToggleAttackRangeVision;

            if (interfaceBuilding != null)
                buildingInteractor.BuildingsWithAttackRange.Remove(interfaceBuilding);
            var build = selectedBuilding.GetComponent<PlaceableBuilding>();
            buildingInteractor.PlayerMoney.Money += (int)(build.BasicData.price * build.HealthComponent.CurHP / build.HealthComponent.MaxHP);
            objectPool.ReturnToPool(selectedBuilding.GetComponent<PlaceableBuilding>());
            build.notifyGridWeightChanged -= buildingInteractor.OnBuildingComplete;
        }
    }
    private void OnMouseMoved() 
    {
        Vector2 mousePosition = buildingInteractor.Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = grid.CellToWorld(grid.WorldToCell(mousePosition));
        raycastHit = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), Vector2.zero,1f,LayerMask.GetMask("Building"));
        selectedCellBorder.transform.position = pos;

        if (raycastHit.collider == null)
        {
            if (selectedBuilding != null)
            {
                selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                selectedBuilding.GetComponent<PlaceableBuilding>().Clickable = true;
            }
                
            selectedBuilding = null;
            return;
        }

        if(selectedBuilding != raycastHit.collider.gameObject)
        {
            if(selectedBuilding != null) 
                selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255,255,255,1f);
            selectedBuilding = raycastHit.collider.gameObject;
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.5f);
            selectedBuilding.GetComponent<PlaceableBuilding>().Clickable = false;
        }
        
    }
}
