using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public Pot[] pots;
    public CinemachineVirtualCamera virtualCamera;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            virtualCamera.gameObject.SetActive(true);
            virtualCamera.MoveToTopOfPrioritySubqueue();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            virtualCamera.gameObject.SetActive(false);
        }
    }

    protected void ChangeActivation(Component comp, bool active)
    {
        comp.gameObject.SetActive(active);
    }
}
