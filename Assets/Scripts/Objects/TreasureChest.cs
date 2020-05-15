using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    public BoolValue storedOpen;

    private Animator m_Animator;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        isOpen = storedOpen.runTimeValue;
        if (isOpen)
        {
            m_Animator.SetBool("opened", true);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                OpenChestAlreadyOpen();
            }
        }
    }
    public void OpenChest()
    {
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        raiseItem.Raise();
        context.Raise();
        isOpen = true;
        storedOpen.runTimeValue = isOpen;
        m_Animator.SetBool("opened", true);
    }


    public void OpenChestAlreadyOpen()
    {
        dialogBox.SetActive(false);
        raiseItem.Raise();

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }

}
