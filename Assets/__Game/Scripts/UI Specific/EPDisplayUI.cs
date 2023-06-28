using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EPDisplayUI : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI EPAmount;
    PlayerData playerData;
    // Start is called before the first frame update
    void Awake()
    {
       // EPAmount = GetComponent<TextMeshProUGUI>();
        playerData = PlayerData.Instance;
    }
    
    public void UpdateEPDisplayUI(int amount)
    {
        string number = amount.ToString();
        EPAmount.text = number;
        Debug.Log(amount);
    }
}
