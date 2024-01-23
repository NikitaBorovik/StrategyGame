using App.World.Buildings.BuildingsSO;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    private BuildingData data;
    protected CellGrid cellGrid;

    public BuildingData Data { get => data;}

    public virtual void Init(Vector2 position, CellGrid cellGrid)
    {
        this.cellGrid = cellGrid;
        transform.position = position;
    }

    public abstract void Upgrade();
}
