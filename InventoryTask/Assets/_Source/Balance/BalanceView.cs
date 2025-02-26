using TMPro;
using UnityEngine;

public class BalanceView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI balanceText;

    void Update()
    {
        balanceText.text = $"Balance: {Balance.Instance.Amount}";
    }
}
