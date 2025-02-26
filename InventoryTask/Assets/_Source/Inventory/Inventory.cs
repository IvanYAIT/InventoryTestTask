using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[System.Serializable]
public class Inventory : IInventory
{
    private Dictionary<ISlot, AItem> _data;
    private InventoryView _view;
    private List<SerializableData> loadedItems;

    [Inject]
    public Inventory(InventoryView view)
    {
        _view = view;
        loadedItems = JsonInventorySaver.Load();
        _data = new Dictionary<ISlot, AItem>();
        if (loadedItems == null)
        {
            Init(view.Slots);
        }
        else
        {
            ShowLoadedData(view.Slots);
        }
        //_data = new Dictionary<ISlot, AItem>();
        //Init(view.Slots);
    }

    private void Init(ISlot[] slots)
    {
        foreach (ISlot slot in slots)
        {
            _data.Add(slot, null);
            _view.ShowItem(slot, _data[slot]);
        }
        Debug.Log("init");
    }

    private void ShowLoadedData(ISlot[] slots)
    {
        int counter = 0;
        foreach (ISlot slot in slots)
        {
            Debug.Log(loadedItems[counter]);
            if (loadedItems[counter] != null)
            {
                AItem item = ScriptableObject.Instantiate(Resources.Load<AItem>("Items/" + loadedItems[counter].itemType));
                item.SetAmount(loadedItems[counter].amount);
                _data.Add(slot, item);
            }
            else
                _data.Add(slot, null);

            _view.ShowItem(slot, _data[slot]);
            counter++;
        }
    }

    public bool AddItem(AItem item, int amount)
    {
        AItem newItem = ScriptableObject.Instantiate(item);

        ISlot newSlot = FindItemSlotByItem(item, true);
        if (newSlot != null)
        {
            int remains = _data[newSlot].Add(amount);
            _view.ShowItem(newSlot, _data[newSlot]);
            JsonInventorySaver.Save(_data.Values.ToArray());
            if (remains < 0)
                return true;
            else
            {
                newItem.SetAmount(remains);
                return AddItem(newItem, remains);
            }
        }

        newSlot = FindEmptySlot();
        if (newSlot != null)
        {
            _data[newSlot] = newItem;
            _data[newSlot].SetAmount(amount);
            _view.ShowItem(newSlot, _data[newSlot]);
            JsonInventorySaver.Save(_data.Values.ToArray());

            return true;
        }
        Debug.Log("Inventory is full");
        return false;
    }

    public bool RemoveItem(AItem item, int amount)
    {
        ISlot newSlot = FindItemSlotByItem(item, false);
        if (newSlot != null)
        {
            int remains = _data[newSlot].Subtract(amount);
            _view.ShowItem(newSlot, _data[newSlot]);
            JsonInventorySaver.Save(_data.Values.ToArray());

            if (remains < 0)
            {
                return true;
            }
            else
            {
                _data[newSlot] = null;
                _view.ShowItem(newSlot, _data[newSlot]);
                JsonInventorySaver.Save(_data.Values.ToArray());

                if (remains > 0)
                    return RemoveItem(item, remains);
                else
                    return true;
            }
        }
        Debug.Log("Inventory is empty");
        return false;
    }

    public bool RemoveItemFromSlot(ISlot slot, int amount)
    {
        if (slot.IsLocked())
            return false;
        if (_data[slot] != null)
        {
            int remains = _data[slot].Subtract(amount);
            Debug.Log(remains);
            _view.ShowItem(slot, _data[slot]);
            JsonInventorySaver.Save(_data.Values.ToArray());

            if (remains > 0)
            {
                _data[slot] = null;
                _view.ShowItem(slot, _data[slot]);
                JsonInventorySaver.Save(_data.Values.ToArray());

                return true;
            }
            return true;
        }

        Debug.Log("Slot is empty");
        return false;
    }

    public void Clear()
    {
        foreach (ISlot slot in _data.Keys)
        {
            _data[slot] = null;
            _view.ShowItem(slot, _data[slot]);
            JsonInventorySaver.Save(_data.Values.ToArray());
        }
    }

    public ISlot[] FindAllItemSlotsByType<T>() where T : AItem
    {
        List<ISlot> items = new List<ISlot>();
        foreach (ISlot slot in _data.Keys)
        {
            if (slot.IsLocked())
                continue;
            if (_data[slot] == null)
                continue;
            try
            {
                T item = (T)_data[slot];
                items.Add(slot);
            }
            catch { }
        }

        if(items.Count > 0)
            return items.ToArray();
        else
            return null;
    }

    public ISlot FindItemSlotByType<T>(bool notFullStack) where T : AItem
    {
        foreach (ISlot slot in _data.Keys)
        {
            try
            {
                T item = (T)_data[slot];
                if (!notFullStack)
                    return slot;
                else
                {
                    if (_data[slot].GetAmount() != _data[slot].MaxAmount)
                        return slot;
                }
            }
            catch { }
        }
        return null;
    }

    public ISlot FindItemSlotByItem(AItem item, bool notFullStack)
    {
        foreach (ISlot slot in _data.Keys)
        {
            if (slot.IsLocked())
                continue;

            if (_data[slot]?.GetType() == item?.GetType())
            {
                if(!notFullStack)
                    return slot;
                else
                {
                    if (_data[slot].GetAmount() != _data[slot].MaxAmount)
                        return slot;
                }
            }
        }
        return null;
    }

    public ISlot FindEmptySlot()
    {
        foreach (ISlot slot in _data.Keys)
        {
            if (slot.IsLocked())
                continue;

            if (_data[slot] == null)
                return slot;
        }
        return null;
    }

    public AItem GetRandomItem()
    {
        if (!IsThereAnyItemInInventory())
            throw new System.Exception("Inventory is empty");

        AItem randomItem = null;

        while (randomItem == null)
            randomItem = _data.ElementAt(Random.Range(0, _data.Count)).Value;

        return randomItem;
    }

    private bool IsThereAnyItemInInventory()
    {
        foreach (ISlot slot in _data.Keys)
        {
            if (_data[slot] != null)
                return true;
        }
        return false;
    }
}
