using App.World.Buildings.BuildingsSO;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class MouseOptionSelector : MonoBehaviour
    {
        private GameObject selectedBuilding;
        private MouseOption selectedMouseOption = MouseOption.Idle;
        private IMouseOptionHandler mouseInputHandler;
        public void SetParameters(MouseOption option, GameObject building)
        {
            this.SelectedMouseOption = option;
            this.SelectedBuilding = building;
            MouseInputHandler?.ProceedMouseOption(SelectedMouseOption);
        }

        public IMouseOptionHandler MouseInputHandler { get => mouseInputHandler; set => mouseInputHandler = value; }
        public GameObject SelectedBuilding { get => selectedBuilding; set => selectedBuilding = value; }
        public MouseOption SelectedMouseOption { get => selectedMouseOption; set => selectedMouseOption = value; }
    }
    public enum MouseOption
    {
        Building,
        Upgrading,
        Destroying,
        Idle,
    }
}
