using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField]
        private GameObject overallShadow;
        [SerializeField]
        private GameObject pauseBoard;
        private bool isPaused = false;

        public bool IsPaused { get => isPaused; set => isPaused = value; }

        public void TogglePause()
        {
            overallShadow.GetComponent<Animator>().SetBool("isIdle", false);
            pauseBoard.GetComponent<Animator>().SetBool("isIdle", false);
            if (!IsPaused)
            {
                overallShadow.GetComponent<Animator>().SetBool("isVisible", true);
                pauseBoard.GetComponent<Animator>().SetBool("isVisible", true);
                Time.timeScale = 0;
            }
            else
            {
                overallShadow.GetComponent<Animator>().SetBool("isVisible", false);
                pauseBoard.GetComponent<Animator>().SetBool("isVisible", false);
                Time.timeScale = 1f;
            }
            IsPaused = !IsPaused;
        }
    }

}
