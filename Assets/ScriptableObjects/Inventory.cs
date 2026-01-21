using UnityEngine;
using System.Collections.Generic;

public class Inventory
{
    private List<Items> itemList;
    public IReadOnlyList<Items> InventoryContents => itemList;

    private int count;

    public Inventory()
    {
        itemList = new List<Items>();
    }

    public void Add(Items item)
    {
        itemList.Add(item);
    }

    public void Remove(Items item)
    {
        itemList.Remove(item);
    }

    public bool Contains(Items item)
    {
        return itemList.Contains(item);
    }

    public int CountItems(Items item)
    {
        count = 0;
        foreach (Items i in itemList)
        {
            if (i.itemName == item.itemName)
            {
                count++;
            }
        }
        return count;
    }
}
