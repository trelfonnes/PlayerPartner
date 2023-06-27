using UnityEngine;
using TMPro;

public class SPDisplayUI : MonoBehaviour
{
    TextMeshProUGUI numberAmount;

    void Awake()
    {
        numberAmount = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeSPDisplay(int amount)
    {
        string number = amount.ToString();
        numberAmount.text = number;
    }
}
