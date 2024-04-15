using App.Systems.BattleWaveSystem;
using App.World.Buildings.Towers;
using App.World.Enemies.States;
using App.World.WorldGrid;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace App.World.Enemies
{
    public class Enemy : MonoBehaviour, IObjectPoolItem, IDestroyable
    {
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
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private AudioSource audioSource;
        [SerializeField]
        private GameObject stats;

        private EnemyMovingState movingState;
        private EnemyAttackState attackState;
        private EnemyDyingState dyingState;

        public Transform PrimaryTarget { get => primaryTarget;}
        public GridPathfinding Pathfinding { get => pathfinding;}
        public EnemyDataSO Data { get => data;}
        public Rigidbody2D RigidBody { get => rigidBody;}
        public Animator Animator { get => animator; set => animator = value; }

        public string PoolObjectID => data.poolType;

        public List<INotifyEnemyDied> NotifyDiedlist { get => notifyDiedlist; set => notifyDiedlist = value; }
        public Health EnemyHealth { get => enemyHealth; set => enemyHealth = value; }
        public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

        public virtual void Init(Transform primaryTarget, INotifyEnemyDied notifyDied, INotifyBuilt notifyBuilt,GridPathfinding pathfinding, Vector3 position)
        {
            transform.position = position;
            this.primaryTarget = primaryTarget;
            this.pathfinding = pathfinding;
            this.notifyBuilt = notifyBuilt;

            notifyDiedlist = new List<INotifyEnemyDied> { notifyDied };
            stateMachine = new StateMachine();
            movingState = new EnemyMovingState(this);
            attackState = new EnemyAttackState(this);
            dyingState = new EnemyDyingState(this);

            EnemyHealth.MaxHP = data.maxHealth;
            EnemyHealth.CurHP = EnemyHealth.MaxHP;

            stateMachine.Init(movingState);
            notifyBuilt.Subscribe(GoToMovingState);
        }
        
        public void GoToMovingState()
        {
            stateMachine.ChangeState(movingState);
        }
        public void GoToDyingState()
        {
            stateMachine.ChangeState(dyingState);
        }
        public void GoToAttackState(Vector3 attackTargetPosition)
        {
            attackState.AttackTargetPosition = attackTargetPosition;
            stateMachine.ChangeState(attackState);
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
            GoToDyingState();
        }
        public void SetStatsVisibility(bool visible)
        {
            if(stats != null)
                stats.SetActive(visible);
        }
        private void Attack()
        {
            if (stateMachine.State == attackState)
                attackState.Attack();
        }
        private void Die()
        {
            notifyBuilt.Unsubscribe(GoToMovingState);
            foreach (INotifyEnemyDied notifyEnemyDied in notifyDiedlist)
            {
                notifyEnemyDied.NotifyEnemyDied(this);
            }
            objectPool.ReturnToPool(this);
        }
        private void FixedUpdate()
        {
            stateMachine.State.Update();
        }
        private void OnDisable()
        {
            notifyBuilt.Unsubscribe(GoToMovingState);
        }

        //void OnDrawGizmos()
        //{
        //    if (!Application.isPlaying)
        //        return;


        //    foreach (Cell current in pathfinding.closedCells)
        //    {
        //        Vector2 position = pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + pathfinding.cellGrid.StartPos.x, current.Y + pathfinding.cellGrid.StartPos.y + 1, 5));
        //        Handles.Label(position, current.F.ToString());
        //        if (current.ParentCell != null)
        //        {
        //            Gizmos.DrawLine(pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + pathfinding.cellGrid.StartPos.x, current.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f, -0.5f),
        //                pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.ParentCell.X + pathfinding.cellGrid.StartPos.x, current.ParentCell.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f, -0.5f));

        //            DrawArrowHead(pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + pathfinding.cellGrid.StartPos.x, current.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f, -0.5f),
        //                pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.ParentCell.X + pathfinding.cellGrid.StartPos.x, current.ParentCell.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f, -0.5f));
        //        }

        //    }
        //    for (int i = 0; i < pathfinding.openCells.Count; i++)
        //    {
        //        Cell current = pathfinding.openCells.container[i + 1];
        //        Vector2 position = pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + pathfinding.cellGrid.StartPos.x, current.Y + pathfinding.cellGrid.StartPos.y + 1, 5));
        //        Handles.Label(position, current.F.ToString());
        //        if (current.ParentCell != null)
        //        {
        //            Gizmos.DrawLine(pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + pathfinding.cellGrid.StartPos.x, current.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f,-0.5f),
        //                pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.ParentCell.X + pathfinding.cellGrid.StartPos.x, current.ParentCell.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f,-0.5f));

        //            DrawArrowHead(pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.X + pathfinding.cellGrid.StartPos.x, current.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f, -0.5f),
        //                pathfinding.cellGrid.Tilemap.CellToWorld(new Vector3Int(current.ParentCell.X + pathfinding.cellGrid.StartPos.x, current.ParentCell.Y + pathfinding.cellGrid.StartPos.y + 1)) + new Vector3(0.5f, -0.5f));
        //        }
        //    }
        //}
        //private void DrawArrowHead(Vector3 from, Vector3 to)
        //{
        //    const float arrowHeadSize = 0.25f;

        //    Vector3 direction = (to - from).normalized;
        //    Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 15, 0) * Vector3.forward;
        //    Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 15, 0) * Vector3.forward;

        //    Gizmos.DrawRay(to, right * arrowHeadSize);
        //    Gizmos.DrawRay(to, left * arrowHeadSize);
        //}
    }

}
