using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AWeapon : AItem
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private int damage;

    public BulletType BulletType { get { return bulletType; } }

    public override void FromDictionary(Dictionary<string, object> dict)
    {
        base.FromDictionary(dict);
        bulletType = dict.ContainsKey("NeededBulletType") ? (BulletType)dict["NeededBulletType"] : BulletType.Pistol;
        damage = dict.ContainsKey("Damage") ? (int)dict["Damage"] : 0;
    }

    public override Dictionary<string, object> ToDictionary()
    {
        if (!dict.ContainsKey("NeededBulletType"))
            dict.Add("NeededBulletType", bulletType);
        if (!dict.ContainsKey("Damage"))
            dict.Add("Damage", damage);

        return base.ToDictionary();
    }

    public virtual void Shoot(ABullet bullet)
    {
        if (bullet.GetAmount() > 0 && bullet.BulletType == bulletType)
            Debug.Log("Shoot");
    }
}
