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

        private BuildingState buildingState;
        private DestroyingState destroyingState;
        private IdleState idleState;

        public GameObject WorldGrid { get => worldGrid;}
        public Camera Camera { get => camera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
        public GameObject PreviewBuilding { get => previewBuilding;}

        public event Action OnClick;
        public event Action OnMouseMoved;
        public BuildingInteractor(GameObject worldGrid, Camera camera, GameObject selectedCellBorder,GameObject previewBuilding)
        {
            this.worldGrid = worldGrid;
            this.camera = camera;
            this.selectedCellBorder = selectedCellBorder;
            this.previewBuilding = previewBuilding;
            buildingState = new BuildingState(this);
            destroyingState = new DestroyingState(this);
            idleState = new IdleState();
            stateMachne = new StateMachine();
            stateMachne.Init(idleState);
        }
        public void OnLeftButton()
        {
            OnClick?.Invoke();
        }

        public void OnMouseMove()
        {
            OnMouseMoved?.Invoke();
        }

        public void BuildingState(BuildingKindSO building)
        {
            buildingState.Building = building;
            stateMachne.ChangeState(buildingState);
        }

        public void DestroyingState()
        {
            stateMachne.ChangeState(destroyingState);
        }

        public void IdleState()
        {
            stateMachne.ChangeState(idleState);
        }
    }
}
