public interface IInventory
{
    public void AddItem(AItem item, int amount);
    public void RemoveItem(AItem item, int amount);
    public T FindItemByType<T>();
    public T[] FindAllItemsByType<T>();
    public AItem GetItemFromSlot(ISlot slot);
    public void Clear();
}