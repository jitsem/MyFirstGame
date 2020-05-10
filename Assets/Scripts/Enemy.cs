using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle, 
    Walk,
    Attack,
    Stagger, 
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    void Awake()          
    {
        health = maxHealth.initialValue;
    }

    public void Knock(Rigidbody2D myBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myBody, knockTime));
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    private IEnumerator KnockCo(Rigidbody2D myBody, float knockTime)
    {
        if(myBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myBody.velocity = Vector2.zero;
            currentState = EnemyState.Idle;
        }
    }
}
