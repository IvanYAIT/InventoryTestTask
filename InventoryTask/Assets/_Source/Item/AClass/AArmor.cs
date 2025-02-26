using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

[System.Serializable]
public abstract class AArmor : AItem
{
    [SerializeField] private ArmorType armorType;
    [SerializeField] private int defense;

    public ArmorType ArmorType { get { return armorType; } }

    public override void FromDictionary(Dictionary<string, object> dict)
    {
        base.FromDictionary(dict);
        armorType = dict.ContainsKey("ArmorType") ? (ArmorType)dict["ArmorType"] : ArmorType.Body;
        defense = dict.ContainsKey("Defense") ? (int)dict["Defense"] : 0;
    }

    public override Dictionary<string, object> ToDictionary()
    {
        if (!dict.ContainsKey("ArmorType"))
            dict.Add("ArmorType", armorType);
        if (!dict.ContainsKey("Defense"))
            dict.Add("Defense", defense);

        return base.ToDictionary();
    }
}
