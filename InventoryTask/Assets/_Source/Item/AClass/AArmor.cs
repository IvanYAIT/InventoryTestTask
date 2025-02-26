using UnityEngine;

public abstract class AArmor : AItem
{
    [SerializeField] private ArmorType armorType;
    [SerializeField] private int defense;

    public ArmorType ArmorType { get { return armorType; } }

}
