using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Utilities
{
    public class AppCloser : MonoBehaviour
    {
        public void CloseApp()
        {
            Application.Quit();
        }
    }

}
