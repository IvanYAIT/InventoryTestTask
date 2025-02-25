using System.Collections.Generic;

public interface IInventory
{
    public bool AddItem(AItem item, int amount);
    public bool RemoveItem(AItem item, int amount);
    public ISlot FindItemSlotByType<T>(bool notFullStack) where T : AItem;
    public ISlot FindItemSlotByType<T>(T item, bool notFullStack) where T : AItem;
    public ISlot FindEmptySlot();
    public List<ISlot> FindAllItemSlotsByType<T>() where T : AItem;
    public void Clear();
}