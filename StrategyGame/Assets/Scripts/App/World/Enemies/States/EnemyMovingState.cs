using App;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace App.World.Enemies.States
{
    public class EnemyMovingState : IState
    {
        private Enemy parent;
        private GridPathfinding pathfinding;
        private Stack<Vector3> way;
        private Vector3 currentWaypoint;
        private Vector3 actualTargetPosition;
        private bool atFinalPoint = false;
        private bool awaitingPathRecalculation = false;

        public EnemyMovingState(Enemy parent)
        {
            this.pathfinding = parent.Pathfinding;
            this.parent = parent;
            actualTargetPosition = parent.PrimaryTarget.transform.position + new Vector3(1.5f, 1.5f, 0f);
        }

        public Vector3 CurrentWaypoint { get => currentWaypoint; set => currentWaypoint = value; }

        public void Enter()
        {
            SetEnterAnimationParameters();
            atFinalPoint = false;

            if (way == null || pathfinding.CanProcess())
            {
                way = pathfinding.ProceedPathfinding(parent.transform.position, actualTargetPosition, parent.Data.resistances);
            }
            else
            {
                //Debug.Log("Refused to get path");
                awaitingPathRecalculation = true;
            }

            if (way != null && way.Count != 0)
                CurrentWaypoint = way.Pop();
            else
                CurrentWaypoint = actualTargetPosition;
        }

        public void Exit()
        {
            parent.RigidBody.velocity = Vector3.zero;
            parent.RigidBody.Sleep();
        }

        public void Update()
        {
            if (awaitingPathRecalculation && pathfinding.CanProcess())
            {
                way = pathfinding.ProceedPathfinding(parent.transform.position, actualTargetPosition, parent.Data.resistances);
                awaitingPathRecalculation = false;
            }

            RaycastHit2D raycast = Physics2D.Raycast(parent.transform.position, (CurrentWaypoint - parent.transform.position).normalized, parent.Data.attackRange, LayerMask.GetMask("BuildingPhysicalCollider"));
            if (raycast)
            {
                Debug.Log(raycast.collider.gameObject.name);
                parent.GoToAttackState(CurrentWaypoint);
                return;
            }
            if (atFinalPoint)
                return;

            if (Vector3.Distance(parent.transform.position, CurrentWaypoint) > 0.1f)
            {
                parent.RigidBody.velocity = (CurrentWaypoint - parent.transform.position).normalized * parent.Data.speed;
            }
            else if (way != null && way.Count != 0)
            {
                CurrentWaypoint = way.Pop();
            }
            else if (CurrentWaypoint != actualTargetPosition)
            {
                CurrentWaypoint = actualTargetPosition;
            }
            else
            {
                parent.RigidBody.velocity = Vector2.zero;
                atFinalPoint = true;
            }
            if (parent.RigidBody.velocity.x >= 0)
                parent.Animator.SetBool("IsFacingRight", true);
            else
                parent.Animator.SetBool("IsFacingRight", false);
        }

        private void SetEnterAnimationParameters()
        {
            parent.Animator.SetBool("IsMoving", true);
            parent.Animator.SetBool("IsAttacking", false);
            parent.Animator.SetBool("IsDying", false);
        }
    }

}
