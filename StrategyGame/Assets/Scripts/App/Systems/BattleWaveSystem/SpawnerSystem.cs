using App.Systems.Inputs.Builder;
using App.World;
using App.World.Enemies;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.BattleWaveSystem
{
    public class SpawnerSystem : MonoBehaviour
    {
        private INotifyEnemyDied notifyEnemyDied;
        private ObjectPool pool;
        private Transform enemyPrimaryTarget;
        private CellGrid cellGrid;
        private GridPathfinding enemiesPathfinding;
        private INotifyBuilt notifyBuilt;

        [SerializeField]
        private List<Vector3> spawningPoints;
        


        public void Init(ObjectPool pool, Transform enemyPrimaryTarget,CellGrid cellGrid, INotifyEnemyDied notifyEnemyDied, INotifyBuilt notifyBuilt)
        {
            this.pool = pool;
            this.enemyPrimaryTarget = enemyPrimaryTarget;
            this.notifyEnemyDied = notifyEnemyDied;
            this.notifyBuilt = notifyBuilt;
            enemiesPathfinding = new GridPathfinding(cellGrid);
        }

        public void Spawn(GameObject enemy)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript == null)
            {
                Debug.Log("No enemy script found on enemy game object");
                return;
            }
            int random = Random.Range(0, spawningPoints.Count);
            Vector3 pos = spawningPoints[random];
            Enemy instantiatedEnemy = pool.GetObjectFromPool(enemyScript.PoolObjectID, enemy).GetGameObject().GetComponent<Enemy>();
            instantiatedEnemy.Init(enemyPrimaryTarget, notifyEnemyDied, notifyBuilt, enemiesPathfinding, pos);
        }
    }

}
