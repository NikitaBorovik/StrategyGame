using App.Systems.MoneySystem;
using App.World.Enemies;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Spikes
{
    public class Spike : PlaceableBuilding
    {
        [SerializeField]
        private SpikesDataSO extendedData;
        [SerializeField]
        private BoxCollider2D damageCollider;

        private float damage;
        private float timeBetweenAttacks;

        public override void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            base.Init(position, cellGrid, playerMoney);
            damage = extendedData.damage;
            timeBetweenAttacks = extendedData.timeBetweenAttacks;
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.piercing);
            StartCoroutine(AttackCoroutine());
        }
        public override void Upgrade()
        {
            PlayerMoney.Money -= BasicData.upgradePrice;
            Level++;
            damage *= extendedData.levelUpDamageMultiplier;
            Debug.Log(HealthComponent);
            HealthComponent.CurHP = HealthComponent.MaxHP;
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[Level];
            cellGrid.AddAttributeToCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.piercing);
        }
        private void OnDisable()
        {
            for (int i = 0; i <= Level; i++)
            {
                cellGrid.RemoveAttributeFromCells(new Vector2((transform.position.x + 0.5f * BasicData.size), (transform.position.y + 0.5f * BasicData.size)), 0.5f, DamageAttribute.piercing);
            }
            GetComponent<SpriteRenderer>().sprite = extendedData.spritesForLevels[0];
        }

        private IEnumerator AttackCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenAttacks);
                MakeHarm();
            }
        }

        private void MakeHarm()
        {
            Collider2D[] targets = new Collider2D[10] ;
            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Enemy"));

            int targetsCount = damageCollider.OverlapCollider(filter, targets);

            for (int i = 0; i < targetsCount; i++)
            {
                Health targetHealth = targets[i].gameObject.GetComponent<Health>();
                if (targetHealth != null)
                    targetHealth.TakeDamage(damage);
            }

        }
    }

}
