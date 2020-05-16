using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public GameObject target;
    public float chaseRadius;
    public float attackRadius;
    protected Animator m_Animator;
    protected Rigidbody2D m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.Idle;
        m_Animator.SetBool("wakeUp", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= chaseRadius
        && Vector3.Distance(target.transform.position, transform.position) >= attackRadius
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
        || target.GetComponent<PlayerMovement>().currentState == PlayerState.diying)
        {
            m_Animator.SetBool("wakeUp", false);
        }
    }

    protected void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
            return;
        currentState = newState;
    }

    protected void ChangeAnim(Vector2 direction)
    {        
        direction = direction.normalized;
        m_Animator.SetFloat("moveX", direction.x);
        m_Animator.SetFloat("moveY", direction.y);
    }
}
