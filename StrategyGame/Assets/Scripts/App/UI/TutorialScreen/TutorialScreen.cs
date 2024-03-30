using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Tutorial
{
    public class TutorialScreen : MonoBehaviour
    {
        [SerializeField] 
        private Button nextPageButton;

        [SerializeField] 
        private Button prevPageButton;

        [SerializeField]
        private Image tutorialPageImage;

        [SerializeField]
        private TextMeshProUGUI tutorialPageText;

        [SerializeField]
        private List<TutorialDataSO> pages;

        private int currentPage;

        private void Awake()
        {
            currentPage = 0;
            prevPageButton.interactable = currentPage > 0;
            nextPageButton.interactable = currentPage < pages.Count - 1;

            SetUpDisplayData();
        }

        public void NextPage()
        {
            if (currentPage < pages.Count - 1) 
            { 
                currentPage++;
                nextPageButton.interactable = currentPage < pages.Count - 1;
                prevPageButton.interactable = currentPage > 0;

                SetUpDisplayData();
            }
        }

        public void PrevPage()
        {
            if(currentPage > 0)
            {
                currentPage--;
                nextPageButton.interactable = currentPage < pages.Count - 1;
                prevPageButton.interactable = currentPage > 0;

                SetUpDisplayData();
            }
        }

        private void SetUpDisplayData()
        {
            tutorialPageImage.sprite = pages[currentPage].tutorialPageImage;
            tutorialPageText.text = pages[currentPage].tutorialPageInfo;
        }
    }

}
