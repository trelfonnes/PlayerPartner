using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
   [SerializeField] private EvolutionEvents evolutionEvents;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize evolutionEvents reference
           // evolutionEvents = Resources.Load<EvolutionEvents>("EvolutionEvents"); // Adjust the path based on your project structure

            // Subscribe to the sceneUnloaded event
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }
        else
        {
            Destroy(gameObject);
        }
        OnSceneSwitch();

    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneUnloaded event
        SceneManager.sceneUnloaded -= OnSceneUnloaded;

        // Unsubscribe from events here
    }

    public void SubscribeToPlayerSwitch(Action method)
    {
        evolutionEvents.OnSwitchToPartner += method;
    }

    public void UnsubscribeFromPlayerSwitch(Action method)
    {
    }

    public void PlayerSwitchEvent()
    {
        // Broadcast the event
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // Clean up subscriptions when a scene is unloaded
        OnSceneSwitch();
    }

    private void OnSceneSwitch()
    {
        // Clean up subscriptions when a scene switch occurs
        evolutionEvents.UnSubscribeAllEvents();
    }
}