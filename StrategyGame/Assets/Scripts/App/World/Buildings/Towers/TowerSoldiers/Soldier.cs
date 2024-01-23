using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.Towers.TowerSoldiers
{
    public abstract class Soldier : MonoBehaviour
    {
        [SerializeField]
        private SoldierData data;
        private float attackDamage;
        private float attackRange;
        private float attackSpeed;
        private bool initialised = false;
        private DamageAttribute attribute;

        public float AttackDamage { get => attackDamage; set => attackDamage = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public DamageAttribute Attribute { get => attribute; set => attribute = value; }

        public virtual void Init()
        {
            AttackDamage = data.damage;
            AttackSpeed = data.attackSpeed;
            AttackRange = data.attackRange;
            Attribute = data.attribute;
            initialised = true;
        }

        public virtual void LevelUp()
        {
            AttackDamage *= data.levelDamageMultiplier;
            AttackRange *= data.levelAttackRangeMultiplier;
            AttackSpeed *= data.levelAttackSpeedMultiplier;
        }
        public abstract void Attack();
    }
}

