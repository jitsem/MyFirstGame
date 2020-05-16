using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
    public PowerUp DropLoot()
    {
        var cumProb = 0;
        var currentProb = Random.Range(0, 100);
        foreach (var loot in loots)
        {
            cumProb += loot.SpawnChance;
            if(currentProb <= cumProb)
            {
                return loot.thisLoot;
            }
        }
        return null;
    }
}
