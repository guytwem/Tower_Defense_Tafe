using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;




public class Enemy : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField, Tooltip("how fast the enemy will move")]
    private float speed = 1;
    [SerializeField, Tooltip("How much the damage the enemy can take before dying")]
    private float health = 1;
    [SerializeField, Tooltip("How much damage the enemy will do to the players health")]
    private float damage = 1;
    [SerializeField, Tooltip("Distance before the enemy starts attacking")]
    private float minAttackDistance = 1;
    [SerializeField]
    private float attackDamage = 1;

    [Header("Rewards")]
    [SerializeField]
    private int money = 1;

    Player player;
    EnemyManager enemyManager;



    public enum State { Move, Attack }

    public State state;

    

    private IEnumerator MoveState()
    {
        while (state == State.Move)
        {
            Move();
            yield return null;
            if (Vector2.Distance(enemyManager.enemy.transform.position, player.playerBase.transform.position) < minAttackDistance)
            {
                state = State.Attack;
            }
        }
        NextState();
    }

    private IEnumerator AttackState()
    {
        while (state == State.Attack)
        {
            Attack();
            yield return null;
            if (player.health == 0)
            {
                //Game Over
            }
        }
        NextState();
    }

    public void Attack()
    {
        player.health -= attackDamage * Time.deltaTime;
    }

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
        NextState();
    }

    private void Move()
    {
        if(player.health > 0)
        {
            enemyManager.enemy.transform.position = Vector3.MoveTowards
            (enemyManager.enemy.transform.position, player.playerBase.transform.position, speed * Time.deltaTime);
        }
        
    }

    private void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}

