using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.BattleWaveSystem
{
    [Serializable]
    public class Subwave
    {
        public GameObject enemy;
        public int enemyNumber;
        public float spawnDelay;
    }

}
