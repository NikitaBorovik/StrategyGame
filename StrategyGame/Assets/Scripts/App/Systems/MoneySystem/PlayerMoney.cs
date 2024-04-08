using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.Systems.MoneySystem
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField]
        private int money;
        [SerializeField]
        private TextMeshProUGUI moneyTextCounter;

        private void Awake()
        {
            if (moneyTextCounter != null)
                moneyTextCounter.text = money.ToString();
        }

        public int Money 
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                if (moneyTextCounter != null)
                    moneyTextCounter.text = money.ToString();
            }
        }
    }

}

