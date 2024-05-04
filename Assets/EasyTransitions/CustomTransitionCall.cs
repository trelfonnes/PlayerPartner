using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class CustomTransitionCall : MonoBehaviour
{//This class sits on the Transition Manager game object in each scene.
 //This way sceneLoader Utility can send the message while this sets references
    public static CustomTransitionCall Instance { get; private set; } // Singleton instance

    public TransitionSettings circleFade;
    public TransitionSettings fade;
    public TransitionSettings currentTransition;
    public float startDelay;

    private void Awake()
    {
        if (Instance == null) // If no instance exists, set this as the instance
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this one to ensure singleton
        }
    }

    private void Start()
    {
        currentTransition = circleFade; // Default transition
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Load the scene from curomtransitioncall");
        TransitionManager.Instance().Transition(sceneName, currentTransition, startDelay); // Use singleton instance
    }

    public void ChangeTransitionToFade()
    {
        currentTransition = fade;
    }

    public void ChangeTransitionToCircleFade()
    {
        currentTransition = circleFade;
    }
}