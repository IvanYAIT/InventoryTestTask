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
        lockBtn.onClick.AddListener(delegate { Unlock(true); });
    }

    private void Update()
    {
        if (Balance.Instance.Amount >= costToUnlock)
            lockBtn.interactable = true;
        else 
            lockBtn.interactable = false;
    }

    public bool IsLocked() => locked;

    public Button LockBtn => lockBtn;

    public int GetCost() => costToUnlock;
    public void SetCost(int value) => costToUnlock = value;

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

    public void Unlock(bool unlocked)
    {
        if (unlocked)
        {
            Balance.Instance.Amount -= costToUnlock;
            JsonInventorySaver.SaveBalance();
            locked = false;
            lockBtn.gameObject.SetActive(locked);
        }
        else
        {
            locked = true;
            lockBtn.gameObject.SetActive(locked);
            costText.text = $"Unlock for {costToUnlock}";
        }
    }
}
