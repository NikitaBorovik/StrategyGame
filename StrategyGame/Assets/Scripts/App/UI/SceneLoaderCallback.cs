using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI
{
    public static class SceneLoaderCallback
    {
        public static Action callback;

        public static void InvokeSelf()
        {
            callback?.Invoke();

        }
    }

}
