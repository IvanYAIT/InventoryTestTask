using UnityEngine;

public abstract class AWeapon : AItem
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private int damage;

    public BulletType BulletType { get { return bulletType; } }

    public virtual void Shoot(ABullet bullet)
    {
        if (bullet.GetAmount() > 0 && bullet.BulletType == bulletType)
            Debug.Log("Shoot");
    }
}
