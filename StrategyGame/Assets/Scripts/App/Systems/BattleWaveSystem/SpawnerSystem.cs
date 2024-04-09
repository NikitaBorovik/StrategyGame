using App.Systems.Inputs.Builder;
using App.World;
using App.World.Enemies;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        private List<PointRange> spawningPoints;

        public void FixedUpdate()
        {
            enemiesPathfinding.RefreshIterations();
        }

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
            PointRange range = spawningPoints[random];
            Vector3 pos = new Vector3(Random.Range(range.rangeStart.x, range.rangeEnd.x+1), Random.Range(range.rangeStart.y, range.rangeEnd.y+1));
            Enemy instantiatedEnemy = pool.GetObjectFromPool(enemyScript.PoolObjectID, enemy).GetGameObject().GetComponent<Enemy>();
            instantiatedEnemy.Init(enemyPrimaryTarget, notifyEnemyDied, notifyBuilt, enemiesPathfinding, pos);
        }
    }
}

[System.Serializable]
public class PointRange
{
    public Vector3Int rangeStart;
    public Vector3Int rangeEnd;
}