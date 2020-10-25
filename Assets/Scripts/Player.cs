﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;
using TowerDefence.Enemies;

namespace TowerDefence
{

    public class Player : MonoBehaviour
    {
        public static Player instance = null;

        [SerializeField]
        private int money = 100;//money player starts with

        public void AddMoney(int _money)
        {
            money += _money;// adds money to player method
        }

        public void PurchaseTower(Tower _tower)
        {
            money -= _tower.Cost;//player loses money when buying towers
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}
