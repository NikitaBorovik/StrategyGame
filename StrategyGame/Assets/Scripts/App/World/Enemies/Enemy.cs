using App;
using App.World.Enemies.States;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        private StateMachine stateMachine;
        private GridPathfinding pathfinding;
        private Rigidbody2D rigidbody;

        [SerializeField]
        private GameObject primaryTarget;
        
        [SerializeField]
        private EnemyDataSO data;

        private EnemyMovingState movingState;

        public GameObject PrimaryTarget { get => primaryTarget;}
        public GridPathfinding Pathfinding { get => pathfinding;}
        public EnemyDataSO Data { get => data;}
        public Rigidbody2D Rigidbody { get => rigidbody;}

        public virtual void Init()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            stateMachine = new StateMachine();
            pathfinding = GetComponent<GridPathfinding>();
            pathfinding.Init(FindObjectOfType<CellGrid>(), Data.resistances);
            movingState = new EnemyMovingState(this);
            stateMachine.Init(movingState);
        }
        private void Start()
        {
            Init();
        }
        private void Update()
        {
            stateMachine.State.Update();
        }
        public abstract void Attack();
    }

}
