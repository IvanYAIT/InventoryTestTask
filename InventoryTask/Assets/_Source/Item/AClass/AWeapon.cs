using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AWeapon : AItem
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private int damage;
}
