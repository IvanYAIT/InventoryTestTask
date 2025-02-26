using UnityEngine;

public abstract class ABullet : AItem
{
    [SerializeField] private BulletType bulletType;

    public BulletType BulletType { get { return bulletType; } }

}
