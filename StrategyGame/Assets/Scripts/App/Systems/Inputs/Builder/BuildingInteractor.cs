using App;
using App.World.Buildings.BuildingsSO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace App.Systems.Inputs.Builder
{
    public class BuildingInteractor
    {
        private StateMachine stateMachne;
        private GameObject worldGrid;
        private Camera camera;
        private GameObject selectedCellBorder;
        private GameObject previewBuilding;
        private List<IToggleAttackRangeVision> buildingsWithAttackRange;

        private BuildingState buildingState;
        private DestroyingState destroyingState;
        private InputIdleState idleState;
        private UpgradingState upgradingState;

        public GameObject WorldGrid { get => worldGrid;}
        public Camera Camera { get => camera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
        public GameObject PreviewBuilding { get => previewBuilding;}
        public List<IToggleAttackRangeVision> BuildingsWithAttackRange { get => buildingsWithAttackRange; }

        public event Action OnClick;
        public event Action OnMouseMoved;
        public event Action OnAltHold;
        public BuildingInteractor(GameObject worldGrid, Camera camera, GameObject selectedCellBorder,GameObject previewBuilding)
        {
            this.worldGrid = worldGrid;
            this.camera = camera;
            this.selectedCellBorder = selectedCellBorder;
            this.previewBuilding = previewBuilding;

            buildingsWithAttackRange = new List<IToggleAttackRangeVision>();
            buildingState = new BuildingState(this);
            destroyingState = new DestroyingState(this);
            idleState = new InputIdleState();
            upgradingState = new UpgradingState(this);
            stateMachne = new StateMachine();
            stateMachne.Init(idleState);

            OnAltHold += ToggleTowerRangeVision;
        }
        public void OnLeftButton()
        {
            OnClick?.Invoke();
        }

        public void OnMouseMove()
        {
            OnMouseMoved?.Invoke();
        }

        public void OnAltButtonHold()
        {
            OnAltHold?.Invoke();
        }

        public void BuildingState(GameObject building)
        {
            buildingState.Building = building;
            stateMachne.ChangeState(buildingState);
        }

        public void DestroyingState()
        {
            stateMachne.ChangeState(destroyingState);
        }

        public void UpgradingState()
        {
            stateMachne.ChangeState(upgradingState);
        }

        public void IdleState()
        {
            stateMachne.ChangeState(idleState);
        }

        private void ToggleTowerRangeVision()
        {
            foreach(IToggleAttackRangeVision building in BuildingsWithAttackRange)
            {
                building.ToggleAttackRangeVision();
            }
        }
    }
}
