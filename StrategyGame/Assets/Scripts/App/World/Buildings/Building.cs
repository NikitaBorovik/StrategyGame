using App.Systems.MoneySystem;
using App.World;
using App.World.Buildings.BuildingsSO;
using App.World.WorldGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour, IObjectPoolItem
{
    [SerializeField]
    private BuildingData basicData;
    private int level;
    private float health;
    private float currentHealth;
    private bool clickable = true;
    protected CellGrid cellGrid;
    protected ObjectPool objectPool;
    protected PlayerMoney playerMoney;
    

    public BuildingData BasicData { get => basicData;}
    public int Level { get => level; set => level = value; }
    public bool Clickable { get => clickable; set => clickable = value; }

    public virtual string PoolObjectID { get => BasicData.poolObjectID; }
    public float Health { get => health; set => health = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public Action notifyGridWeightChanged;

    public virtual void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
    {
        this.cellGrid = cellGrid;
        this.playerMoney = playerMoney;
        transform.position = position;
        level = 0;
        health = basicData.health;
    }

    public abstract void Upgrade();

    public virtual void Repair()
    {
        if (playerMoney.Money < basicData.upgradePrice)
        {
            //TODO play music
            return;
        }
        else playerMoney.Money -= basicData.upgradePrice;
        currentHealth = health;
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
