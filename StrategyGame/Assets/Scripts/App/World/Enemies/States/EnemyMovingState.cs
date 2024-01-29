using App;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Enemies.States
{
    public class EnemyMovingState : IState
    {
        private GridPathfinding pathfinding;
        private Stack<Vector3> way;
        private Vector3 currentWaypoint;
        private Transform target;
        private Enemy enemy;
        public EnemyMovingState(Enemy enemy)
        {
            this.pathfinding = enemy.Pathfinding;
            this.target = enemy.PrimaryTarget.transform;
            this.enemy = enemy;
        }
        public void Enter()
        {
            way = pathfinding.ProceedPathfinding(enemy.transform.position, target.position + new Vector3(1.5f, 1.5f, 0f));
            if(way != null && way.Count != 0)
            {
                currentWaypoint = way.Pop();
            }
            else
            {
                currentWaypoint = target.transform.position;
            }
            Debug.Log("Way " + way.Count);
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (Vector3.Distance(enemy.transform.position, currentWaypoint) > enemy.Data.attackRange)
            {
               // Debug.Log("1");
                enemy.Rigidbody.velocity = (currentWaypoint - enemy.transform.position).normalized * enemy.Data.speed;
            }
            else if (way != null && way.Count != 0)
            {
               // Debug.Log("2");
                currentWaypoint = way.Pop();
            }
            else if(currentWaypoint != target.transform.position)
            {
              //  Debug.Log("3");
                currentWaypoint = target.transform.position;
            }
            else
            {
              //  Debug.Log("4");
                enemy.Rigidbody.velocity = Vector2.zero;
            }
        }
    }

}
