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
    public class Tower : Building, ISoldierHolder
    {
        [SerializeField]
        private GameObject selectWarriorButtons;
        [SerializeField]
        private int maxSoldiersNumber;
        [SerializeField]
        private List<Transform> soldierPlaces;
        private int soldiersNumber = 0;
        private List<Soldier> soldiers = new List<Soldier>();

        public int MaxSoldiersNumber { get => maxSoldiersNumber;}
        public int SoldiersNumber { get => soldiersNumber; set => soldiersNumber = value; }
        public List<Transform> SoldierPlaces { get => soldierPlaces;}

        public void AddSoldier(Soldier soldier)
        {
            soldiers.Add(soldier);
            foreach(Soldier s in soldiers)
            {
                Debug.Log(s);
            }
            Debug.Log(new Vector2((transform.position.x + 0.5f * Data.size), (transform.position.y + 0.5f * Data.size)));
            Debug.Log(soldier.AttackRange);
            Debug.Log(soldier.Attribute);
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * Data.size), (transform.position.y + 0.5f * Data.size)), soldier.AttackRange, soldier.Attribute);
        }

        public override void Upgrade()
        {
            foreach (Soldier soldier in soldiers)
            {
                soldier.LevelUp();
            }
        }

        private void OnMouseDown()
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
        private void OnDestroy()
        {
            foreach(Soldier soldier in soldiers)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * Data.size), (transform.position.y + 0.5f * Data.size)), soldier.AttackRange, soldier.Attribute);
            }
        }
    }
}

