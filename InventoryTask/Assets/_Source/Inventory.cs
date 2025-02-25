using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : IInventory
{
    private Dictionary<ISlot, AItem> data;

    public Inventory(ISlot[] slots)
    {
        data = new Dictionary<ISlot, AItem>();
        Init(slots);
    }

    private void Init(ISlot[] slots)
    {
        foreach (ISlot slot in slots)
        {
            data.Add(slot, null);
        }
    }

    public void AddItem(AItem item, int amount)
    {
        foreach (ISlot slot in data.Keys)
        {
            if (data[slot] == null)
            {
                data[slot] = item;
            }
        }
    }

    public void Clear()
    {
        foreach (ISlot slot in data.Keys)
        {
            data[slot] = null;
        }
    }

    public T[] FindAllItemsByType<T>()
    {
        throw new System.NotImplementedException();
    }

    public T FindItemByType<T>()
    {
        throw new System.NotImplementedException();
    }

    public AItem GetItemFromSlot(ISlot slot)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveItem(AItem item, int amount)
    {
        throw new System.NotImplementedException();
    }
}
