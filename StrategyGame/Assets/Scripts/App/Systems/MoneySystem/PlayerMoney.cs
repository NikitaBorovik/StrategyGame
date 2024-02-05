using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems.MoneySystem
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField]
        private int money;

        public int Money 
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                Debug.Log(money);
            }
        }
    }

}

