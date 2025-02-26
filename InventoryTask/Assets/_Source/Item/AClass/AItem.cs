using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AItem : ScriptableObject
{
    [SerializeField] private int maxAmount;
    [SerializeField] private float weight;
    [SerializeField][JsonIgnore] private Sprite icon;

    public int MaxAmount {  get { return maxAmount; } }
    public string IconName
    {
        get
        {
            iconName = icon.name;
            return iconName;
        }
        set
        {
            icon.name = value;
        }
    }

    private int amount;
    private string iconName;
    protected Dictionary<string, object> dict = new Dictionary<string, object>();

    public virtual Dictionary<string, object> ToDictionary()
    {
        if (!dict.ContainsKey("MaxAmount"))
            dict.Add("MaxAmount", maxAmount);
        if (!dict.ContainsKey("Amount"))
            dict.Add("Amount", amount);
        if (!dict.ContainsKey("IconName"))
            dict.Add("IconName", IconName);
        if (!dict.ContainsKey("Weight"))
            dict.Add("Weight", weight);

        return dict;
    }

    public virtual void FromDictionary(Dictionary<string, object> dict)
    {
        maxAmount = dict.ContainsKey("MaxAmount") ? (int)dict["MaxAmount"] : 0;
        amount = dict.ContainsKey("Amount") ? (int)dict["Amount"] : 0;
        weight = dict.ContainsKey("Weight") ? (float)dict["Weight"] : 0f;
        IconName = dict.ContainsKey("IconName") ? dict["IconName"].ToString() : null;
    }

    public int Add(int value)
    {
        if (value + amount <= maxAmount)
        {
            amount += value;
            return -1;
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
            return -1;
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
