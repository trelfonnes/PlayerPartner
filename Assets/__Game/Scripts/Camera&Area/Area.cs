using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Area : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject partnerCamera;
    private CinemachineVirtualCamera playerVC;
    private CinemachineVirtualCamera partnerVC;
    [SerializeField] EvolutionEvents switchEvents;
    bool partnerCameraSwitched;
    bool partnerCameraActive;
    
    private void Awake()
    {

        playerVC = playerCamera.GetComponent<CinemachineVirtualCamera>();        
        partnerVC = partnerCamera.GetComponent<CinemachineVirtualCamera>();        
       CameraSwitcher.Register(playerVC);
        CameraSwitcher.Register(partnerVC);
        switchEvents.OnSwitchToPlayer += HandlePlayerControl;
        switchEvents.OnSwitchToPartner += HandlePartnerControl;

    }

    private void HandlePlayerControl()
    {
        partnerCameraActive = false;
    }

    private void HandlePartnerControl()
    {
        partnerCameraActive = true;

    }

   

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && !collider.isTrigger)
        {
            var player = collider.GetComponent<Player>();
            player.PlayerCamera = playerVC;
           // playerCamera.transform.position = player.transform.position;
            playerCamera.SetActive(true);

            partnerCameraSwitched = false;
        }
        if(collider.CompareTag("Partner") && !collider.isTrigger)// && partnerCameraActive)
        {
            Debug.Log("Partner has entered the cameras collider and should be set");
            var partner = collider.GetComponent<Partner>();
            partner.PartnerCamera = partnerVC;
            //partnerCamera.transform.position = partner.transform.position;

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
            partnerCameraSwitched = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(!partnerCameraSwitched && collider.CompareTag("Partner") && !collider.isTrigger && partnerCameraActive)
        {
            Debug.Log("ontrigger partner stay");
            var partner = collider.GetComponent<Partner>();
            partner.PartnerCamera = partnerVC;
            partnerCamera.SetActive(true);
            partnerCameraSwitched = true;
        }
        
    }

    private void OnDisable()
    {
        CameraSwitcher.UnRegister(playerVC);
        CameraSwitcher.UnRegister(partnerVC);
        switchEvents.OnSwitchToPlayer -= HandlePlayerControl;
        switchEvents.OnSwitchToPartner -= HandlePartnerControl;

    }
}
