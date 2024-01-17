using App.Systems.Inputs.Builder;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class Inputs : MonoBehaviour, IMouseOptionHandler
    {
        private BuildingInteractor processor;
        [SerializeField]
        private GameObject building;
        [SerializeField]
        private MouseOptionSelector mouseSelector;

        public void Init(GameObject worldGrid, Camera camera, GameObject selectedCellBorder, GameObject previewBuilding)
        {
            processor = new BuildingInteractor(worldGrid, camera, selectedCellBorder,previewBuilding);
            mouseSelector.MouseInputHandler = this;
        }
        void Update()
        {
            ProceedMouseInput();
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
                ProceedMouseOption(mouseSelector.SelectedMouseOption);
            }
        }

        public void ProceedMouseOption(MouseOption option)
        {
            switch (option)
            {
                case MouseOption.Building:
                    processor.BuildingState(mouseSelector.SelectedBuilding);
                    break;
                case MouseOption.Upgrading:
                    //TODO Implement
                    break;
                case MouseOption.Destroying:
                    processor.DestroyingState();
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

