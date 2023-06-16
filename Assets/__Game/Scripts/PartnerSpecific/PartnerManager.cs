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

    bool isEvolving;

    private void Awake()
    {
        currentPartner = dinoOne;
        workingTransform.position = currentPartner.transform.position;
    }
    private void Start()
    {
        EvolveBehavior.OnEndEvolution += delegate (object sender, EvolveBehavior.OnEvolutionEventArgs e)
        {
            SwitchStage(e.evolutionStage);
            isEvolving = e.isEvolving;
        };
    }
    public void SwitchStage(int stage)
    {
        workingTransform.position = currentPartner.transform.position;
        currentPartner.transform.position = inactiveTransform.position;

        if (stage == 1)
        {
            dinoOne.gameObject.SetActive(true);
            currentPartner = dinoOne;
            currentPartner.GetComponentInChildren<IEvolutionPower>().StartEvolutionTimer();
            currentPartner.transform.position = workingTransform.position;
            dinoTwo.gameObject.SetActive(false);
            dinoThree.gameObject.SetActive(false);
        }
        else if(stage == 2)
        {
            dinoTwo.gameObject.SetActive(true);
            currentPartner = dinoTwo;
            currentPartner.GetComponentInChildren<IEvolutionPower>().StartEvolutionTimer();
            currentPartner.transform.position = workingTransform.position;
            dinoOne.gameObject.SetActive(false);
            dinoThree.gameObject.SetActive(false);
        }
        else if(stage == 3)
        {
            dinoThree.gameObject.SetActive(true);

            currentPartner = dinoThree;
            currentPartner.GetComponentInChildren<IEvolutionPower>().StartEvolutionTimer();
            currentPartner.transform.position = workingTransform.position;
            dinoOne.gameObject.SetActive(false);
            dinoTwo.gameObject.SetActive(false);

        }
    }
    
}
