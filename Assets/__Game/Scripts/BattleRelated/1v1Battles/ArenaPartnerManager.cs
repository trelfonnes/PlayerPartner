using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPartnerManager : MonoBehaviour
{
    [SerializeField] PartnerPrefabMappings prefabMappings;

    private Dictionary<PartnerType, GameObject> partnerPrefabs = new Dictionary<PartnerType, GameObject>();

    private void Start()
    {
        prefabMappings = GetComponentInChildren<PartnerPrefabMappings>();
    }
    public GameObject ReturnPartnerType(PartnerType partnerType) // goes through here to get to mappings
    {
        GameObject partner = prefabMappings.GetPartnerPrefab(partnerType);

        return partner;




    }

}
