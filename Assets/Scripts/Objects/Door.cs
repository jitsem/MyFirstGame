using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button,
}


public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool isOpen = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D doorCollider;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(playerInRange && thisDoorType == DoorType.key)
            {
                if(playerInventory.nrOfKeys > 0)
                {
                    playerInventory.nrOfKeys--;
                    Open();
                }
            }
        }
    }
    
    public void Open()
    {
          doorSprite.enabled = false;
          doorCollider.enabled = false;
          isOpen = doorSprite;
    }
    
    public void Close()
    {
    }
}
