using UnityEngine;

public interface ISlot
{
    public void ShowIcon(Sprite icon);
    public void ShowItemAmount(int value);
    public bool IsLocked();
}
