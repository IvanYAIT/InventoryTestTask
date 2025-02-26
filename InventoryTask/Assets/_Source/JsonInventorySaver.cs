using System;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class JsonInventorySaver
{
    private static string FILE_PATH = Path.Combine(Application.persistentDataPath, "inventoryData.json");

    public static void Save(AItem[] inventory)
    {
        List< SerializableData > data = new List< SerializableData >();
        foreach ( AItem item in inventory)
        {
            if (item != null)
                data.Add(new SerializableData(item.GetType().ToString(), item.GetAmount()));
            else
                data.Add(null);
        }
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        Debug.Log("Serialized JSON: " + json);

        File.WriteAllText(FILE_PATH, json);
        Debug.Log("Data saved to: " + FILE_PATH);
    }

    public static List<SerializableData> Load()
    {
        if (File.Exists(FILE_PATH))
        {
            string loadedJson = File.ReadAllText(FILE_PATH);
            List<SerializableData> data = JsonConvert.DeserializeObject<List<SerializableData>>(loadedJson);
            return data;
        }

        Debug.Log("Save file not found");
        return null;
    }
}

[Serializable]
public class SerializableData
{
    public string itemType;
    public int amount;

    public SerializableData(string itemType, int amount)
    {
        this.itemType = itemType;
        this.amount = amount;
    }
}
