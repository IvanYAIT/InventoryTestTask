using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Inventory : IInventory
{
    private Dictionary<ISlot, AItem> data;

    [Inject]
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
        Debug.Log("init");
    }

    public bool AddItem(AItem item, int amount)
    {
        ISlot newSlot = FindItemSlotByType(item, true);
        if (newSlot != null)
        {
            int remains = data[newSlot].Add(amount);
            if (remains <= 0)
                return true;
            else
                return AddItem(item, remains);
        }

        newSlot = FindEmptySlot();
        if (newSlot != null)
        {
            data[newSlot] = item;
            data[newSlot].SetAmount(amount);
            return true;
        }
        Debug.Log("Inventory is full");
        return false;
    }

    public bool RemoveItem(AItem item, int amount)
    {
        ISlot newSlot = FindItemSlotByType(item, false);
        if (newSlot != null)
        {
            int remains = data[newSlot].Subtract(amount);
            if (remains <= 0)
                return true;
            else
            {
                data[newSlot] = null;
                return RemoveItem(item, remains);
            }
        }
        Debug.Log("Inventory is empty");
        return false;
    }

    public void Clear()
    {
        foreach (ISlot slot in data.Keys)
        {
            data[slot] = null;
        }
    }

    public List<ISlot> FindAllItemSlotsByType<T>() where T : AItem
    {
        List<ISlot> slots = new List<ISlot>();
        foreach (ISlot slot in data.Keys)
        {
            if (data[slot].GetType() == typeof(T))
                slots.Add(slot);
        }

        if(slots.Count > 0)
            return slots;
        else
            return null;
    }

    public ISlot FindItemSlotByType<T>(bool notFullStack) where T : AItem
    {
        foreach (ISlot slot in data.Keys)
        {
            if (data[slot].GetType() == typeof(T))
            {
                if (!notFullStack)
                    return slot;
                else
                {
                    if (data[slot].GetAmount() != data[slot].MaxAmount)
                        return slot;
                }
            }
        }
        return null;
    }

    public ISlot FindItemSlotByType<T>(T item, bool notFullStack) where T : AItem
    {
        foreach (ISlot slot in data.Keys)
        {
            if (data[slot].GetType() == typeof(T))
            {
                if(!notFullStack)
                    return slot;
                else
                {
                    if (data[slot].GetAmount() != data[slot].MaxAmount)
                        return slot;
                }
            }
        }
        return null;
    }

    public ISlot FindEmptySlot()
    {
        foreach (ISlot slot in data.Keys)
        {
            if (data[slot] == null)
                return slot;
        }
        return null;
    }
}
