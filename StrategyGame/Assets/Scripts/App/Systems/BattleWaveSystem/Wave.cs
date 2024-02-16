using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.BattleWaveSystem
{
    [Serializable]
    public class Wave
    {
        public List<Subwave> subwaves;
        public float waveDelay;
    }

}
