using UnityEngine;

public abstract class AItem : ScriptableObject
{
    [SerializeField] private int maxAmount;
    [SerializeField] private float weight;
    [SerializeField] private Sprite icon;

    public int MaxAmount {  get { return maxAmount; } }

    private int amount;

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

    public int Subtract(int value)
    {
        if (amount - value > 0)
        {
            amount -= value;
            return 0;
        }
        else
        {
            int remains = value - amount;
            amount = 0;
            return remains;
        }
    }

    public void SetAmount(int value) => amount = value;
    public int GetAmount() => amount;
}
