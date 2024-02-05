using App.Systems.Inputs;
using App.Systems.Inputs.Builder;
using App.Systems.MoneySystem;
using App.World;
using UnityEngine;
using UnityEngine.UIElements;

namespace App
{
    public class App : MonoBehaviour
    {
        [SerializeField]
        private ObjectsContainer objectsContainer;
        [SerializeField]
        private Inputs inputs;
        [SerializeField]
        private PlayerMoney playerMoney;
        [SerializeField]
        private ObjectPool objectPool;
        private BuildingInteractor buildingInteractor;
        private void Start()
        {
            buildingInteractor = new BuildingInteractor(objectsContainer.WorldGrid, objectsContainer.MainCamera, objectsContainer.SelectedCellBorder, objectsContainer.PreviewBuilding, objectPool, playerMoney);
            inputs.Init(buildingInteractor);
        }
    }
}
