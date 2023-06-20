using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "newEvolutionEvents", menuName = "EvolveEvents Data/EvolveEvents Data")]

public class EvolutionEvents : ScriptableObject
{
    //for Partner mover
    public event Action<EvolutionEventData> OnEvolveToSecondStage;
    public event Action<EvolutionEventData> OnEvolveToThirdStage;
    public event Action<EvolutionEventData> OnDevolve;
    //for Player to stop movement
    public event Action OnStopForEvolution;
    public event Action OnReturnFromEvolution;

    public int Stage1 = 1;
    public int Stage2 = 2;
    public int Stage3 = 3;
    public int devolveStage = 1;
    public bool isEvolving = false;
    public bool isDevolving = false;
    public void EvolveToSecondStage()
    {
        if (OnEvolveToSecondStage != null)
        {
            EvolutionEventData e = GetPooledEvent();
            e.evolutionStage = Stage2;
            OnEvolveToSecondStage?.Invoke(e);
            ReturnPooledEvent(e);
        }
    }
    public void EvolveToFinalStage()
    {

        EvolutionEventData e = GetPooledEvent();
        e.evolutionStage = Stage3;
        OnEvolveToThirdStage?.Invoke(e);
        ReturnPooledEvent(e);

    }
    public void StopForEvolution()
    {
        OnStopForEvolution?.Invoke();
    }
    public void ReturnFromEvolution()
    {
        OnReturnFromEvolution?.Invoke();
    }
    public void Devolve()
    {
        if (OnDevolve != null)
        {
            EvolutionEventData e = GetPooledEvent();
            e.isDevolving = isDevolving;
            e.evolutionStage = devolveStage;
            OnDevolve?.Invoke(e);
            ReturnPooledEvent(e);
           
        }
    }
    private static readonly Queue<EvolutionEventData> eventPool = new Queue<EvolutionEventData>();



    public class EvolutionEventData
    {
        public int evolutionStage;
        public int devolveStage;
        public bool isEvolving;
        public bool isDevolving;

        public void Reset()
        {
            evolutionStage = 0;
            isEvolving = false;
            isDevolving = false;
        }
    }

    private static EvolutionEventData GetPooledEvent()
    {
        if (eventPool.Count > 0)
        {
            return eventPool.Dequeue();
        }
        else
        {
            return new EvolutionEventData();
        }
    }

    public static void ReturnPooledEvent(EvolutionEventData e)
    {
        e.Reset();
        eventPool.Enqueue(e);
    }
}