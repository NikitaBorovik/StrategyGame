using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

namespace App.World.Enemies
{
    public class Health : MonoBehaviour
    {
        private float curHP;
        private float maxHP;
        private Color32 colorWhenHit = new Color32(215, 115, 115, 255);
        private List<SpriteRenderer> renderers;
        private List<SpriteRenderer> deletable;
        private Coroutine blinking;
        private float blinkingDuration = 0.1f;

        public float CurHP { get => curHP; set => curHP = value; }
        public float MaxHP { get => maxHP; set => maxHP = value; }

        public void Awake()
        {
            renderers = new List<SpriteRenderer>();
            deletable = new List<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            {
                renderers.Add(spriteRenderer);
            }
        }
        public void TakeDamage(float damage)
        {
            CurHP -= damage;
            if(curHP <= 0)
            {
                IDestroyable destroyable = GetComponent<IDestroyable>();
                if(destroyable != null)
                    destroyable.DestroySequence();
            }
            Blink();
        }

        private IEnumerator BlinkCoroutine()
        {
            if (renderers == null)
                yield break;
            foreach (SpriteRenderer spriteRenderer in renderers)
            {      
                spriteRenderer.color = colorWhenHit;
            }
            yield return new WaitForSeconds(blinkingDuration);
            RemoveBlinkingColor();
            blinking = null;
        }

        private void RemoveBlinkingColor()
        {
            foreach (SpriteRenderer spriteRenderer in renderers)
            {
                if (spriteRenderer != null)
                    spriteRenderer.color = new Color32(255, 255, 255, 255); ;
            }
        }

        private void Blink()
        {
            if (blinking != null)
            {
                StopCoroutine(blinking);
            }
            foreach (SpriteRenderer spriteRenderer in deletable)
            {
                renderers.Remove(spriteRenderer);
            }
            deletable.Clear();

            if (gameObject.activeSelf)
            {
                blinking = StartCoroutine(BlinkCoroutine());
            }
        }
    }

}
