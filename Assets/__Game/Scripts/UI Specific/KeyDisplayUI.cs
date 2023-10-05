using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KeyDisplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI keysHeld;

    public void ChangeKeyDisplayAmount(int keyAmount)
    {
        string keyNumber = keyAmount.ToString();

        keysHeld.text = keyNumber;
    }

}
