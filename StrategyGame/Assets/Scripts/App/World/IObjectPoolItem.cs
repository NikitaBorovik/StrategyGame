using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World
{
    public interface IObjectPoolItem
    {
        string PoolObjectID { get; }
        void GetFromPool(ObjectPool pool);
        void ReturnToPool();
        GameObject GetGameObject();
    }

}
