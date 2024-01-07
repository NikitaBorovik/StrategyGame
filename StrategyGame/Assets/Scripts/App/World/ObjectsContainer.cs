using UnityEngine;


namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField]
        private Grid worldGrid;
        [SerializeField]
        private Camera camera;
        [SerializeField]
        private GameObject selectedCellBorder;

        public Grid WorldGrid { get => worldGrid;}
        public Camera Camera { get => camera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
    }
}

