using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Utilities
{
    public class DisableSelf : MonoBehaviour
    {
        public void MakeInactive()
        {
            gameObject.SetActive(false);
        }
    }

}
