using App.World.Buildings.Towers;
using App.World.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEnemyDetector : MonoBehaviour
{
    [SerializeField]
    private Tower parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        Debug.Log("Enter");
        if (enemy != null)
        {
            parent.DetectedEnemies.Add(enemy);
            if (parent.CurrentTarget == null)
            {
                parent.CurrentTarget = enemy;
                parent.RefreshSoldiersTarget();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.NotifyDiedlist.Remove(parent);
            parent.DetectedEnemies.Remove(enemy);
            if (parent.DetectedEnemies.Count != 0)
            {
                parent.CurrentTarget = parent.DetectedEnemies[0];
            }
            else
            {
                parent.CurrentTarget = null;
            }
            parent.RefreshSoldiersTarget();
        }
    }
}
