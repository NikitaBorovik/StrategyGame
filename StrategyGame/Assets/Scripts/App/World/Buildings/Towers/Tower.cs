using App.Systems.Inputs.Builder;
using App.Systems.MoneySystem;
using App.World.Buildings.Towers.TowerSoldiers;
using App.World.WorldGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;


namespace App.World.Buildings.Towers
{
    public class Tower : Building, ISoldierHolder, IToggleAttackRangeVision
    {
        [SerializeField]
        private GameObject selectWarriorButtons;
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

        public int MaxSoldiersNumber { get => maxSoldiersNumber;}
        public int SoldiersNumber { get => soldiersNumber; set => soldiersNumber = value; }
        public List<Transform> SoldierPlaces { get => soldierPlaces;}

        public override void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            base.Init(position, cellGrid, playerMoney);
            soldiers = new List<Soldier>();
        }

        public void AddSoldier(Soldier soldier)
        {
            if(playerMoney.Money < soldierPrice)
            {
                //TODO PLAY SOME SOUND
                return;
            }
            soldiers.Add(soldier);
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * Data.size), (transform.position.y + 0.5f * Data.size)), soldier.AttackRange, soldier.Attribute);
            playerMoney.Money -= soldierPrice;
        }

        public override void Upgrade()
        {
            base.Upgrade();
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
        private void UpgradeTower()
        {
            level++;
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

        private void OnMouseDown()
        {
            if (Clickable)
            {
                Animator animator = selectWarriorButtons.GetComponent<Animator>();
                if (selectWarriorButtons.activeSelf)
                {
                    animator.SetBool("IsVisible", false);
                }
                else
                {
                    selectWarriorButtons.SetActive(true);
                    animator.SetBool("IsVisible", true);
                }
            }
            
        }
        private void OnDisable()
        {
            foreach (Soldier soldier in soldiers)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * Data.size), (transform.position.y + 0.5f * Data.size)), soldier.AttackRange, soldier.Attribute);
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

    }
}
