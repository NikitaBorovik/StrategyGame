using App.Systems.Inputs;
using App.World.Buildings.BuildingsSO;
using UnityEngine;

namespace App.UI.Buttons
{
    public class SelectMouseOptionButton : MonoBehaviour
    {
        [SerializeField]
        private MouseOptionSelector optionSelector;
        [SerializeField]
        private BuildingKindSO building;
        [SerializeField]
        private MouseOption option;

        public void SelectOption()
        {
            optionSelector.SetParameters(option,building);
        }
    }
}

