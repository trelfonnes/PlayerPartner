using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FirelightHandler : MonoBehaviour, ILightable, IInteractable, IExtinguishable
{
    [SerializeField] GameObject fire;
    [SerializeField] GameObject RestAndRestore;
    [SerializeField] bool isIndoorObject;
    [SerializeField] string audioName;
    Light2D pointLight;
    bool audioIsPlaying;
    bool hasBeenActivated;
    bool isLit;
    

    private void Start()
    {
       
        TimeOfDayManager.Instance.onDay += DimForDaylight;
        TimeOfDayManager.Instance.onEvening += BrightenForDusk;
        CheckTheTime();
    }
    void CheckTheTime()
    {
        int currentHour = TimeOfDayManager.Instance.GetCurrentHour();
        if( currentHour >=2 && currentHour < 6)
        {
            //morning brightness
        }
        else if(currentHour >=6 && currentHour< 10)
        {
            DimForDaylight();
        }
        else if(currentHour>=10 && currentHour < 14)
        {
            BrightenForDusk();
        }
        else
        {
            //night brightness
        }
    }
    public void Interact()
    {
        if (hasBeenActivated)
        {
            SaveLoadManager.Instance.SaveGlobalData();
            //TODO allow game to save and bring up UI
        }
    }
    public void Light()
    {
        fire.SetActive(true);
        CheckTheTime();
        RestAndRestore.SetActive(true);
        pointLight = GetComponentInChildren<Light2D>();
        hasBeenActivated = true;
        isLit = true;

    }

    public void Extinguish()
    {
        fire.SetActive(false);
        RestAndRestore.SetActive(false);

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger || collision.CompareTag("Partner") && !collision.isTrigger)
        {
            if (hasBeenActivated && !audioIsPlaying)
            {
                AudioManager.Instance.PlayLoopingAudioClip(audioName);
                audioIsPlaying = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger || collision.CompareTag("Partner") && !collision.isTrigger)
        {
            if (hasBeenActivated && audioIsPlaying)
            {
                AudioManager.Instance.TurnLoopingObjectOff(audioName);
                audioIsPlaying = false;
            }
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
