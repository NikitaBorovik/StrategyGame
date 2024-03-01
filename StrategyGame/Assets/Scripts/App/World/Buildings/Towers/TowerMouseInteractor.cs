using App.World.Buildings.Towers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace App.World.Buildings.Towers
{
    public class TowerMouseInteractor : MonoBehaviour
    {
        [SerializeField]
        private GameObject selectWarriorButtons;
        [SerializeField]
        private Tower parent;
        private void OnMouseDown()
        {
            if (parent.Clickable)
            {
                Animator animator = selectWarriorButtons.GetComponent<Animator>();
                if (selectWarriorButtons.activeSelf)
                {
                    animator.SetBool("IsVisible", false);
                }
                else
                {
                    selectWarriorButtons.SetActive(true);
                    animator.SetBool("IsVisible", true);
                }
            }

        }
    }
}

