using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ButtonDisableOnPause : MonoBehaviour
    {
        private Button self;
        private void Awake()
        {
            self = GetComponent<Button>();
        }
        private void Update()
        {
            if (Time.deltaTime == 0)
            {
                self.interactable = false;
            }
            else
            {
                self.interactable = true;
            }
        }
    }

}

