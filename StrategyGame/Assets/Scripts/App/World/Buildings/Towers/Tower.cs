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


namespace App.World.Buildings.Towers
{
    public class Tower : Building, ISoldierHolder, IToggleAttackRangeVision, INotifyEnemyDied
    {
        //[SerializeField]
        //private GameObject selectWarriorButtons;
        [SerializeField]
        private int maxSoldiersNumber;
        [SerializeField]
        private int soldierPrice;
        [SerializeField]
        private List<Transform> soldierPlaces;
        [SerializeField]
        private GameObject attackRangeField;
        [SerializeField]
        private List<GameObject> objectsToReveal;
        
        private int soldiersNumber = 0;
        
        private List<Soldier> soldiers = new List<Soldier>();
        private List<Enemy> detectedEnemies;
        private Enemy currentTarget;

        public int MaxSoldiersNumber { get => maxSoldiersNumber;}
        public int SoldiersNumber { get => soldiersNumber; set => soldiersNumber = value; }
        public List<Transform> SoldierPlaces { get => soldierPlaces;}
        public Enemy CurrentTarget { get => currentTarget; set => currentTarget = value; }
        public List<Enemy> DetectedEnemies { get => detectedEnemies; set => detectedEnemies = value; }

        public override void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            base.Init(position, cellGrid, playerMoney);
            soldiers = new List<Soldier>();
            DetectedEnemies = new List<Enemy>();
        }

        public void AddSoldier(Soldier soldier)
        {
            if(playerMoney.Money < soldierPrice)
            {
                //TODO PLAY SOME SOUND
                return;
            }
            soldiers.Add(soldier);
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), 
                (transform.position.y + 0.5f * BasicData.size)), soldier.AttackRange, soldier.Attribute);
            playerMoney.Money -= soldierPrice;
        }

        public override void Upgrade()
        {
            if (playerMoney.Money < BasicData.upgradePrice)
            {
                //TODO PLAY SOME SOUND
                return;
            }
            playerMoney.Money -= BasicData.upgradePrice;
            UpgradeTower();
            UpgradeSoldiers();
        }
        public void ToggleAttackRangeVision()
        {
            if (attackRangeField != null)
            {
                if (attackRangeField.activeSelf)
                    attackRangeField.SetActive(false);
                else
                    attackRangeField.SetActive(true);
            }
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
            CurrentHealth = Health;
            foreach (GameObject obj in objectsToReveal)
                obj.SetActive(false);
            objectsToReveal[Level - 1].SetActive(true);
        }

        private void UpgradeSoldiers()
        {
            foreach (Soldier soldier in soldiers)
            {
                soldier.LevelUp();
            }
        }

        //private void OnMouseDown()
        //{
        //    if (Clickable)
        //    {       
        //        Animator animator = selectWarriorButtons.GetComponent<Animator>();
        //        if (selectWarriorButtons.activeSelf)
        //        {
        //            animator.SetBool("IsVisible", false);
        //        }
        //        else
        //        {
        //            selectWarriorButtons.SetActive(true);
        //            animator.SetBool("IsVisible", true);
        //        }
        //    }
            
        //}
        private void OnDisable()
        {
            foreach (Soldier soldier in soldiers)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * BasicData.size), 
                    (transform.position.y + 0.5f * BasicData.size)), soldier.AttackRange, soldier.Attribute);
            }
            foreach (GameObject obj in objectsToReveal)
                obj.SetActive(false);
            foreach (Soldier soldier in soldiers)
            {
                //TODO ADD OBJECT POOL
                GameObject.Destroy(soldier.gameObject);
            }
            soldiersNumber = 0;
        }
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        //    Debug.Log("Enter");
        //    if(enemy != null)
        //    {
        //        DetectedEnemies.Add(enemy);
        //        if (CurrentTarget == null)
        //        {
        //            CurrentTarget = enemy;
        //            RefreshSoldiersTarget();
        //        }
        //    }
        //}
        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        //    if(enemy != null)
        //    {
        //        enemy.NotifyDiedlist.Remove(this);
        //        DetectedEnemies.Remove(enemy);
        //        if(DetectedEnemies.Count != 0)
        //        {
        //            currentTarget = DetectedEnemies[0];
        //        }
        //        else
        //        {
        //            currentTarget = null;
        //        }
        //        RefreshSoldiersTarget();
        //    }
        //}

        public void RefreshSoldiersTarget()
        {
            foreach (var soldier in soldiers)
            {
                Debug.Log(soldier);
                soldier.CurrentTarget = currentTarget;
            }
        }
    }
}
