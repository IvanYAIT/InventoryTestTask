using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DebugMenu
{
    private IInventory _inventory;
    private DebugMenuData _data;

    [Inject]
    public DebugMenu(IInventory inventory, DebugMenuData data)
    {
        _inventory = inventory;
        _data = data;
        _data.AddBulletBtn.onClick.AddListener(AddBullet);
        _data.AddEquipmentBtn.onClick.AddListener(AddEquipment);
        _data.ShootBtn.onClick.AddListener(Shoot);
        _data.RemoveItemBtn.onClick.AddListener(RemoveRandomItem);
    }

    //private void OnDestroy()
    //{
    //    _data.AddBulletBtn.onClick.RemoveListener(AddBullet);
    //    _data.AddEquipmentBtn.onClick.RemoveListener(AddEquipmnt);
    //    _data.ShootBtn.onClick.RemoveListener(Shoot);
    //    _data.RemoveItemBtn.onClick.RemoveListener(RemoveRandomItem);
    //}

    private void AddBullet()
    {
        foreach (AItem item in _data.Items)
        {
            try
            {
                ABullet bullet = (ABullet)item;
                _inventory.AddItem(item, item.MaxAmount);
            }
            catch { }
        }
    }

    private void AddEquipment()
    {
        List<AWeapon> weapons = new List<AWeapon>();
        List<AArmor> head = new List<AArmor>();
        List<AArmor> body = new List<AArmor>();

        foreach (AItem item in _data.Items)
        {
            try
            {
                AWeapon weapon = (AWeapon)item;
                weapons.Add(weapon);
            }
            catch { }

            try
            {
                AArmor armor = (AArmor)item;
                if (armor.ArmorType == ArmorType.Head)
                    head.Add(armor);
                else if (armor.ArmorType == ArmorType.Body)
                    body.Add(armor);
            }
            catch { }
        }

        AItem weaponRnd = weapons[Random.Range(0, weapons.Count)];
        _inventory.AddItem(weaponRnd, weaponRnd.MaxAmount);
        AItem headRnd = head[Random.Range(0, head.Count)];
        _inventory.AddItem(headRnd, headRnd.MaxAmount);
        AItem bodyRnd = body[Random.Range(0, body.Count)];
        _inventory.AddItem(bodyRnd, bodyRnd.MaxAmount);
    }

    private void Shoot()
    {
        ISlot[] bullets = _inventory.FindAllItemSlotsByType<ABullet>();
        if (bullets != null)
            _inventory.RemoveItemFromSlot(bullets[Random.Range(0, bullets.Length)], 1);
        else
            Debug.Log("no bullets in inventory");
    }

    private void RemoveRandomItem()
    {
        try
        {
            AItem randomItem = _inventory.GetRandomItem();
            _inventory.RemoveItem(randomItem, randomItem.GetAmount());
        } catch (System.Exception)
        {
            throw;
        }
    }
}
