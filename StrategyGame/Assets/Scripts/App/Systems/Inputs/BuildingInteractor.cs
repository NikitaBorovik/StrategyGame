using App;
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

        public Grid WorldGrid { get => worldGrid;}
        public Camera Camera { get => camera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}

        public event Action OnClick;
        public event Action OnMouseMoved;
        public BuildingInteractor(Grid worldGrid, Camera camera, GameObject building, GameObject selectedCellBorder)
        {
            this.worldGrid = worldGrid;
            this.camera = camera;
            this.selectedCellBorder = selectedCellBorder;
            BuildingState buildingState = new BuildingState(this, building);
            stateMachne = new StateMachine();
            stateMachne.Init(buildingState);
        }
        public void OnLeftButton()
        {
            OnClick?.Invoke();
        }

        public void OnMouseMove()
        {
            OnMouseMoved?.Invoke();
        }
    }
}
