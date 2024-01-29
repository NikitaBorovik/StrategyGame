using App.Systems.Inputs;
using App.World;
using UnityEngine;


namespace App
{
    public class App : MonoBehaviour
    {
        [SerializeField]
        private ObjectsContainer objectsContainer;
        [SerializeField]
        private Inputs inputs;
        private void Start()
        {

            inputs.Init(objectsContainer.WorldGrid, objectsContainer.MainCamera, objectsContainer.SelectedCellBorder, objectsContainer.PreviewBuilding);
        }
    }
}
