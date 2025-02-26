using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

public class JsonInventorySaver
{
    private static string FILE_PATH = Path.Combine(Application.persistentDataPath, "inventoryData.json");

    public static void Save(AItem[] inventory)
    {
        string json = JsonConvert.SerializeObject(inventory, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        }) ;
        Debug.Log("Serialized JSON: " + json);

        File.WriteAllText(FILE_PATH, json);
        Debug.Log("Data saved to: " + FILE_PATH);
    }

    public static AItem[] Load()
    {
        if (File.Exists(FILE_PATH))
        {
            string loadedJson = File.ReadAllText(FILE_PATH);
            return JsonConvert.DeserializeObject<AItem[]>(loadedJson);
        }

        Debug.Log("Save file not found");
        return null;
    }
}
