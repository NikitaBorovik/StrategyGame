using App.UI;
using App.World.Cameras;
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
        private CameraController cameraController;
        [SerializeField]
        private GameObject selectedCellBorder;
        [SerializeField]
        private GameObject previewBuilding;
        [SerializeField]
        private Transform enemyPrimaryTarget;
        [SerializeField]
        private PauseController pauser;

        public GameObject WorldGrid { get => worldGrid;}
        public Camera MainCamera { get => mainCamera;}
        public GameObject SelectedCellBorder { get => selectedCellBorder;}
        public GameObject PreviewBuilding { get => previewBuilding;}
        public Transform EnemyPrimaryTarget { get => enemyPrimaryTarget; }
        public PauseController Pauser { get => pauser;}
        public CameraController CameraController { get => cameraController;}
    }
}

