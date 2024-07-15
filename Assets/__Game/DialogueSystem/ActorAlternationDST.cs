using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;


public class ActorAlternationDST : MonoBehaviour, IInteractable
{
    //This is ticked to onTrigger dialogue on an object that only needs dialogue once.
    // Not a 'inspectable' object. Those will have isOnUse ticked
    //make sure the object it is attached to has a trigger collider if it is onTrigger or needs to set the actor/conversant.
    [SerializeField] bool isActor; //this is ticked for making the player the actor or unticked if I want the player to be the conversant
    bool alreadyTriggered;
    [SerializeField] bool isOnTriggerEnter; // tick for onTrigger dialogue user. Untick for dialogue user.
    [SerializeField] string DialogueName; //Fill this out to give an ONTRIGGEREnter only a uniqe identifier for saving its status
    DialogueSystemTrigger DST;

    void Start()
    {
        DST = GetComponent<DialogueSystemTrigger>();
        if (DialogueName != null && isOnTriggerEnter)
        {
            alreadyTriggered = SaveLoadManager.Instance.LoadBool(DialogueName);

            if (alreadyTriggered)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (!isOnTriggerEnter)
        {
            gameObject.GetComponent<DialogueSystemTrigger>().OnUse();
        }
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
        if (collision.CompareTag("Player") && isOnTriggerEnter && !alreadyTriggered)
        {
            alreadyTriggered = true;
            SaveLoadManager.Instance.SaveBool(DialogueName, alreadyTriggered);
            gameObject.SetActive(false);

        }
    }
}
