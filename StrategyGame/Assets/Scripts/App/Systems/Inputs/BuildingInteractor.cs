using App;
using App.World.Buildings.BuildingsSO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace App.Systems.Inputs
{
    public class BuildingInteractor
    {
        private StateMachine stateMachne;
        private Grid worldGrid;
        private Camera camera;
        private GameObject selectedCellBorder;

        private BuildingState buildingState;
        private IdleState idleState;

        public Grid WorldGrid { get => worldGrid;}
        public Camera Camera { get => camera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}

        public event Action OnClick;
        public event Action OnMouseMoved;
        public BuildingInteractor(Grid worldGrid, Camera camera, GameObject selectedCellBorder)
        {
            this.worldGrid = worldGrid;
            this.camera = camera;
            this.selectedCellBorder = selectedCellBorder;
            buildingState = new BuildingState(this);
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

        public void IdleState()
        {
            stateMachne.ChangeState(idleState);
        }
    }
}
