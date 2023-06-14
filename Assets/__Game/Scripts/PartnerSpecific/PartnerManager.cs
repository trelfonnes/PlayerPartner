using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerManager : MonoBehaviour
{
    [SerializeField] GameObject dinoOne;
    [SerializeField] GameObject dinoTwo;
    [SerializeField] GameObject dinoThree;
    GameObject currentPartner;
    
    [SerializeField] Transform inactiveTransform;
    [SerializeField]Transform workingTransform;

    private void Awake()
    {
        currentPartner = dinoOne;
        workingTransform.position = currentPartner.transform.position;
    }
    private void Start()
    {
        EvolveBehavior.OnEvolution += delegate (object sender, EvolveBehavior.OnEvolutionEventArgs e)
        {
            SwitchStage(e.evolutionStage);
        };
    }
    public void SwitchStage(int stage)
    {
        workingTransform.position = currentPartner.transform.position;
        currentPartner.transform.position = inactiveTransform.position;

        if (stage == 1)
        {
            currentPartner = dinoOne;
            currentPartner.transform.position = workingTransform.position;

        }
        else if(stage == 2)
        {
            dinoTwo.gameObject.SetActive(true);
            currentPartner = dinoTwo;
            currentPartner.transform.position = workingTransform.position;
        }
        else if(stage == 3)
        {
            currentPartner = dinoThree;
            currentPartner.transform.position = workingTransform.position;

        }
    }
    
}
