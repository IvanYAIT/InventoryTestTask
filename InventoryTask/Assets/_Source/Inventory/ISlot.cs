using UnityEngine;

public interface ISlot
{
    public void ShowIcon(Sprite icon);
    public void ShowItemAmount(int value);
    public bool IsLocked();
    public int GetCost();
    public void SetCost(int value);
    public void Unlock(bool unlocked);
}
