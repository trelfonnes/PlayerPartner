using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirelightHandler : MonoBehaviour, ILightable, IInteractable, IExtinguishable
{
    [SerializeField] GameObject fire;
    [SerializeField] bool isIndoorObject;
    Light2D pointLight;
    bool hasBeenActivated;
    bool isLit;
    

    private void Start()
    {
       
        TimeOfDayManager.Instance.onDay += DimForDaylight;
        TimeOfDayManager.Instance.onEvening += BrightenForDusk;
    }
    public void Interact()
    {
        if (hasBeenActivated)
        {
            SaveLoadManager.Instance.SaveWithEasySave();
            //TODO allow game to save and bring up UI
        }
    }
    public void Light()
    {
        fire.SetActive(true);
        pointLight = GetComponentInChildren<Light2D>();
        hasBeenActivated = true;
        isLit = true;
    }

    public void Extinguish()
    {
        fire.SetActive(false);
        hasBeenActivated = false;

    }
    void DimForDaylight()
    {
        if (hasBeenActivated && !isIndoorObject)
        {
            pointLight.intensity = 0f;
        }
    }
    void BrightenForDusk()
    {
        if (hasBeenActivated && !isIndoorObject)
        {
            pointLight.intensity = .5f;
        }
    }
    private void OnEnable()
    {
       

    }
    private void OnDisable()
    {
        TimeOfDayManager.Instance.onDay -= DimForDaylight;
        TimeOfDayManager.Instance.onEvening -= BrightenForDusk;

    }
}
