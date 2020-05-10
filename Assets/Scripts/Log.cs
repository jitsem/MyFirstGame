using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator m_Animator;

    private Rigidbody2D m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = EnemyState.Idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
        && Vector3.Distance(target.position, transform.position) >= attackRadius)
        {
            if (currentState == EnemyState.Idle || currentState == EnemyState.Walk)
            {
                ChangeState(EnemyState.Walk);
                m_Animator.SetBool("wakeUp", true);
                var temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                m_RigidBody.MovePosition(temp);
            }
        }
        else
        {
            m_Animator.SetBool("wakeUp", false);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
            return;
        currentState = newState;
    }

    private void ChangeAnim(Vector2 direction)
    {
        
        direction = direction.normalized;
        m_Animator.SetFloat("moveX", direction.x);
        m_Animator.SetFloat("moveY", direction.y);
        // if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        // {
        //     if(direction.x > 0)
        //     {

        //     }
        //     else if(direction.x < 0)
        //     {

        //     }
        // }
        // else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        // {
        //     if(direction.y > 0)
        //     {

        //     }
        //     else if(direction.y < 0)
        //     {

        //     }
        // }
    }
}
