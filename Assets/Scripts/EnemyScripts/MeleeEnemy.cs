using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{
    protected override void CheckDistance()
    {
        if (target.GetComponent<PlayerMovement>().currentState == PlayerState.diying)
            return;
        if (Vector3.Distance(target.transform.position, transform.position) > chaseRadius)
            return;
        if (Vector3.Distance(target.transform.position, transform.position) >= attackRadius)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk)
            {
                ChangeState(EnemyState.Walk);
                var temp = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
            }
        }
        else if(currentState != EnemyState.Attack)
        {
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        ChangeState(EnemyState.Attack);
        m_Animator.SetBool("attacking", true);
        yield return new WaitForSeconds(1f);
        m_Animator.SetBool("attacking", false);
        ChangeState(EnemyState.Walk);

    }
}
