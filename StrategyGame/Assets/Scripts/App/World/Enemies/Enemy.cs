using App.Systems.BattleWaveSystem;
using App.World.Buildings.Towers;
using App.World.Enemies.States;
using App.World.WorldGrid;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Enemies
{
    public  class Enemy : MonoBehaviour, IObjectPoolItem, IDestroyable
    {
        private INotifyEnemyDied notifyDied;
        private INotifyBuilt notifyBuilt;
        private StateMachine stateMachine;
        private GridPathfinding pathfinding;

        private Transform primaryTarget;
        private ObjectPool objectPool;
        private List<INotifyEnemyDied> notifyDiedlist;

        [SerializeField]
        private Health enemyHealth;

        [SerializeField]
        private EnemyDataSO data;

        [SerializeField]
        private Rigidbody2D rigidBody;

        private EnemyMovingState movingState;

        public Transform PrimaryTarget { get => primaryTarget;}
        public GridPathfinding Pathfinding { get => pathfinding;}
        public EnemyDataSO Data { get => data;}
        public Rigidbody2D RigidBody { get => rigidBody;}

        public string PoolObjectID => data.poolType;

        public List<INotifyEnemyDied> NotifyDiedlist { get => notifyDiedlist; set => notifyDiedlist = value; }
        public Health EnemyHealth { get => enemyHealth; set => enemyHealth = value; }

        public virtual void Init(Transform primaryTarget, INotifyEnemyDied notifyDied, INotifyBuilt notifyBuilt,GridPathfinding pathfinding, Vector3 position)
        {
            transform.position = position;
            this.primaryTarget = primaryTarget;
            this.pathfinding = pathfinding;
            this.notifyBuilt = notifyBuilt;

            notifyDiedlist = new List<INotifyEnemyDied> { notifyDied };
            stateMachine = new StateMachine();
            movingState = new EnemyMovingState(this);

            EnemyHealth.MaxHP = data.maxHealth;
            EnemyHealth.CurHP = EnemyHealth.MaxHP;

            stateMachine.Init(movingState);
            notifyBuilt.Subscribe(MovingState);
        }
        private void Update()
        {
            stateMachine.State.Update();
        }
        private void OnDisable()
        {
            notifyBuilt.Unsubscribe(MovingState);
        }
        public void Attack() { }

        public void MovingState()
        {
            stateMachine.ChangeState(movingState);
        }

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
        public void DestroySequence()
        {
            notifyBuilt.Unsubscribe(MovingState);
            foreach (INotifyEnemyDied notifyEnemyDied in notifyDiedlist)
            {
                notifyEnemyDied.NotifyEnemyDied(this);
            }
            objectPool.ReturnToPool(this);
        }
    }

}
