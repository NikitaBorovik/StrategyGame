using App.Systems.BattleWaveSystem;
using App.World;
using App.World.Enemies;
using App.World.Enemies.States;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        if (target == null)
        {
            objectPool.ReturnToPool(this);
            return;
        }
        rb.velocity = (target.position - transform.position).normalized * velocity;
        transform.right = (target.position - transform.position).normalized;
        if ((target.position - transform.position).magnitude < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        objectPool.ReturnToPool(this);
        Health health = target.GetComponent<Health>();
        if (health == null)
            Debug.Log("Hitting target without health component");
        else
        {
            health.TakeDamage(damage);
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
