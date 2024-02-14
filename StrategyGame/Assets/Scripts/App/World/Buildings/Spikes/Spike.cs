using App.Systems.MoneySystem;
using App.World.Buildings.Walls;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace App.World.Buildings.Spikes
{
    public class Spike : Building
    {
        [SerializeField]
        private SpikesDataSO extendedData;

        private float damage;

        public override void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            base.Init(position, cellGrid, playerMoney);
            damage = extendedData.damage;
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.piercing);
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
            damage *= extendedData.levelUpDamageMultiplier;
            Debug.Log(Health);
            CurrentHealth = Health;
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[Level];
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.piercing);
        }
        private void OnDisable()
        {
            Health = BasicData.health;
            for (int i = 0; i <= Level; i++)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.piercing);
            }
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[0];
        }
    }

}
