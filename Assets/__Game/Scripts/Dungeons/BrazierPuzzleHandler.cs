using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BrazierPuzzleHandler : MonoBehaviour, ILightable, IExtinguishable
{
    [SerializeField] GameObject fire;
   [SerializeField] bool isIndoorObject;
    Light2D pointLight;
    bool hasBeenActivated;
    Timer timer;
    [SerializeField] float timeToBurnOut;
    private void Start()
    {
        timer = new Timer(timeToBurnOut);
        TimeOfDayManager.Instance.onDay += DimForDaylight;
        TimeOfDayManager.Instance.onDay += BrightenForDusk;
        CheckTheTime();

    }
    private void Update()
    {
        if (hasBeenActivated)
        {
            timer.Update(Time.deltaTime);
           
        }
        if (timer.IsFinished())
        {

            Extinguish();
        }

    }
    void CheckTheTime()
    {
        int currentHour = TimeOfDayManager.Instance.GetCurrentHour();
        if (currentHour >= 2 && currentHour < 6)
        {
            //morning brightness
        }
        else if (currentHour >= 6 && currentHour < 10)
        {
            DimForDaylight();
        }
        else if (currentHour >= 10 && currentHour < 14)
        {
            BrightenForDusk();
        }
        else
        {
            //night brightness
        }
    }
    public void Light()
    {
        fire.SetActive(true);
        CheckTheTime();
        pointLight = GetComponentInChildren<Light2D>();
        hasBeenActivated = true;
        timer.Reset();
        
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
    private void OnDisable()
    {
        TimeOfDayManager.Instance.onDay -= DimForDaylight;
        TimeOfDayManager.Instance.onEvening -= BrightenForDusk;

    }
}
