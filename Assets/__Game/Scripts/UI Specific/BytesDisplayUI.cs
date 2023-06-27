using UnityEngine;
using TMPro;
public class BytesDisplayUI : MonoBehaviour
{
     TextMeshProUGUI numberAmount;

    // Start is called before the first frame update
    void Awake()
    {
        numberAmount = GetComponent<TextMeshProUGUI>();
    }
    public void ChangeByteDisplay(int amount)
    {
        string number = amount.ToString();
        numberAmount.text = number;
    }

    
}
