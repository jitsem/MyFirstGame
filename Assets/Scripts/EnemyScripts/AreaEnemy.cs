using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;
    
    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius
        && Vector3.Distance(target.transform.position, transform.position) >= attackRadius
        && boundary.bounds.Contains(target.transform.position)
        && target.GetComponent<PlayerMovement>().currentState != PlayerState.diying)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk)
            {
                ChangeState(EnemyState.Walk);
                m_Animator.SetBool("wakeUp", true);
                var temp = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
            }
        }
        else if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius
        || !boundary.bounds.Contains(target.transform.position)
        || target.GetComponent<PlayerMovement>().currentState == PlayerState.diying)
        {
            m_Animator.SetBool("wakeUp", false);
        }
    }
}
