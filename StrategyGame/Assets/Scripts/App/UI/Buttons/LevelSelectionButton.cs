using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LevelSelectionButton : MonoBehaviour
    {
        private Button button;

        [SerializeField]
        private int level;

        [SerializeField]
        private Sprite enabledLevelSprite;

        [SerializeField]
        private Sprite disabledLevelSprite;

        void Start()
        {
            button = GetComponent<Button>();

            if (PlayerPrefs.GetInt($"Level{level}Opened", 0) != 0)
            {
                button.image.sprite = enabledLevelSprite;
                button.enabled = true;
            }
            else
            {
                button.image.sprite = disabledLevelSprite;
                button.enabled = false;
            }
        }
    }

}

