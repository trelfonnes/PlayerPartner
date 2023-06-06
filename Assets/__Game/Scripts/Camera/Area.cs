using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Area : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject partnerCamera;
    private CinemachineVirtualCamera playerVC;
    private CinemachineVirtualCamera partnerVC;


    private void Awake()
    {
        playerVC = playerCamera.GetComponent<CinemachineVirtualCamera>();        
        partnerVC = partnerCamera.GetComponent<CinemachineVirtualCamera>();        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && !collider.isTrigger)
        {
            var player = collider.GetComponent<Player>();
            player.PlayerCamera = playerVC;
            playerCamera.SetActive(true);
        }
        if(collider.CompareTag("Partner") && !collider.isTrigger)
        {
            var partner = collider.GetComponent<Partner>();
            partner.PartnerCamera = partnerVC;
            partnerCamera.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger)
        {
            playerCamera.SetActive(false);

        }
        if (collider.CompareTag("Partner") && !collider.isTrigger)
        {
            partnerCamera.SetActive(false);

        }
    }
}