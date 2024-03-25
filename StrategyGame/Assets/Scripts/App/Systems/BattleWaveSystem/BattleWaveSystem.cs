using App.Systems.MoneySystem;
using App.World.Enemies;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace App.Systems.BattleWaveSystem
{
    public class BattleWaveSystem : MonoBehaviour, INotifyEnemyDied
    {
        private SpawnerSystem spawnerSystem;
        private int enemies;
        private int currentWaveNumber;
        private PlayerMoney playerMoney;
        private GameEndScreen defeatScreen;
        private GameEndScreen victoryScreen;


        [SerializeField]
        private List<Wave> waves;

        public void Init(SpawnerSystem spawnerSystem, PlayerMoney playerMoney, GameEndScreen victoryScreen)
        {
            this.spawnerSystem = spawnerSystem;
            this.playerMoney = playerMoney;
            this.victoryScreen = victoryScreen;
            enemies = 0;
            currentWaveNumber = 0;
            StartCoroutine(SpawnAllWaves());
        }

        public void NotifyEnemyDied(Enemy enemy)
        {
            playerMoney.Money += enemy.Data.bounty;
            Debug.Log(enemies);
            Debug.Log(currentWaveNumber);
            if(--enemies <= 0 && currentWaveNumber >= waves.Count)
            {
                Debug.Log("You won!");
                victoryScreen.EndGame();
            }
        }

        private IEnumerator SpawnAllWaves()
        {
            for(int i = 0; i < waves.Count; i++)
            {
                yield return SpawnWave(waves[i]);
                currentWaveNumber++;
            }
        }

        private IEnumerator SpawnWave(Wave wave)
        {
            yield return new WaitForSeconds(wave.waveDelay);
            for (int i = 0; i < wave.subwaves.Count; i++)
            {
                yield return SpawnSubwave(wave.subwaves[i]);
            }
        }

        private IEnumerator SpawnSubwave(Subwave subwave)
        {
            for(int i =0; i < subwave.enemyNumber; i++)
            {
                yield return new WaitForSeconds(subwave.spawnDelay);
                spawnerSystem.Spawn(subwave.enemy);
            }
        }
    }

}
