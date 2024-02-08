using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.Spikes
{
    [CreateAssetMenu(fileName = "SpikesExtendedDataSO", menuName = "Scriptable Objects/Buildings/Spikes Extended Data")]
    public class SpikesDataSO : ScriptableObject
    {
        public float damage;
        public float levelUpDamageMultiplier;
        public List<Sprite> spritesForLevels;
    }

}
