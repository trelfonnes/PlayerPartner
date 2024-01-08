using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PartnerManager : MonoBehaviour
{

   [SerializeField] private PartnerPrefabMappings prefabMappings;

    // Dictionary to store partner prefabs with their corresponding PartnerType

    private static PartnerManager instance;
    public static PartnerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PartnerManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("PartnerManager");
                    instance = managerObject.AddComponent<PartnerManager>();
                }
            }
            return instance;
        }
    }



    [SerializeField] GameObject partnerOne;
    [SerializeField] GameObject partnerTwo;
    [SerializeField] GameObject partnerThree;
   public GameObject currentPartner { get; private set; }
    
    [SerializeField] Transform inactiveTransform;
    [SerializeField]Transform workingTransform;
    [SerializeField] EvolutionEvents evolutionEvents;

    [SerializeField] Vector2 startingSpawnPoint; //TODO: For testing


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
        prefabMappings = GetComponentInChildren<PartnerPrefabMappings>();

        if (prefabMappings == null)
        {
            Debug.LogError("Prefab mapping reference in PartnerManager is null");
        }

    }
   
   public void SetPartners(PartnerType type)
    {
        if(type == PartnerType.DinoOne)
        {
            partnerOne = prefabMappings.GetPartnerPrefab(type);
            partnerTwo = prefabMappings.GetPartnerPrefab(PartnerType.DinoTwo);
            partnerThree = prefabMappings.GetPartnerPrefab(PartnerType.DinoThree);

        }
        else if(type == PartnerType.BearOne)
        {
            partnerOne = prefabMappings.GetPartnerPrefab(type);
            partnerTwo = prefabMappings.GetPartnerPrefab(PartnerType.BearTwo);
            partnerThree = prefabMappings.GetPartnerPrefab(PartnerType.BearThree);
        }
        else if(type == PartnerType.RabbitOne)
        {
            partnerOne = prefabMappings.GetPartnerPrefab(type);
            partnerTwo = prefabMappings.GetPartnerPrefab(PartnerType.RabbitTwo);
            partnerThree = prefabMappings.GetPartnerPrefab(PartnerType.RabbitThree);
        }
        else if(type == PartnerType.AxelOne)
        {
            partnerOne = prefabMappings.GetPartnerPrefab(type);
            partnerTwo = prefabMappings.GetPartnerPrefab(PartnerType.AxelTwo);
            partnerThree = prefabMappings.GetPartnerPrefab(PartnerType.AxelThree);
        }
        currentPartner = partnerOne;
        workingTransform.position = currentPartner.transform.position;
        SavePartnerType(type);
        InstantiatePartner(currentPartner, type);
    }
 
    void InstantiatePartner(GameObject partner, PartnerType type)
    {
        partner.SetActive(true);
        partner.transform.position = startingSpawnPoint;

        GameManager.Instance.SetPartnerInSaveManager(type);
    }
    void SavePartnerType(PartnerType type)
    {
        GameManager.Instance.SetPartnerInSaveManager(type);
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
            partnerOne.gameObject.SetActive(true);
            currentPartner = partnerOne;
            currentPartner.transform.position = workingTransform.position;
            partnerTwo.gameObject.SetActive(false);
            partnerThree.gameObject.SetActive(false);
        }
        else if (stage == 2)
        {
            partnerTwo.gameObject.SetActive(true);
            currentPartner = partnerTwo;
            currentPartner.transform.position = workingTransform.position;
            partnerOne.gameObject.SetActive(false);
            partnerThree.gameObject.SetActive(false);
            
                currentPartner.GetComponentInChildren<IEvolutionPower>().StartEvolutionTimer();
        }
        else if (stage == 3)
        {
            partnerThree.gameObject.SetActive(true);

            currentPartner = partnerThree;
            currentPartner.transform.position = workingTransform.position;
            partnerOne.gameObject.SetActive(false);
            partnerTwo.gameObject.SetActive(false);
            
                currentPartner.GetComponentInChildren<IEvolutionPower>().StartEvolutionTimer();

        }
    }
    public void SetLastPartnerActive(PartnerType savedPartner)
    {
        if (currentPartner)
        {
            currentPartner.SetActive(false);
        }
        currentPartner = null;
       currentPartner = ReturnPartnerType(savedPartner);

        Partner partner = currentPartner.GetComponent<Partner>();

            if (partner.stageOne )
            {
                SwitchStage(1);
            }
            else if (partner.stageTwo)
            {
                SwitchStage(2);

            }
            else if (partner.stageThree)
            {
                SwitchStage(3);

            }
        }
    
    public void MoveCurrentPartner(Vector2 location)
    {
        currentPartner.transform.position = location;
    }

    public GameObject ReturnPartnerType(PartnerType partnerType) // goes through here to get to mappings
    {
       GameObject partner = prefabMappings.GetPartnerPrefab(partnerType);

        return partner;




    }


}
