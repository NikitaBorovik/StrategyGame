using App.Systems.Inputs.Builder;
using App.UI;
using App.World;
using App.World.Cameras;
using App.World.WorldGrid;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class Inputs : MonoBehaviour, IMouseOptionHandler
    {
        private BuildingInteractor processor;
        private CameraController cameraController;
        [SerializeField]
        private MouseOptionSelector mouseSelector;
        [SerializeField]
        private GameObject waypoint;
        private PauseController pauseController;
        public void Init(BuildingInteractor buildingInteractor, CameraController cameraController, PauseController pauseController)
        {
            processor = buildingInteractor;
            this.cameraController = cameraController;
            mouseSelector.MouseInputHandler = this;
            this.pauseController = pauseController;
        }
        void Update()
        {
            ProceedMouseInputPress();
            ProcessKeyboardInput();
            ProcessMouseMoveInput();
        }

        private void ProceedMouseInputPress()
        {
            
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

        private void ProcessKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.LeftAlt))
            {
                processor.OnAltButtonHold();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseController.TogglePause();
            }
        }

        private void ProcessMouseMoveInput()
        {
            processor.OnMouseMove();
            if (Input.mousePosition.x >= Screen.width - cameraController.CameraMovementZoneWidth)
            {
                cameraController.MoveCamera(CameraController.Direction.Right);
            }
            if (Input.mousePosition.x <= cameraController.CameraMovementZoneWidth)
            {
                cameraController.MoveCamera(CameraController.Direction.Left);
            }
            if (Input.mousePosition.y >= Screen.height + cameraController.CameraMovementZoneWidth)
            {
                cameraController.MoveCamera(CameraController.Direction.Down);
            }
            if (Input.mousePosition.y <= cameraController.CameraMovementZoneWidth)
            {
                cameraController.MoveCamera(CameraController.Direction.Up);
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
                    processor.UpgradingState();
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

