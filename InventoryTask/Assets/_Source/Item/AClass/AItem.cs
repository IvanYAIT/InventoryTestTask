using UnityEngine;

public abstract class AItem : ScriptableObject
{
    [SerializeField] private int maxAmount;
    [SerializeField] private int amount;
    [SerializeField] private float weight;
    [SerializeField] private Sprite icon;

    public int Add(int value)
    {
        if (value + amount <= maxAmount)
        {
            amount += value;
            return 0;
        }
        else
        {
            int remains = (amount + value) - maxAmount;
            amount = maxAmount;
            return remains;
        }
    }

    public void SetAmount(int value) => amount = value;
    public int GetAmount() => amount;
}
