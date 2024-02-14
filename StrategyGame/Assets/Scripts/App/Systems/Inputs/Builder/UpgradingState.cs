using App;
using App.Systems.Inputs.Builder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradingState : IState
{
    private BuildingInteractor buildingInteractor;
    private Grid grid;
    private GameObject selectedCellBorder;
    private GameObject selectedBuilding;
    RaycastHit2D raycastHit;

    public UpgradingState(BuildingInteractor buildingInteractor)
    {
        this.buildingInteractor = buildingInteractor;
        this.grid = buildingInteractor.WorldGrid.GetComponent<Grid>();
        this.selectedCellBorder = buildingInteractor.SelectedCellBorder;
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
        if (selectedBuilding != null)
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
    }

    public void Update()
    {
    }

    private void OnMouseClicked()
    {
        if (selectedBuilding != null)
        {
            var buildingScript = selectedBuilding.GetComponent<Building>();
            if(buildingScript.Level < 2)
            {
                buildingScript.Upgrade();
            }
            else
            {
                buildingScript.Repair();
            }
                
        }
    }
    private void OnMouseMoved()
    {
        Vector2 mousePosition = buildingInteractor.Camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = grid.CellToWorld(grid.WorldToCell(mousePosition));
        raycastHit = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), Vector2.zero, 1f, LayerMask.GetMask("Building"));
        selectedCellBorder.transform.position = pos;

        if (raycastHit.collider == null)
        {
            if (selectedBuilding != null)
            {
                selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                selectedBuilding.GetComponent<Building>().Clickable = true;
            }
                
            selectedBuilding = null;
            return;
        }

        if (selectedBuilding != raycastHit.collider.gameObject)
        {
            if (selectedBuilding != null)
                selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
            selectedBuilding = raycastHit.collider.gameObject;
            selectedBuilding.GetComponent<Building>().Clickable = false;
        }

        if (selectedBuilding == null)
            return;

        if (selectedBuilding.GetComponent<Building>().Level < 2)
        {
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 0.5f);
        }
        else
        {
            selectedBuilding.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255, 0.5f);
        }
    }
}
