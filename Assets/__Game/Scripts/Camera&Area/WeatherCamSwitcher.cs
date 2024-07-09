using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherCamSwitcher : MonoBehaviour
{
    public static WeatherCamSwitcher Instance;
    //make sure these start as innactive in the scene heirarchy
    public GameObject rainEffectParticles;
    public GameObject snowEffectParticles;
    public GameObject ashEffectParticles;
    public GameObject fogEffectParticles;
    public GameObject sandEffectParticles;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWeather(AreaType areaType)
    {
        //deactivate all potential weather effects
        TurnAllWeatherOff();
       
        switch (areaType)
        {
            case AreaType.Swamp:
                rainEffectParticles.SetActive(true);
                break;
            case AreaType.IceCliff:
                snowEffectParticles.SetActive(true);
                break;
            case AreaType.Volcano:
                ashEffectParticles.SetActive(true);
                break;
            case AreaType.Cemetary:
                fogEffectParticles.SetActive(true);
                break;
            case AreaType.Forest:
                fogEffectParticles.SetActive(true);
                break;
            case AreaType.Desert:
                sandEffectParticles.SetActive(true);
                break;
            default:
                TurnAllWeatherOff();
                break;

        }
    }
    void TurnAllWeatherOff()
    {
        if (rainEffectParticles)
        {
            rainEffectParticles.SetActive(false);
        }
        if (snowEffectParticles)
        {
            snowEffectParticles.SetActive(false);
        }
        if (ashEffectParticles)
        {
            ashEffectParticles.SetActive(false);
        }
        if (fogEffectParticles)
        {
            fogEffectParticles.SetActive(false);
        }
        if (sandEffectParticles)
        {
            sandEffectParticles.SetActive(false);
        }
    }

    
}
