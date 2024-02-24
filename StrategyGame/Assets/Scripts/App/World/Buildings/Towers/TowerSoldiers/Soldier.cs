using App.World.Enemies;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace App.World.Buildings.Towers.TowerSoldiers
{
    public class Soldier : MonoBehaviour
    {
        [SerializeField]
        private SoldierData data;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private GameObject projectilePrefab;
        private float attackDamage;
        private float attackRange;
        private float attackSpeed;
        private bool initialised = false;
        private bool isIdle = false;
        private bool attacking = false;
        private float angleBetweenTargetAndThis = 0;
        private DamageAttribute attribute;
        private Enemy currentTarget;
        private ObjectPool objectPool;

        public float AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public DamageAttribute Attribute { get => attribute; set => attribute = value; }
        public Enemy CurrentTarget { get => currentTarget; set => currentTarget = value; }

        private void Update()
        {
            if(!initialised) return;
            if (currentTarget == null)
            {
                isIdle = true;
                animator.SetBool("isIdle", isIdle);
            }
            else if(!attacking)
            {
                isIdle = false;
                animator.SetBool("isIdle", isIdle);
                StartAttacking();
            }
                
        }

        public void Init(Enemy currentTarget)
        {
            AttackDamage = data.damage;
            AttackSpeed = data.attackSpeed;
            AttackRange = data.attackRange;
            Attribute = data.attribute;
            initialised = true;
            this.currentTarget = currentTarget;
            objectPool = FindObjectOfType<ObjectPool>();
        }

        public void LevelUp()
        {
            AttackDamage *= data.levelDamageMultiplier;
            AttackRange *= data.levelAttackRangeMultiplier;
            AttackSpeed *= data.levelAttackSpeedMultiplier;
        }
        public void Attack()
        {
            Projectile projectileScript = projectilePrefab.GetComponent<Projectile>();
            if (projectilePrefab == null)
            {
                print("Can`t find projectile attached to soldier");
            }
            Projectile instantiatedProjectile = objectPool.GetObjectFromPool(projectileScript.PoolObjectID, projectilePrefab)
                .GetGameObject().GetComponent<Projectile>();
            instantiatedProjectile.Init(currentTarget.transform, transform.position, data.projectileSpeed, data.damage, data.attribute);
            RefreshCanAttack();
        }
        private void RefreshCanAttack()
        {
            Debug.Log("Refreshed");
            attacking = false;
        }
        public void StartAttacking()
        {
            Debug.Log("asdasd");
            attacking = true;
            Vector3 vector = (currentTarget.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(vector, Vector3.right);
            animator.SetFloat("angle", angle);
            animator.SetFloat("yDiff",transform.position.y - currentTarget.transform.position.y);
        }
    }
}

