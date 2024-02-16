using App.World;
using App.World.Enemies;
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

        [SerializeField]
        private List<Vector3> spawningPoints;

        public void Init(ObjectPool pool, Transform enemyPrimaryTarget, INotifyEnemyDied notifyEnemyDied)
        {
            this.pool = pool;
            this.enemyPrimaryTarget = enemyPrimaryTarget;
            this.notifyEnemyDied = notifyEnemyDied;
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
            instantiatedEnemy.Init(enemyPrimaryTarget, notifyEnemyDied, pos);
        }
    }

}
