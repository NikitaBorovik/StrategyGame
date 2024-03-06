using App.World.Enemies;
using UnityEngine;

namespace App.World.Utilities
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Health health;
        [SerializeField]
        private GameObject fill;

        private void OnEnable()
        {
            fill.transform.localScale = new Vector3(1, 1);
            health.OnHpChanged += ScaleHealthbarFill;
        }

        private void OnDisable()
        {
            health.OnHpChanged -= ScaleHealthbarFill;
        }

        private void ScaleHealthbarFill()
        {
            Debug.Log(health.CurHP / health.MaxHP);
            fill.transform.localScale = new Vector3(health.CurHP / health.MaxHP, 1);
        }

    }

}
