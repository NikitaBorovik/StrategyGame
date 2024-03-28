using App;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace App.World.Enemies.States
{
    public class EnemyAttackState : IState
    {
        private Enemy parent;
        private Vector3 attackTargetPosition;
        private Health targetHealth;

        public Vector3 AttackTargetPosition { get => attackTargetPosition; set => attackTargetPosition = value; }
        public EnemyAttackState(Enemy parent)
        {
            this.parent = parent;
        }
        public void Enter()
        {
            Debug.Log("Entered attack state");
            SetEnterAnimationParameters();
            RaycastHit2D raycast = Physics2D.Raycast(parent.transform.position, AttackTargetPosition - parent.transform.position, parent.Data.attackRange, LayerMask.GetMask("BuildingPhysicalCollider"));
            if (!raycast)
            {
                Debug.Log("No collision detected");
                parent.GoToMovingState();
            }
                
            targetHealth = raycast.collider.GetComponentInParent<Health>();
            if(targetHealth == null)
            {
                Debug.Log("Tried to attack target without health");
                parent.GoToMovingState();
            }
            parent.RigidBody.velocity = Vector3.zero;
        }

        public void Exit()
        {
        }

        public void Update()
        {
            //if (!targetHealth.isActiveAndEnabled)
            //    targetHealth = null;
            //if(targetHealth == null)
            //    parent.GoToMovingState();
        }

        public void Attack()
        {
            parent.AudioSource.PlayOneShot(parent.Data.attackSound);
            RaycastHit2D raycast = Physics2D.Raycast(parent.transform.position, (AttackTargetPosition - parent.transform.position).normalized, parent.Data.attackRange, LayerMask.GetMask("BuildingPhysicalCollider"));
            if (!raycast)
            {
                Debug.Log("No collision detected");
                parent.GoToMovingState();
                return;
            }
            targetHealth = raycast.collider.GetComponentInParent<Health>();
            if (targetHealth != null && targetHealth.isActiveAndEnabled)
                targetHealth.TakeDamage(parent.Data.damage);
        }


        private void SetEnterAnimationParameters()
        {
            parent.Animator.SetBool("IsMoving", false);
            parent.Animator.SetBool("IsAttacking", true);
            parent.Animator.SetBool("IsDying", false);
        }
    }
}