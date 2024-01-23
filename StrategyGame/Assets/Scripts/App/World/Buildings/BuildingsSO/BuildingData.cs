using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.BuildingsSO
{
    [CreateAssetMenu(fileName = "BuildingDataSO", menuName = "Scriptable Objects/Buildings/Building Data")]
    public class BuildingData : ScriptableObject
    {
        public int size;
        public float health;
        public Sprite sprite;
    }

}
