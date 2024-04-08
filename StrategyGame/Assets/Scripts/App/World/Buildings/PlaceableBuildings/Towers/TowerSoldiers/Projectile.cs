using App.Systems.BattleWaveSystem;
using App.World;
using App.World.Enemies;
using App.World.Enemies.States;
using App.World.WorldGrid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Enemy target;
    private Rigidbody2D rb;

    public string PoolObjectID => poolObjectID;

    private void Update()
    {
        if (target == null)
        {
            objectPool.ReturnToPool(this);
            return;
        }
        rb.velocity = (target.transform.position - transform.position).normalized * velocity;
        transform.right = (target.transform.position - transform.position).normalized;
        if ((target.transform.position - transform.position).magnitude < 0.1f)
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
            health.TakeDamage(DecreaseDamageForEnemy(target, damage));
        }
    }
    private float DecreaseDamageForEnemy(Enemy enemy, float damageToDeal)
    {
        var resist = enemy.Data.resistances.FirstOrDefault(resistance => resistance.attribute == damageAttribute);
        if (resist != null)
        {
            return damageToDeal * (1 - resist.resistance); 
        }
        return damageToDeal;
    }

    public void Init(Enemy target, Vector3 position, float velocity, float damage, DamageAttribute damageAttribute)
    {
        this.target = target;
        transform.position = position;
        transform.right = (target.transform.position - transform.position).normalized;
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
