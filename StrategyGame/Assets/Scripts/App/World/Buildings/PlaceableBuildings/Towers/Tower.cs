using App.Systems.BattleWaveSystem;
using App.Systems.Inputs.Builder;
using App.Systems.MoneySystem;
using App.World.Buildings.Towers.TowerSoldiers;
using App.World.Enemies;
using App.World.WorldGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;


namespace App.World.Buildings.PlaceableBuildings.Towers
{
    public class Tower : PlaceableBuilding, ISoldierHolder, IToggleAttackRangeVision, INotifyEnemyDied, IDestroyable
    {
        //[SerializeField]
        //private GameObject selectWarriorButtons;
        
        [SerializeField]
        private List<Transform> soldierPlaces;
        [SerializeField]
        private GameObject attackRangeField;
        [SerializeField]
        private TowerEnemyDetector enemyDetector;
        [SerializeField]
        private List<GameObject> objectsToReveal;
        [SerializeField]
        private TowerDataSO extendedData;

        [SerializeField]
        private AudioSource audioSource;
        private float curAttackRange;
        private int soldiersNumber = 0;
        
        private List<Soldier> soldiers = new List<Soldier>();
        private List<Enemy> detectedEnemies;
        private Enemy currentTarget;


        public int SoldiersNumber { get => soldiersNumber; set => soldiersNumber = value; }
        public List<Transform> SoldierPlaces { get => soldierPlaces;}
        public Enemy CurrentTarget { get => currentTarget; set => currentTarget = value; }
        public List<Enemy> DetectedEnemies { get => detectedEnemies; set => detectedEnemies = value; }
        public AudioSource AudioSource { get => audioSource;}
        public TowerDataSO ExtendedData { get => extendedData;}

        public override void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            base.Init(position, cellGrid, playerMoney);
            soldiers = new List<Soldier>();
            DetectedEnemies = new List<Enemy>();
            curAttackRange = ExtendedData.attackRange;
            enemyDetector.Collider2D.radius = curAttackRange;
            attackRangeField.transform.localScale = new Vector3(curAttackRange, curAttackRange, 1);
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 1f ),
                (transform.position.y + 1f)), 1f, DamageAttribute.fortified);
        }

        public void AddSoldier(Soldier soldier)
        {
            soldiers.Add(soldier);
            for(int i = 0; i < Level; i++)
            {
                soldier.LevelUp();
            }

            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), 
                (transform.position.y + 0.5f * BasicData.size)), curAttackRange, soldier.Attribute);
            PlayerMoney.Money -= ExtendedData.soldierPrice;
            notifyGridWeightChanged?.Invoke();
        }

        public override void Upgrade()
        {
            PlayerMoney.Money -= BasicData.upgradePrice;
            UpgradeTower();
            UpgradeSoldiers();
        }
        public void SetAttackRangeVision(bool value)
        {
            attackRangeField.SetActive(value);
        }

        public void NotifyEnemyDied(Enemy enemy)
        {
            DetectedEnemies.Remove(enemy);
            currentTarget = DetectedEnemies[0];
            RefreshSoldiersTarget();
        }

        private void UpgradeTower()
        {
            Level++;
            
            foreach (GameObject obj in objectsToReveal)
                obj.SetActive(false);
            objectsToReveal[Level - 1].SetActive(true);

            foreach (Soldier soldier in soldiers)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * BasicData.size),
                (transform.position.y + 0.5f * BasicData.size)), curAttackRange, soldier.Attribute);
            }

            curAttackRange += ExtendedData.levelAttackRangeIncrementor;

            foreach (Soldier soldier in soldiers)
            {
                cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size),
                (transform.position.y + 0.5f * BasicData.size)), curAttackRange, soldier.Attribute);
            }
            enemyDetector.Collider2D.radius = curAttackRange;
            attackRangeField.transform.localScale = new Vector3(curAttackRange, curAttackRange,1);

        }

        private void UpgradeSoldiers()
        {
            foreach (Soldier soldier in soldiers)
            {
                soldier.LevelUp();
            }
        }


        private void OnDisable()
        {
            transform.position = new Vector3(10000, 10000, 0);
            cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 1f),
                (transform.position.y + 1f)), 1f, DamageAttribute.fortified);
            foreach (Soldier soldier in soldiers)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * BasicData.size), 
                    (transform.position.y + 0.5f * BasicData.size)), curAttackRange, soldier.Attribute);
            }
            foreach (GameObject obj in objectsToReveal)
                obj.SetActive(false);
            foreach (Soldier soldier in soldiers)
            {
                //TODO ADD OBJECT POOL
                GameObject.Destroy(soldier.gameObject);
            }
            curAttackRange = ExtendedData.attackRange;
            soldiersNumber = 0;
            notifyGridWeightChanged?.Invoke();
        }

        public void RefreshSoldiersTarget()
        {
            foreach (var soldier in soldiers)
            {
                Debug.Log(soldier);
                soldier.CurrentTarget = currentTarget;
            }
        }

        public void DestroySequence()
        {
            objectPool.ReturnToPool(this);
        }
    }
}
