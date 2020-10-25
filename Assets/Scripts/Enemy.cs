using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;


namespace TowerDefence.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("General Stats")]
        [SerializeField, Tooltip("how fast the enemy will move")]
        private float speed = 1;
        [SerializeField, Tooltip("How much the damage the enemy can take before dying")]
        private float health = 1;
        [SerializeField, Tooltip("How much damage the enemy will do to the players health")]
        private float damage = 1;

        [Header("Rewards")]
        [SerializeField]
        private int money = 1;

        private Player player;

        public void Damage(Tower _tower)
        {
            //loses health based on tower damage
            health -= _tower.Damage;
            if (health <= 0)//if health reaches 0 then the enemy dies
            {
                Die(_tower);
            }
        }

        private void Die(Tower _tower)
        {
            player.AddMoney(money);//on death add money to player
        }

        private void Start()
        {
            player = Player.instance;
        }
    }
}
