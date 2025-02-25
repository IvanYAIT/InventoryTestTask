using UnityEngine;

public class AWeapon : AItem
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private int damage;

    public virtual void Shoot(ABullet bullet)
    {
        if (bullet.GetAmount() > 0 && bullet.BulletType == bulletType)
            Debug.Log("Shoot");
    }
}
