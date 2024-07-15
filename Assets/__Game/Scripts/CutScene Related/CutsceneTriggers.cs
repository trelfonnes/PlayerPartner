using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneTriggers : MonoBehaviour, IInteractable
{
    //the trigger goes on the object being animated
    // public CutsceneManager cutsceneManager;
    [SerializeField] bool isEnterColliderTrigger = true;
    [SerializeField] bool isInteractableTrigger = true;
    [SerializeField]string cutsceneToPlay; //corresponds to registered cutscenes in the manager must be registered first
    public enum TriggerType { OnTriggerEnter, OnStart, OnEvent, OnCondition}
    public TriggerType triggerType;
    public UnityEvent onCutsceneTriggered;
    public Collider2D triggerCollider;
    public bool conditionMet;

    private void Start()
    {
        switch (triggerType)
        {
            case TriggerType.OnStart:
                TriggerCutscene();
                break;
            case TriggerType.OnTriggerEnter:
                if (triggerCollider == null)
                {
                    triggerCollider = GetComponent<Collider2D>();
                }
                break;
        }
    }

    public void CutsceneToPlay(string cutsceneToPlay)
    {
        this.cutsceneToPlay = cutsceneToPlay;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerType == TriggerType.OnTriggerEnter && other.CompareTag("Player"))
        {
            TriggerCutscene();
        }
    }

    public void TriggerCutscene()
    {
        if (CutsceneManager.Instance != null)
        {
            CutsceneManager.onCutsceneFinished += CleanUp;
            CutsceneManager.Instance.PlayCutscene(cutsceneToPlay);
            onCutsceneTriggered?.Invoke(); // Call additional events or actions
        }
    }

    public void CheckConditionAndTrigger()
    {
        if (triggerType == TriggerType.OnCondition && conditionMet)
        {
            TriggerCutscene();
        }
    }
    void CleanUp()
    {
        CutsceneManager.onCutsceneFinished -= CleanUp;

        //reset position of game object or whatever else
        gameObject.SetActive(false);
    }
}
