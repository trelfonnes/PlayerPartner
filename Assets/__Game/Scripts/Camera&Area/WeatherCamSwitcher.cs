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
            default:
                Debug.LogWarning("no weather effect in : " + areaType);
                break;

        }
    }

    
}
