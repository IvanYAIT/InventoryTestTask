using UnityEngine;
using UnityEngine.UI;

public class DebugMenuData : MonoBehaviour
{
    [SerializeField] private Button shootBtn;
    [SerializeField] private Button addBulletBtn;
    [SerializeField] private Button addEquipmentBtn;
    [SerializeField] private Button removeItemBtn;
    [SerializeField] private AItem[] items;

    public Button ShootBtn => shootBtn;
    public Button AddBulletBtn => addBulletBtn;
    public Button AddEquipmentBtn => addEquipmentBtn;
    public Button RemoveItemBtn => removeItemBtn;
    public AItem[] Items => items;
}
