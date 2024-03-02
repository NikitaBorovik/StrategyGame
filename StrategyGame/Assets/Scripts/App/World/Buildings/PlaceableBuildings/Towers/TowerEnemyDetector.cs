using App.World.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings.PlaceableBuildings.Towers
{
    public class TowerEnemyDetector : MonoBehaviour
    {
        [SerializeField]
        private Tower parent;
        [SerializeField]
        private CircleCollider2D collider2D;

        public CircleCollider2D Collider2D { get => collider2D; set => collider2D = value; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
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

}
