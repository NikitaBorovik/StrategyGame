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
        public void Init(Grid worldGrid, Camera camera, GameObject selectedCellBorder)
        {
            processor = new BuildingInteractor(worldGrid, camera, building, selectedCellBorder);
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
        }
    }
}

