using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionDevice : MonoBehaviour
{
    PlayerData playerData;
   [SerializeField] bool isFirstDevice;
   [SerializeField] bool isSecondDevice;

    private void Start()
    {
        playerData = PlayerData.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
                if (isFirstDevice)
                {
                    playerData.deviceOneCollected = true;
                gameObject.SetActive(false);
            }
            if (isSecondDevice)
                {
                    playerData.deviceTwoCollected = true;
                gameObject.SetActive(false);
                }
        }
    }


}



