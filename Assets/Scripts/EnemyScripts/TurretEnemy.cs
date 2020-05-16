using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelayInSeconds;
    private bool canFire = true;
    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius
        && Vector3.Distance(target.transform.position, transform.position) >= attackRadius
        && target.GetComponent<PlayerMovement>().currentState != PlayerState.diying)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk)
            {
                if (canFire)
                {
                    var tempVec = target.transform.position - transform.position;
                    var currentProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                    currentProjectile.GetComponent<Projectile>().Launch(tempVec);
                    canFire = false;
                }
                ChangeState(EnemyState.Walk);
                m_Animator.SetBool("wakeUp", true);
            }
        }
        else if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius
        || target.GetComponent<PlayerMovement>().currentState == PlayerState.diying)
        {
            m_Animator.SetBool("wakeUp", false);
        }
    }
    void Update()
    {
        fireDelayInSeconds -= Time.deltaTime;
        if (fireDelayInSeconds <= 0)
        {
            canFire = true;
            fireDelayInSeconds = fireDelay;
        }
    }
}
