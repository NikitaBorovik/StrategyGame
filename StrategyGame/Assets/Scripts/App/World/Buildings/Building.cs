using App.World.Buildings.BuildingsSO;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    private BuildingData data;
    protected int level;
    protected bool clickable = true;
    protected CellGrid cellGrid;

    public BuildingData Data { get => data;}
    public int Level { get => level;}
    public bool Clickable { get => clickable; set => clickable = value; }

    public virtual void Init(Vector2 position, CellGrid cellGrid)
    {
        this.cellGrid = cellGrid;
        transform.position = position;
        level = 0;
    }

    public abstract void Upgrade();
}
