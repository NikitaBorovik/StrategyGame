using App.Systems.Inputs.Builder;
using App.World;
using App.World.WorldGrid;
using UnityEngine;

namespace App.Systems.Inputs
{
    public class Inputs : MonoBehaviour, IMouseOptionHandler
    {
        private BuildingInteractor processor;
        [SerializeField]
        private MouseOptionSelector mouseSelector;
        [SerializeField]
        private GameObject waypoint;

        ////TMP TEST
        //private Vector2 pos1;
        //private Vector2 pos2;
        //private GameObject worldGrid;

        public void Init(BuildingInteractor buildingInteractor)
        {
            processor = buildingInteractor;
            mouseSelector.MouseInputHandler = this;
        }
        void Update()
        {
            ProceedMouseInputPress();
            ProcessKeyboardInput();

        }

        private void ProceedMouseInputPress()
        {
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

        private void ProcessKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Debug.Log("Alt");
                processor.OnAltButtonHold();
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

