using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float healthIncrease;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            var newHealth = playerHealth.runTimeValue + healthIncrease;
            playerHealth.runTimeValue = Mathf.Min(newHealth, heartContainers.runTimeValue*2f);
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }

}
