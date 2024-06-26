using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.BuildingsSO
{
    [CreateAssetMenu(fileName = "BuildingDataSO", menuName = "Scriptable Objects/Buildings/Building Data")]
    public class BuildingData : ScriptableObject
    {
        public int price;
        public int upgradePrice;
        public int size;
        public float health;
        public Sprite sprite;
        public string poolObjectID;
       // public GameObject prefab;
    }

}
