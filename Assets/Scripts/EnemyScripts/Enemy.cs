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
    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Stats")] 
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 1f;

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
            DeathEffect();
            this.gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if(deathEffect != null)
        {
            var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
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
