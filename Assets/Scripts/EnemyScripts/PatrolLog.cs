using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{

    public Transform[] path;
    public int currentPoint;
    public float roundingDistance;
    protected override void CheckDistance()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius
        && Vector3.Distance(target.transform.position, transform.position) >= attackRadius        
        && target.GetComponent<PlayerMovement>().currentState != PlayerState.diying)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk)
            {
                var temp = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, path[currentPoint].position) >= roundingDistance)
            {
                var temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if(currentPoint == path.Length -1)
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint++;
        }
    }
}
