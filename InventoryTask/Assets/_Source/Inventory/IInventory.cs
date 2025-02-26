public interface IInventory
{
    public bool AddItem(AItem item, int amount);
    public bool RemoveItem(AItem item, int amount);
    public bool RemoveItemFromSlot(ISlot slot, int amount);
    public ISlot FindItemSlotByType<T>(bool notFullStack) where T : AItem;
    public ISlot FindItemSlotByItem(AItem item, bool notFullStack);
    public ISlot FindEmptySlot();
    public ISlot[] FindAllItemSlotsByType<T>() where T : AItem;
    public void Clear();
    public AItem GetRandomItem();
}