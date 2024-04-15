using App.Systems.MoneySystem;
using App.World.Enemies;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

namespace App.Systems.BattleWaveSystem
{
    public class BattleSystem : MonoBehaviour, INotifyEnemyDied
    {
        private SpawnerSystem spawnerSystem;
        private int enemies;
        private int currentWaveNumber;
        private PlayerMoney playerMoney;
        private GameEndScreen defeatScreen;
        private GameEndScreen victoryScreen;
        private List<Enemy> activeEnemies;
        private bool enemyStatsVisible = false;

        [SerializeField]
        private int levelToOpen;
        [SerializeField]
        private List<Wave> waves;
        

        public void Init(SpawnerSystem spawnerSystem, PlayerMoney playerMoney, GameEndScreen victoryScreen)
        {
            this.spawnerSystem = spawnerSystem;
            this.playerMoney = playerMoney;
            this.victoryScreen = victoryScreen;
            activeEnemies = new List<Enemy>();
            enemies = 0;
            currentWaveNumber = 0;
            StartCoroutine(SpawnAllWaves());
        }

        public void NotifyEnemyDied(Enemy enemy)
        {
            activeEnemies.Remove(enemy);
            playerMoney.Money += enemy.Data.bounty;
            if(--enemies <= 0 && currentWaveNumber >= waves.Count)
            {
                Debug.Log("You won!");
                PlayerPrefs.SetInt($"Level{levelToOpen}Opened", 1);
                victoryScreen.EndGame();
            }
        }

        public void ToggleEnemyStats()
        {
            enemyStatsVisible = !enemyStatsVisible;
            foreach(Enemy enemy in activeEnemies)
            {
                enemy.SetStatsVisibility(enemyStatsVisible);
            }
        }

        private IEnumerator SpawnAllWaves()
        {
            for (int i = 0; i < waves.Count; i++)
            {
                foreach(Subwave subwave in waves[i].subwaves)
                {
                    enemies += subwave.enemyNumber;
                }
            }

            for (int i = 0; i < waves.Count; i++)
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
                spawnerSystem.Spawn(subwave.enemy, activeEnemies);
            }
        }
    }

}
