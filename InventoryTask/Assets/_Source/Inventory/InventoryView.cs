using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Slot[] slots;

    public Slot[] Slots { get { return slots; } }

    private List<SlotSerializableData> loadedData;

    private void Start()
    {
        loadedData = JsonInventorySaver.LoadSlots();
        if(loadedData != null)
        {
            for(int i = 0; i < loadedData.Count; i++)
            {
                slots[i].Unlock(!loadedData[i].locked);
                slots[i].SetCost(loadedData[i].cost);
            }
        }

        foreach (Slot slot in slots)
        {
            slot.LockBtn.onClick.AddListener(SaveSlots);
        }
    }

    private void SaveSlots() =>
        JsonInventorySaver.SaveSlots(slots);

    public void ShowItem(ISlot slot, AItem item)
    {
        if(item == null)
        {
            slot.ShowIcon(null);
            slot.ShowItemAmount(0);
        }
        else
        {
            slot.ShowIcon(Resources.Load<Sprite>(item.IconName));
            slot.ShowItemAmount(item.GetAmount());
        }
    }
}
