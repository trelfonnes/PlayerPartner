using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PixelCrushers.DialogueSystem;

//This class is attached to the game object with the battle ended conversation.
public class BossBattleDialogueListener : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "OverWorld";
    [SerializeField] float timeToEndArenaScene = 2f;

    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();

    void Start()
    {
        BossRewardItem.onRewardCollected += StartConversation;
    }

    void StartConversation()
    {
        DialogueManager.Instance.conversationEnded += ConversationFinished;
        gameObject.GetComponent<DialogueSystemTrigger>().OnUse();
        Debug.Log("START THE CONVERSATION");

    }
    void ConversationFinished(Transform primaryActorName)
    {
        DialogueManager.Instance.conversationEnded -= ConversationFinished;
        StartCoroutine(EndTheBattle());
        
    }
    private IEnumerator EndTheBattle()
    {
        yield return new WaitForSeconds(timeToEndArenaScene);

        sceneLoader.LoadScene(sceneToLoad);

    }

    private void OnDisable()
    {
        BossRewardItem.onRewardCollected -= StartConversation;
    }
}
