using UnityEngine;


namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField]
        private Grid worldGrid;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private GameObject selectedCellBorder;
        [SerializeField]
        private GameObject previewBuilding;

        public Grid WorldGrid { get => worldGrid;}
        public Camera MainCamera { get => mainCamera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
        public GameObject PreviewBuilding { get => previewBuilding;}
    }
}

