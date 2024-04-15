using App.Systems.MoneySystem;
using App.World.Buildings.Towers.TowerSoldiers;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Walls
{
    public class Wall : PlaceableBuilding, IDestroyable
    {
        [SerializeField]
        private WallDataSO extendedData;

        public void DestroySequence()
        {
            objectPool.ReturnToPool(this);
        }

        public override void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            base.Init(position, cellGrid, playerMoney);
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.fortified);
        }
        public override void Upgrade()
        {
            PlayerMoney.Money -= BasicData.upgradePrice;
            Level++;
            HealthComponent.MaxHP *= extendedData.levelUpHpMultiplier;
            HealthComponent.CurHP = HealthComponent.MaxHP;
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[Level];
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.fortified);
        }
        private void OnDisable()
        {
            HealthComponent.MaxHP = BasicData.health;
            for(int i = 0; i <= Level; i++)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.fortified);
            }
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[0];
        }
    }
}


