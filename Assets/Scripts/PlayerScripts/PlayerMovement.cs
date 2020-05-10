using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger,
}
public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    public FloatValue currentHealth;
    public VectorValue startingPosition;
    public Signal PlayerHealthSignal;
    public Inventory playerInventory;

    public SpriteRenderer receivedItemSprite;

    private Rigidbody2D m_RidigBody;
    private Vector3 change;
    private Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_RidigBody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("moveX", 0);
        m_Animator.SetFloat("moveY", -1);
        currentState = PlayerState.walk;
        transform.position = startingPosition.startValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == PlayerState.interact)
            return;
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack")
        && currentState != PlayerState.attack
        && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk
        || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        currentState = PlayerState.attack;
        m_Animator.SetBool("attacking", true);
        yield return null;
        m_Animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.33f);
        if (currentState != PlayerState.interact)
            currentState = PlayerState.walk;
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                m_Animator.SetBool("receivedItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                m_Animator.SetBool("receivedItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }
    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            m_Animator.SetFloat("moveX", change.x);
            m_Animator.SetFloat("moveY", change.y);
            m_Animator.SetBool("moving", true);
        }
        else
        {
            m_Animator.SetBool("moving", false);
        }
    }
    void MoveCharacter()
    {
        change.Normalize();
        m_RidigBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.runTimeValue -= damage;
        PlayerHealthSignal.Raise();
        if (currentHealth.runTimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }
    private IEnumerator KnockCo(float knockTime)
    {
        if (m_RidigBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            m_RidigBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }
}
