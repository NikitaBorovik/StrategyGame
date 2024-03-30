using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Tutorial
{
    [CreateAssetMenu(fileName = "TutorialDataSO", menuName = "Scriptable Objects/Tutorial/Tutorial Page Data")]
    public class TutorialDataSO : ScriptableObject
    {
        public Sprite tutorialPageImage;
        [TextArea(3, 10)]
        public string tutorialPageInfo;
    }

}
