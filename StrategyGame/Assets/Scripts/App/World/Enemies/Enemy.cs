using App;
using App.Systems.BattleWaveSystem;
using App.World.Enemies.States;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Enemies
{
    public  class Enemy : MonoBehaviour, IObjectPoolItem
    {
        private INotifyEnemyDied notifyDied;
        private StateMachine stateMachine;
        private GridPathfinding pathfinding;
        private Rigidbody2D rigidbody;
        private Transform primaryTarget;
        private ObjectPool objectPool;

        [SerializeField]
        private EnemyDataSO data;

        private EnemyMovingState movingState;

        public Transform PrimaryTarget { get => primaryTarget;}
        public GridPathfinding Pathfinding { get => pathfinding;}
        public EnemyDataSO Data { get => data;}
        public Rigidbody2D Rigidbody { get => rigidbody;}

        public string PoolObjectID => data.poolType;

        public virtual void Init(Transform primaryTarget, INotifyEnemyDied notifyDied, Vector3 position)
        {
            transform.position = position;
            this.primaryTarget = primaryTarget;
            this.notifyDied = notifyDied;
            rigidbody = GetComponent<Rigidbody2D>();
            stateMachine = new StateMachine();
            pathfinding = GetComponent<GridPathfinding>();
            pathfinding.Init(FindObjectOfType<CellGrid>(), Data.resistances);
            movingState = new EnemyMovingState(this);
            stateMachine.Init(movingState);
            
        }
        private void Update()
        {
            stateMachine.State.Update();
        }
        public void Attack() { }

        public void GetFromPool(ObjectPool pool)
        {
            objectPool = pool;
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public void Die()
        {
            notifyDied.NotifyEnemyDied();
        }
    }

}
