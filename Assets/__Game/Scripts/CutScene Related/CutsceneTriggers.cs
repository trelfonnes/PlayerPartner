using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTriggers : MonoBehaviour, IInteractable
{

    // public CutsceneManager cutsceneManager;
    public CutsceneData cutsceneData;
    [SerializeField] bool isEnterColliderTrigger = true;
    [SerializeField] bool isInteractableTrigger = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isEnterColliderTrigger)
        {
            CutsceneManager.Instance.PlayCutscene(cutsceneData);
        }
    }
    // implement other ways to trigger cut scenes and add bools to check which it is

    public void Interact()
    {
        if (isInteractableTrigger)
        {
            CutsceneManager.Instance.PlayCutscene(cutsceneData);
        }
    }
}
