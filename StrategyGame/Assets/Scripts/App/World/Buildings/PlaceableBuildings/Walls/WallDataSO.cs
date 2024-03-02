using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Walls
{
    [CreateAssetMenu(fileName = "WallUpgradeDataSO", menuName = "Scriptable Objects/Buildings/Wall Upgrade Data")]
    public class WallDataSO : ScriptableObject
    {
        public float levelUpHpMultiplier;
        public List<Sprite> spritesForLevels;
    }

}
