using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedActive;
    public Sprite activeSprite;
    public Door linkedDoor;
    private SpriteRenderer m_SpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        active = storedActive.runTimeValue;
        if(active)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {        
            active = true;
            storedActive.runTimeValue = active;
            linkedDoor.Open();
            m_SpriteRenderer.sprite = activeSprite;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
