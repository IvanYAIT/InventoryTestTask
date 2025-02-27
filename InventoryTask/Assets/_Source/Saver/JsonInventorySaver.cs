using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonInventorySaver
{
    private static string FILE_PATH_INVENTORY = Path.Combine(Application.persistentDataPath, "inventoryData.json");
    private static string FILE_PATH_BALANCE = Path.Combine(Application.persistentDataPath, "balanceData.json");
    private static string FILE_PATH_SLOTS = Path.Combine(Application.persistentDataPath, "slotsData.json");

    public static void SaveInventory(AItem[] inventory)
    {
        List< InventorySerializableData > data = new List< InventorySerializableData >();
        foreach ( AItem item in inventory)
        {
            if (item != null)
                data.Add(new InventorySerializableData(item.GetType().ToString(), item.GetAmount()));
            else
                data.Add(null);
        }
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(FILE_PATH_INVENTORY, json);
    }

    public static List<InventorySerializableData> LoadInventory()
    {
        if (File.Exists(FILE_PATH_INVENTORY))
        {
            string loadedJson = File.ReadAllText(FILE_PATH_INVENTORY);
            List<InventorySerializableData> data = JsonConvert.DeserializeObject<List<InventorySerializableData>>(loadedJson);
            return data;
        }

        Debug.Log("Save file not found");
        return null;
    }

    public static void SaveBalance()
    {
        int balance = Balance.Instance.Amount;
        string json = JsonConvert.SerializeObject(balance, Formatting.Indented);

        File.WriteAllText(FILE_PATH_BALANCE, json);
    }

    public static void LoadBalance()
    {
        if (File.Exists(FILE_PATH_BALANCE))
        {
            string loadedJson = File.ReadAllText(FILE_PATH_BALANCE);
            int data = JsonConvert.DeserializeObject<int>(loadedJson);
            Balance.Instance.Amount = data;
        }

        Balance.Instance.Amount = 10000;
        Debug.Log("Save file not found");
    }

    public static void SaveSlots(ISlot[] slots)
    {
        List<SlotSerializableData> data = new List<SlotSerializableData>();
        foreach (ISlot slot in slots)
        {
            data.Add(new SlotSerializableData(slot.IsLocked(), slot.GetCost()));
        }
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(FILE_PATH_SLOTS, json);
    }

    public static List<SlotSerializableData> LoadSlots()
    {
        if (File.Exists(FILE_PATH_SLOTS))
        {
            string loadedJson = File.ReadAllText(FILE_PATH_SLOTS);
            List<SlotSerializableData> data = JsonConvert.DeserializeObject<List<SlotSerializableData>>(loadedJson);
            return data;
        }

        Debug.Log("Save file not found");
        return null;
    }
}

[Serializable]
public class InventorySerializableData
{
    public string itemType;
    public int amount;

    public InventorySerializableData(string itemType, int amount)
    {
        this.itemType = itemType;
        this.amount = amount;
    }
}

[Serializable]
public class SlotSerializableData
{
    public bool locked;
    public int cost;

    public SlotSerializableData(bool locked, int cost)
    {
        this.locked = locked;
        this.cost = cost;
    }
}
