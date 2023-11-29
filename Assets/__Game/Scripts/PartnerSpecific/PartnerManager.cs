using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PartnerPrefabMapping
{
    public PartnerType partnerType;
    public GameObject partnerPrefab;
}
public class PartnerManager : MonoBehaviour
{

   [SerializeField] private List<PartnerPrefabMapping> partnerMappings = new List<PartnerPrefabMapping>();

    // Dictionary to store partner prefabs with their corresponding PartnerType
    private Dictionary<PartnerType, GameObject> partnerPrefabs = new Dictionary<PartnerType, GameObject>();

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
    GameObject currentPartner;
    
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
        DontDestroyOnLoad(gameObject);
        InitializePartnerPrefabs();

    }
    private void InitializePartnerPrefabs()
    {
        // Load and store partner prefabs in the dictionary
        foreach (var mapping in partnerMappings)
        {
            if (mapping.partnerPrefab != null && !partnerPrefabs.ContainsKey(mapping.partnerType))
            {
                partnerPrefabs.Add(mapping.partnerType, mapping.partnerPrefab);
            }
            else
            {
                Debug.LogError("Invalid prefab mapping for " + mapping.partnerType.ToString());
            }
        }
       // SetPartners();
    }
   public void SetPartners(PartnerType type)
    {
        if(type == PartnerType.DinoOne)
        {
            partnerOne = GetPartnerPrefab(type);
            partnerTwo = GetPartnerPrefab(PartnerType.DinoTwo);
            partnerThree = GetPartnerPrefab(PartnerType.DinoThree);

        }
        else if(type == PartnerType.BearOne)
        {
            partnerOne = GetPartnerPrefab(type);
            partnerTwo = GetPartnerPrefab(PartnerType.BearTwo);
            partnerThree = GetPartnerPrefab(PartnerType.BearThree);
        }
        else if(type == PartnerType.RabbitOne)
        {
            partnerOne = GetPartnerPrefab(type);
            partnerTwo = GetPartnerPrefab(PartnerType.RabbitTwo);
            partnerThree = GetPartnerPrefab(PartnerType.RabbitThree);
        }
        else if(type == PartnerType.AxelOne)
        {
            partnerOne = GetPartnerPrefab(type);
            partnerTwo = GetPartnerPrefab(PartnerType.AxelTwo);
            partnerThree = GetPartnerPrefab(PartnerType.AxelThree);
        }
        currentPartner = partnerOne;
        workingTransform.position = currentPartner.transform.position;
        InstantiatePartner(currentPartner);
    }
    public GameObject GetPartnerPrefab(PartnerType partnerType)
    {
        // Retrieve the partner prefab based on the provided PartnerType
        if (partnerPrefabs.ContainsKey(partnerType))
        {
            return partnerPrefabs[partnerType];
        }
        else
        {
            Debug.LogError("Prefab not found for " + partnerType.ToString());
            return null;
        }
    }
    void InstantiatePartner(GameObject partner)
    {
        Partner partnerClass = partner.GetComponent<Partner>();
        Instantiate(partner);
        partner.transform.position = startingSpawnPoint;
        GameManager.Instance.SetPartner(partnerClass);
        partner.SetActive(true);
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
    

}
