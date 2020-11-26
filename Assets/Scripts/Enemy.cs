using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;


public enum State { Move, Attack } // State Machine

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
    private float minAttackDistance = 0.2f;

    public GameObject enemy;

    [Header("Rewards")]
    [SerializeField]
    private int money = 1;

    private Player player; //reference to player script


    public State state; // reference to the state machine

    private void Start()
    {
        player = Player.instance;
        NextState();//starts the state machine
    }

    /// <summary>
    /// Move State that moves enemy to base
    /// </summary>
    /// <returns>Attack state if distance is less than attack distance</returns>
    private IEnumerator MoveState()
    {
        
        while (state == State.Move)
        {
            Move();
            yield return null;
            float distance = Vector2.Distance(transform.position, player.playerBase.transform.position);
            if (distance <= minAttackDistance)
            {
                state = State.Attack;
            }
        }
        NextState();
    }

    /// <summary>
    /// Attack state that reduces player health
    /// </summary>
    /// <returns>if player health is 0 then game over</returns>
    private IEnumerator AttackState()
    {
        while (state == State.Attack)
        {
            Attack();
            yield return null;
            if (player.health == 0)
            {
                Time.timeScale = 0;
            }
        }
        NextState();
    }

    /// <summary>
    /// reduced player health * Time.deltatime
    /// </summary>
    public void Attack()
    {
        player.health -= damage * Time.deltaTime;
    }

    /// <summary>
    /// Damage the tower does to the enemy
    /// </summary>
    /// <param name="_tower">what tower is attacking</param>
    public void Damage(float damage)
    {
        //loses health based on tower damage
        health -= damage;
        if (health <= 0)//if health reaches 0 then the enemy dies
        {
            Die();
        }
    }

    private void Die()//Tower _tower)
    {
        player.AddMoney(money);//on death add money to player
        EnemyManager.instance.enemies.Remove(gameObject.GetComponent<Enemy>());
        Destroy(gameObject);
    }

   
    /// <summary>
    /// Moves the enemy to the player base as long as the player base still has health
    /// </summary>
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
                             player.playerBase.transform.position, speed * 10f * Time.deltaTime);

    }

    /// <summary>
    /// switches to the next state
    /// </summary>
    public void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                   System.Reflection.BindingFlags.NonPublic |
                                   System.Reflection.BindingFlags.Instance);

        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }
}

