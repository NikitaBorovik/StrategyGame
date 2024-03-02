using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.Towers.TowerSoldiers
{
    [CreateAssetMenu(fileName = "SoldierDataSO", menuName = "Scriptable Objects/Soldiers/Soldier Data")]
    public class SoldierData : ScriptableObject
    {
        public float damage;
        public float attackSpeed;
        public float projectileSpeed;
        public float levelDamageMultiplier;
        public float levelAttackSpeedMultiplier;
        public float levelProjectileSpeedMultiplier;
        public DamageAttribute attribute;
    }
}

