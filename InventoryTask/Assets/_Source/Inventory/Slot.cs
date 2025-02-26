using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour, ISlot
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image iconImg;
    [SerializeField] private Button lockBtn;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private bool locked;
    [SerializeField] private int costToUnlock;

    private void Awake()
    {
        lockBtn.gameObject.SetActive(locked);
        costText.text = $"Unlock for {costToUnlock}";
        lockBtn.onClick.AddListener(Unlock);
    }

    private void Update()
    {
        if (Balance.Instance.Amount >= costToUnlock)
            lockBtn.interactable = true;
        else 
            lockBtn.interactable = false;
    }

    public bool IsLocked() => locked;


    public void ShowIcon(Sprite icon)=>
        iconImg.sprite = icon;

    public void ShowItemAmount(int value)
    {
        if (value <= 1)
            amountText.gameObject.SetActive(false);
        else
        {
            amountText.gameObject.SetActive(true);
            amountText.text = value.ToString();
        }
    }

    public void Unlock()
    {
        Balance.Instance.Amount -= costToUnlock;
        locked = false;
        lockBtn.gameObject.SetActive(locked);
    }
}
