using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Breakable")
        && gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }
        if(other.gameObject.CompareTag("Enemy")
        || other.gameObject.CompareTag("Player"))
        {
            var toHit = other.GetComponent<Rigidbody2D>();
            if(toHit != null)
            {    
                Vector2 diffVect = toHit.transform.position - transform.position;
                diffVect = diffVect.normalized * thrust;
                toHit.AddForce(diffVect, ForceMode2D.Impulse);

                if(other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    toHit.GetComponent<Enemy>().currentState = EnemyState.Stagger;
                    other.GetComponent<Enemy>().Knock(toHit, knockTime, damage);
                }

                if(other.gameObject.CompareTag("Player"))
                {
                    if(other.gameObject.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        toHit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}
