using App.Systems.MoneySystem;
using App.World;
using App.World.Buildings.PlaceableBuildings.Towers;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace App.Systems.Inputs.Builder
{
    public class BuildingSystem : MonoBehaviour, INotifyBuilt
    {
        private StateMachine stateMachne;
        private GameObject worldGrid;
        private Camera mainCamera;
        private GameObject selectedCellBorder;
        private GameObject previewBuilding;
        private List<IToggleAttackRangeVision> buildingsWithAttackRange;
        private PlayerMoney playerMoney;

        private BuildingState buildingState;
        private DestroyingState destroyingState;
        private InputIdleState idleState;
        private UpgradingState upgradingState;
        private bool altButtonPressed = false;

        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip buildSound;
        [SerializeField]
        private AudioClip wrongActionSound;

        public GameObject WorldGrid { get => worldGrid;}
        public Camera Camera { get => mainCamera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
        public GameObject PreviewBuilding { get => previewBuilding;}
        public PlayerMoney PlayerMoney { get => playerMoney;}
        public AudioClip BuildSound { get => buildSound; set => buildSound = value; }
        public AudioClip WrongActionSound { get => wrongActionSound;}
        public AudioSource AudioSource { get => audioSource;}

        public Action OnClick;
        public Action OnMouseMoved;
        public Action OnAltHold;
        public Action OnBuilt;
        public void Init(GameObject worldGrid, Camera camera, GameObject selectedCellBorder,GameObject previewBuilding,ObjectPool objectPool, PlayerMoney playerMoney)
        {
            this.worldGrid = worldGrid;
            this.mainCamera = camera;
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

        public void AddToBuildingsWithAttackRange(IToggleAttackRangeVision building)
        {
            buildingsWithAttackRange.Add(building);
            building.SetAttackRangeVision(altButtonPressed);
        }
        public void RemoveFromBuildingsWithAttackRange(IToggleAttackRangeVision building)
        {
            buildingsWithAttackRange.Remove(building);
            building.SetAttackRangeVision(false);
        }

        private void ToggleTowerRangeVision()
        {
            altButtonPressed = !altButtonPressed;
            foreach(IToggleAttackRangeVision building in buildingsWithAttackRange)
            {
                building.SetAttackRangeVision(altButtonPressed);
            }
        }


    }
}
