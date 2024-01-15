using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.BuildingsSO
{
    [CreateAssetMenu(fileName = "BuildingKindSO", menuName = "Scriptable Objects/Buildings/Building Kind")]
    public class BuildingKindSO : ScriptableObject
    {
        public GameObject prefab;
        public int size;
        public Sprite sprite;
    }
}

