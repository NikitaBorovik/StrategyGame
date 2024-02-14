using App.Systems.MoneySystem;
using App.World.Buildings.Towers.TowerSoldiers;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.Walls
{
    public class Wall : Building
    {
        [SerializeField]
        private WallDataSO extendedData;

        public override void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            base.Init(position, cellGrid, playerMoney);
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.fortified);
        }
        public override void Upgrade()
        {
            if (playerMoney.Money < BasicData.upgradePrice)
            {
                //TODO Play Music
                return;
            }
            playerMoney.Money -= BasicData.upgradePrice;
            Level++;
            Health *= extendedData.levelUpHpMultiplier;
            Debug.Log("health");
            CurrentHealth = Health;
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[Level];
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.fortified);
        }
        private void OnDisable()
        {
            Debug.Log("R");
            Health = BasicData.health;
            for(int i = 0; i <= Level; i++)
            {
                Debug.Log("Removing");
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.fortified);
            }
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[0];
        }
    }
}


