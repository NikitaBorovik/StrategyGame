using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        float minimalTimeToLoad;
        float timer;
        void Start()
        {
            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= minimalTimeToLoad)
                SceneLoaderCallback.InvokeSelf();
        }

    }

}
