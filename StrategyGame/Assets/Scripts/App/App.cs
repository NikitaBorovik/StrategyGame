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
        private BattleSystem battleWaveSystem;
        [SerializeField]
        private BuildingSystem buildingSystem;
        [SerializeField]
        private PlayerMoney playerMoney;
        [SerializeField]
        private ObjectPool objectPool;
        
        private void Start()
        {
            buildingSystem.Init(objectsContainer.WorldGrid, objectsContainer.MainCamera, objectsContainer.SelectedCellBorder, objectsContainer.PreviewBuilding, objectPool, playerMoney);
            inputs.Init(buildingSystem, battleWaveSystem, objectsContainer.CameraController, objectsContainer.Pauser);
            spawnerSystem.Init(objectPool, objectsContainer.EnemyPrimaryTarget,objectsContainer.WorldGrid.GetComponent<CellGrid>(), battleWaveSystem, buildingSystem);
            battleWaveSystem.Init(spawnerSystem, playerMoney, objectsContainer.VictoryScreen);
        }
    }
}
