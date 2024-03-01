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
        private Enemy enemy;
        private bool atFinalPoint = false;
        private Vector3 actualTargetPosition;
        public EnemyMovingState(Enemy enemy)
        {
            this.pathfinding = enemy.Pathfinding;
            actualTargetPosition = enemy.PrimaryTarget.transform.position + new Vector3(1.5f, 1.5f, 0f);
            this.enemy = enemy;

        }
        public void Enter()
        {
            atFinalPoint = false;
            way = pathfinding.ProceedPathfinding(enemy.transform.position, actualTargetPosition, enemy.Data.resistances);
            if(way != null && way.Count != 0)
            {
                currentWaypoint = way.Pop();
            }
            else
            {
                currentWaypoint = actualTargetPosition;
            }
        }

        public void Exit()
        {

        }

        public void Update()
        {
            if (atFinalPoint)
                return;
            if (Vector3.Distance(enemy.transform.position, currentWaypoint) > enemy.Data.attackRange)
            {
                enemy.RigidBody.velocity = (currentWaypoint - enemy.transform.position).normalized * enemy.Data.speed;
            }
            else if (way != null && way.Count != 0)
            {
                currentWaypoint = way.Pop();
            }
            else if(currentWaypoint != actualTargetPosition)
            {
                currentWaypoint = actualTargetPosition;
            }
            else
            {
                enemy.RigidBody.velocity = Vector2.zero;
                atFinalPoint = true;
            }
        }
    }

}
