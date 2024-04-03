using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.Menu
{
    public class MenuOptionsSystem : MonoBehaviour
    {
        [SerializeField]
        private Animator mainMenuItemsAnimator;
        [SerializeField]
        private Animator tutorialAnimator;
        [SerializeField]
        private Animator levelSelectorAnimator;
        [SerializeField]
        private Animator faderAnimator;

        private void Awake()
        {
            PlayerPrefs.SetInt("Level1Opened", 1);
        }

        public void SelectLevelOption()
        {

            mainMenuItemsAnimator.SetBool("isIdle", false);
            levelSelectorAnimator.SetBool("isIdle", false);
            faderAnimator.SetBool("isIdle", false);

            mainMenuItemsAnimator.SetBool("isVisible", false);
            tutorialAnimator.SetBool("isVisible", false);
            levelSelectorAnimator.SetBool("isVisible", true);

            faderAnimator.SetBool("isVisible", true);
        }

        public void TutorialOption()
        {
            mainMenuItemsAnimator.SetBool("isIdle", false);
            tutorialAnimator.SetBool("isIdle", false);
            faderAnimator.SetBool("isIdle", false);

            mainMenuItemsAnimator.SetBool("isVisible", false);
            tutorialAnimator.SetBool("isVisible", true);
            levelSelectorAnimator.SetBool("isVisible", false);

            faderAnimator.SetBool("isVisible", true);
        }

        public void BackToMenuOption()
        {
            mainMenuItemsAnimator.SetBool("isVisible", true);
            tutorialAnimator.SetBool("isVisible", false);
            levelSelectorAnimator.SetBool("isVisible", false);

            faderAnimator.SetBool("isVisible", false);
        }

        public void ExitOption()
        {
            Application.Quit();
        }
    }

}

