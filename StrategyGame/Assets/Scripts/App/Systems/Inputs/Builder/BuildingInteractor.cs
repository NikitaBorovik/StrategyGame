using App.Systems.MoneySystem;
using App.World;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace App.Systems.Inputs.Builder
{
    public class BuildingInteractor : INotifyBuilt
    {
        private StateMachine stateMachne;
        private GameObject worldGrid;
        private Camera camera;
        private GameObject selectedCellBorder;
        private GameObject previewBuilding;
        private List<IToggleAttackRangeVision> buildingsWithAttackRange;
        private PlayerMoney playerMoney;

        private BuildingState buildingState;
        private DestroyingState destroyingState;
        private InputIdleState idleState;
        private UpgradingState upgradingState;

        public GameObject WorldGrid { get => worldGrid;}
        public Camera Camera { get => camera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
        public GameObject PreviewBuilding { get => previewBuilding;}
        public List<IToggleAttackRangeVision> BuildingsWithAttackRange { get => buildingsWithAttackRange; }
        public PlayerMoney PlayerMoney { get => playerMoney;}

        public event Action OnClick;
        public event Action OnMouseMoved;
        public event Action OnAltHold;
        public event Action OnBuilt;
        public BuildingInteractor(GameObject worldGrid, Camera camera, GameObject selectedCellBorder,GameObject previewBuilding,ObjectPool objectPool, PlayerMoney playerMoney)
        {
            this.worldGrid = worldGrid;
            this.camera = camera;
            this.selectedCellBorder = selectedCellBorder;
            this.previewBuilding = previewBuilding;
            this.playerMoney = playerMoney;

            buildingsWithAttackRange = new List<IToggleAttackRangeVision>();
            buildingState = new BuildingState(this,objectPool);
            destroyingState = new DestroyingState(this,objectPool);
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

        public void OnBuildingComplete()
        {
            OnBuilt?.Invoke();
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

        public void Subscribe(Action action)
        {
            OnBuilt += action;
        }

        public void Unsubscribe(Action action)
        {
            OnBuilt -= action;
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
