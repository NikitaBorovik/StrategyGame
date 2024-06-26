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
        [SerializeField]
        private AudioSource audioSource;
        private float attackDamage;
        private float attackSpeed;
        private float projectileSpeed;
        private bool initialised = false;
        private bool isIdle = false;
        private bool attacking = false;
        private DamageAttribute attribute;
        private Enemy currentTarget;
        private ObjectPool objectPool;

        public float AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
        public DamageAttribute Attribute { get => attribute; set => attribute = value; }
        public Enemy CurrentTarget { get => currentTarget; set => currentTarget = value; }
        public AudioSource AudioSource { get => audioSource; set => audioSource = value; }

        private void Update()
        {
            if(!initialised) return;
            if (currentTarget == null)
            {
                isIdle = true;
                attacking = false;
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
            animator.speed = AttackSpeed;
            ProjectileSpeed = data.projectileSpeed;
            Attribute = data.attribute;
            initialised = true;
            this.currentTarget = currentTarget;
            objectPool = FindObjectOfType<ObjectPool>();
        }

        public void LevelUp()
        {
            AttackDamage *= data.levelDamageMultiplier;
            AttackSpeed *= data.levelAttackSpeedMultiplier;
            ProjectileSpeed *= data.levelProjectileSpeedMultiplier;
            animator.speed = AttackSpeed;
        }
        public void Attack()
        {
            audioSource.PlayOneShot(data.attackSound);
            Projectile projectileScript = projectilePrefab.GetComponent<Projectile>();
            if (projectilePrefab == null)
            {
                print("Can`t find projectile attached to soldier");
            }
            Projectile instantiatedProjectile = objectPool.GetObjectFromPool(projectileScript.PoolObjectID, projectilePrefab)
                .GetGameObject().GetComponent<Projectile>();
            if(currentTarget != null)
                instantiatedProjectile.Init(currentTarget, transform.position, ProjectileSpeed, AttackDamage, data.attribute);
            RefreshCanAttack();
        }
        private void RefreshCanAttack()
        {
            attacking = false;
        }
        public void StartAttacking()
        {
            attacking = true;
            Vector3 vector = (currentTarget.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(vector, Vector3.right);
            animator.SetFloat("angle", angle);
            animator.SetFloat("yDiff",transform.position.y - currentTarget.transform.position.y);
        }
    }
}

