using UnityEngine;


namespace App.World
{
    public class ObjectsContainer : MonoBehaviour
    {
        [SerializeField]
        private GameObject worldGrid;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private GameObject selectedCellBorder;
        [SerializeField]
        private GameObject previewBuilding;
        [SerializeField]
        private Transform enemyPrimaryTarget;

        public GameObject WorldGrid { get => worldGrid;}
        public Camera MainCamera { get => mainCamera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
        public GameObject PreviewBuilding { get => previewBuilding;}
        public Transform EnemyPrimaryTarget { get => enemyPrimaryTarget; }
    }
}

