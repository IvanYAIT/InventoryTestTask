using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, ISlot
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image iconImg;

    public void ShowIcon(Sprite icon) =>
        iconImg.sprite = icon;

    public void ShowItemAmount(int value) =>
        amountText.text = value.ToString();
}
