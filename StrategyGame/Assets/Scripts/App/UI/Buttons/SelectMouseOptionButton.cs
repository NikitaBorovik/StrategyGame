using App.Systems.Inputs;
using UnityEngine;

namespace App.UI.Buttons
{
    public class SelectMouseOptionButton : MonoBehaviour
    {
        [SerializeField]
        private MouseOptionSelector optionSelector;
        [SerializeField]
        private GameObject building;
        [SerializeField]
        private MouseOption option;

        public void SelectOption()
        {
            optionSelector.SetParameters(option,building);
        }
    }
}

