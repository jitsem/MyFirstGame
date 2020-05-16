using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDungeonRoom : DungeonRoom
{
    public Door[] doors;

    public void CheckEnemies()
    {
        StartCoroutine(CheckEnemiesCo());
    }
    private IEnumerator CheckEnemiesCo()
    {
        yield return new WaitForSeconds(3);
        if(enemies.Where(e=>e.gameObject.activeInHierarchy).Count() == 0)
                OpenDoors();
    }

    public void CloseDoors()
    {
        foreach (var door in doors)
        {
            door.Close();
        }
    }
    public void OpenDoors()
    {
        foreach (var door in doors)
        {
            door.Open();
        }
    }
    protected override void OnTriggerEnter2D(Collider2D other)
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
            CloseDoors();
        }
    }

    protected override void OnTriggerExit2D(Collider2D other) 
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
}
