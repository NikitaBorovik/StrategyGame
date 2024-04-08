using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Towers
{
    [CreateAssetMenu(fileName = "TowerExtendedDataSO", menuName = "Scriptable Objects/Buildings/Tower Extended Data")]
    public class TowerDataSO : ScriptableObject
    {

        public float attackRange;

        public float levelAttackRangeIncrementor;

        public int maxSoldiersNumber;

        public int soldierPrice;
    }

}
