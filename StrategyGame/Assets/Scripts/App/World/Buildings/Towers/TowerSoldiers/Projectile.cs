using App.Systems.BattleWaveSystem;
using App.World;
using App.World.Enemies.States;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IObjectPoolItem
{
    [SerializeField]
    private string poolObjectID;
    private ObjectPool objectPool;
    private float velocity;
    private float damage;
    private DamageAttribute damageAttribute;
    private Transform target;
    private Rigidbody2D rb;

    public string PoolObjectID => poolObjectID;

    private void Update()
    {
        rb.velocity = (target.position - transform.position).normalized * velocity;
        transform.right = (target.position - transform.position).normalized;
        if ((target.position - transform.position).magnitude < 0.1f)
        {
            objectPool.ReturnToPool(this);
            print("Hit enemy");
        }
    }

    public void Init(Transform target, Vector3 position, float velocity, float damage, DamageAttribute damageAttribute)
    {
        this.target = target;
        transform.position = position;
        transform.right = (target.position - transform.position).normalized;
        this.velocity = velocity;
        this.damage = damage;
        this.damageAttribute = damageAttribute;
        this.rb = GetComponent<Rigidbody2D>();
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
