using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartnerPrefabMapping
{
    public PartnerType partnerType;
    public GameObject partnerPrefab;
}
public class PartnerPrefabMappings: MonoBehaviour
{
    [SerializeField] private PartnerPrefabMapping[] partnerMappings;

    private Dictionary<PartnerType, GameObject> partnerPrefabs = new Dictionary<PartnerType, GameObject>();

    private void Awake()
    {
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
    public PartnerType GetPartnerType(GameObject partnerObj)
    {
        foreach (var pair in partnerPrefabs)
        {
            if (pair.Value == partnerObj)
            {
                return pair.Key;
            }
        }

        Debug.LogError("PartnerType not found for the provided GameObject");
        return PartnerType.DinoOne;
    }
}
