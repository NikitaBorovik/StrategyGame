using App.Systems.BattleWaveSystem;
using App.Systems.Inputs;
using App.Systems.Inputs.Builder;
using App.Systems.MoneySystem;
using App.World;
using App.World.WorldGrid;
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
        private SpawnerSystem spawnerSystem;
        [SerializeField]
        private BattleWaveSystem battleWaveSystem;
        [SerializeField]
        private PlayerMoney playerMoney;
        [SerializeField]
        private ObjectPool objectPool;
        private BuildingInteractor buildingInteractor;
        private void Start()
        {
            buildingInteractor = new BuildingInteractor(objectsContainer.WorldGrid, objectsContainer.MainCamera, objectsContainer.SelectedCellBorder, objectsContainer.PreviewBuilding, objectPool, playerMoney);
<<<<<<< HEAD
            inputs.Init(buildingInteractor, objectsContainer.Pauser);
=======
            inputs.Init(buildingInteractor,objectsContainer.CameraController);
>>>>>>> 2a41e8ca90f786803928936a44b40c084a2d8420
            spawnerSystem.Init(objectPool, objectsContainer.EnemyPrimaryTarget,objectsContainer.WorldGrid.GetComponent<CellGrid>(), battleWaveSystem, buildingInteractor);
            battleWaveSystem.Init(spawnerSystem, playerMoney);
        }
    }
}
