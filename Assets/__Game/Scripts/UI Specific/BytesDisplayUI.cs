using UnityEngine;
using TMPro;
public class BytesDisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberAmount;

    // Start is called before the first frame update
    private void Awake()
    {
       // numberAmount = GetComponent<TextMeshProUGUI>();

    }
   
   
    public void ChangeByteDisplay(int amount)
    {
        string number = amount.ToString();
        numberAmount.text = number;
    }

    
}
