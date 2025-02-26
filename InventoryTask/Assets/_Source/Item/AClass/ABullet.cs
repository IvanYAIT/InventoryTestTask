using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ABullet : AItem
{
    [SerializeField] private BulletType bulletType;

    public BulletType BulletType { get { return bulletType; } }

    public override void FromDictionary(Dictionary<string, object> dict)
    {
        base.FromDictionary(dict);
        bulletType = dict.ContainsKey("BulletType") ? (BulletType)dict["BulletType"] : BulletType.Pistol;
    }

    public override Dictionary<string, object> ToDictionary()
    {
        if (!dict.ContainsKey("BulletType"))
        {
            dict.Add("BulletType", bulletType);
        }
        return base.ToDictionary();
    }
}
