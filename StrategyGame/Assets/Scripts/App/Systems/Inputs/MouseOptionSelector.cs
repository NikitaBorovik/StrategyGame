using App.World.Buildings.BuildingsSO;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class MouseOptionSelector : MonoBehaviour
    {
        private BuildingKindSO selectedBuilding;
        private MouseOption selectedMouseOption = MouseOption.Idle;

        public BuildingKindSO SelectedBuilding { get => selectedBuilding; set => selectedBuilding = value; }
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
