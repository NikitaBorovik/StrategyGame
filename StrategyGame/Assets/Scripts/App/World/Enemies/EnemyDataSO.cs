using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Enemies
{
    [CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/Enemies/Enemy Data")]
    public class EnemyDataSO : ScriptableObject
    {
        public float maxHealth;
        public float damage;
        public float speed;
        public float attackRange;
        public List<AttributeResistance> resistances;
    }

}
