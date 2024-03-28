using App.Systems.MoneySystem;
using App.World.Buildings.PlaceableBuildings.BuildingsSO;
using App.World.Enemies;
using App.World.WorldGrid;
using System;
using UnityEngine;
namespace App.World.Buildings.PlaceableBuildings
{
    public abstract class PlaceableBuilding : MonoBehaviour, IObjectPoolItem
    {
        [SerializeField]
        private BuildingData basicData;
        [SerializeField]
        private Health healthComponent;
        private int level;
        private bool clickable = true;
        protected CellGrid cellGrid;
        protected ObjectPool objectPool;
        private PlayerMoney playerMoney;



        public BuildingData BasicData { get => basicData; }
        public int Level { get => level; set => level = value; }
        public bool Clickable { get => clickable; set => clickable = value; }

        public virtual string PoolObjectID { get => BasicData.poolObjectID; }
        public Health HealthComponent { get => healthComponent; set => healthComponent = value; }
        public PlayerMoney PlayerMoney { get => playerMoney;}

        public Action notifyGridWeightChanged;

        public virtual void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
        {
            this.cellGrid = cellGrid;
            this.playerMoney = playerMoney;
            transform.position = position;
            level = 0;
            healthComponent.MaxHP = basicData.health;
            healthComponent.CurHP = healthComponent.MaxHP;
        }

        public abstract void Upgrade();

        public virtual void Repair()
        {
            if (PlayerMoney.Money < basicData.upgradePrice)
            {
                //TODO play music
                return;
            }
            else PlayerMoney.Money -= basicData.upgradePrice;
            healthComponent.CurHP = healthComponent.MaxHP;
        }
        public void GetFromPool(ObjectPool pool)
        {
            objectPool = pool;
            gameObject.SetActive(true);
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }

}
