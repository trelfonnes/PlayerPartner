using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour
{
    private static PartnerManager instance;
    public static PartnerManager Instance => instance;

    [SerializeField] GameObject dinoOne;
    [SerializeField] GameObject dinoTwo;
    [SerializeField] GameObject dinoThree;
    GameObject currentPartner;
    
    [SerializeField] Transform inactiveTransform;
    [SerializeField]Transform workingTransform;
    [SerializeField] EvolutionEvents evolutionEvents;

    bool isEvolving;
    bool isDevolving;

    private void OnEnable()
    {
        evolutionEvents.OnEvolveToSecondStage += OnStartEvolutionHandler;
        evolutionEvents.OnEvolveToThirdStage += OnStartEvolutionHandler;
        evolutionEvents.OnDevolve += OnStartEvolutionHandler;

    }
    private void OnDisable()
    {
        evolutionEvents.OnEvolveToSecondStage -= OnStartEvolutionHandler;
        evolutionEvents.OnEvolveToThirdStage -= OnStartEvolutionHandler;
        evolutionEvents.OnDevolve -= OnStartEvolutionHandler; 

    }
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        currentPartner = dinoOne;
        workingTransform.position = currentPartner.transform.position;
    }
 
    private void OnStartEvolutionHandler(EvolutionEvents.EvolutionEventData e)
    {

        SwitchStage(e.evolutionStage);
    }

    public void SwitchStage(int stage)
    {
        workingTransform.position = currentPartner.transform.position;
        currentPartner.transform.position = inactiveTransform.position;
        
            currentPartner.GetComponentInChildren<IEvolutionPower>().StopEvolutionTimer();
        
        if (stage == 1)
        {
            dinoOne.gameObject.SetActive(true);
            currentPartner = dinoOne;
            currentPartner.transform.position = workingTransform.position;
            dinoTwo.gameObject.SetActive(false);
            dinoThree.gameObject.SetActive(false);
        }
        else if (stage == 2)
        {
            dinoTwo.gameObject.SetActive(true);
            currentPartner = dinoTwo;
            currentPartner.transform.position = workingTransform.position;
            dinoOne.gameObject.SetActive(false);
            dinoThree.gameObject.SetActive(false);
            
                currentPartner.GetComponentInChildren<IEvolutionPower>().StartEvolutionTimer();
        }
        else if (stage == 3)
        {
            dinoThree.gameObject.SetActive(true);

            currentPartner = dinoThree;
            currentPartner.transform.position = workingTransform.position;
            dinoOne.gameObject.SetActive(false);
            dinoTwo.gameObject.SetActive(false);
            
                currentPartner.GetComponentInChildren<IEvolutionPower>().StartEvolutionTimer();

        }

    }
    

}
