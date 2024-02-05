using App.Systems.MoneySystem;
using App.World;
using App.World.Buildings.BuildingsSO;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour, IObjectPoolItem
{
    [SerializeField]
    private BuildingData data;
    protected int level;
    protected bool clickable = true;
    protected CellGrid cellGrid;
    protected ObjectPool objectPool;
    protected PlayerMoney playerMoney;

    public BuildingData Data { get => data;}
    public int Level { get => level;}
    public bool Clickable { get => clickable; set => clickable = value; }

    public virtual string PoolObjectID { get => Data.poolObjectID; }

    public virtual void Init(Vector2 position, CellGrid cellGrid, PlayerMoney playerMoney)
    {
        this.cellGrid = cellGrid;
        this.playerMoney = playerMoney;
        transform.position = position;
        level = 0;
    }

    public virtual void Upgrade()
    {
        if (playerMoney.Money < data.upgradePrice)
        {
            //TODO PLAY SOME SOUND
            return;
        }
        playerMoney.Money -= data.upgradePrice;
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
