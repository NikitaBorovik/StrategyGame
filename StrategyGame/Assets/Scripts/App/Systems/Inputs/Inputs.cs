using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class Inputs : MonoBehaviour
    {
        private BuildingInteractor processor;
        [SerializeField]
        private GameObject building;
        [SerializeField]
        private MouseOptionSelector mouseSelector;
        private MouseOption mouseOption;
        public void Init(Grid worldGrid, Camera camera, GameObject selectedCellBorder)
        {
            processor = new BuildingInteractor(worldGrid, camera, selectedCellBorder);
            mouseOption = mouseSelector.SelectedMouseOption;
        }
        void Update()
        {
            Debug.Log(mouseOption);
            ProceedMouseInput();
            if(mouseOption != mouseSelector.SelectedMouseOption)
            {
                ProceedMouseOption(mouseSelector.SelectedMouseOption);
            }
        }

        private void ProceedMouseInput()
        {
            if(processor != null)
            processor.OnMouseMove();
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                processor.OnLeftButton();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                mouseSelector.SelectedMouseOption = MouseOption.Idle;
            }
        }

        private void ProceedMouseOption(MouseOption option)
        {
            mouseOption = mouseSelector.SelectedMouseOption;
            switch (option)
            {
                case MouseOption.Building:
                    processor.BuildingState(mouseSelector.SelectedBuilding);
                    break;
                case MouseOption.Upgrading:
                    //TODO Implement
                    break;
                case MouseOption.Destroying:
                    //TODO Implement
                    break;
                case MouseOption.Idle:
                    processor.IdleState();
                    break;
                default: 
                    break;
            }
        }
    }
}

