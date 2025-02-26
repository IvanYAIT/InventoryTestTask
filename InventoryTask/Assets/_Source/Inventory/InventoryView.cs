using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Slot[] slots;

    public Slot[] Slots { get { return slots; } }

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
