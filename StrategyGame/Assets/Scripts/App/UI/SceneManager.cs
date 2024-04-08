using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.UI
{
    public class SceneManager : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 1f;
        }
        public void LoadSceneWithLoading(string name)
        {
            SceneLoaderCallback.callback = () => LoadScene(name);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }

        public void LoadScene(string name)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
        }

        public void ReloadScene()
        {
            LoadSceneWithLoading(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

}
