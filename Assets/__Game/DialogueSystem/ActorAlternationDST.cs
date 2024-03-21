using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;


public class ActorAlternationDST : MonoBehaviour, IInteractable
{
    [SerializeField] bool isActor;
    DialogueSystemTrigger DST;
    bool alreadyTriggered;
    [SerializeField] bool isOnUseTrigger;
    [SerializeField] bool isOnTriggerEnter;

    // Start is called before the first frame update
    void Start()
    {
        DST = GetComponent<DialogueSystemTrigger>();
        if (alreadyTriggered)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        gameObject.GetComponent<DialogueSystemTrigger>().OnUse();

    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isActor)
            {
                DST.conversationActor = collision.transform;
            }
            else
            {
                DST.conversationConversant = collision.transform;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOnTriggerEnter)
        {
            alreadyTriggered = true;
            gameObject.SetActive(false);
        }
    }
}
