using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int nrOfKeys;

    public void AddItem(Item toAdd)
    {
        if(toAdd.isKey)
        {
            nrOfKeys++;
        }
        else
        {
            if(!items.Contains(toAdd))
            {
                items.Add(toAdd);
            }
        }
    }
}
 