using App.World;
using App.World.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Buildings
{
    [RequireComponent(typeof(Health))]
    public class MainCastle : MonoBehaviour, IDestroyable
    {
        [SerializeField]
        private GameEndScreen defeatedScreen;
        [SerializeField]
        private Health healthComponent;
        [SerializeField]
        private int health;

        public void DestroySequence()
        {
            defeatedScreen.EndGame();
        }

        void Start()
        {
            healthComponent.MaxHP = health;
            healthComponent.CurHP = health;
        }


    }

}
