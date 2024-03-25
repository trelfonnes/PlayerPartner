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
        if (collision.CompareTag("Player"))
        {
                if (isFirstDevice)
                {
                    playerData.deviceOneCollected = true;
                AudioManager.Instance.PlayAudioClip("CollectArtifact");

                gameObject.SetActive(false);
            }
            if (isSecondDevice)
                {
                    playerData.deviceTwoCollected = true;
                AudioManager.Instance.PlayAudioClip("CollectArtifact");

                gameObject.SetActive(false);
                }
        }
    }


}



