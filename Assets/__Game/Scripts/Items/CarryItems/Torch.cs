using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class Torch : MonoBehaviour
{
    Light2D pointLight;
    [SerializeField] bool isIndoorObject;

    private void Start()
    {
        pointLight = GetComponentInChildren<Light2D>();
        TimeOfDayManager.Instance.onDay += DimForDaylight;
        TimeOfDayManager.Instance.onEvening += BrightenForDusk;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ILightable lightable))
        {
            lightable.Light(); //TODO create the "Fire" object that will implement this interface and do the lighting behavior.
        }
    }
    void DimForDaylight()
    {
        if (!isIndoorObject)
        {
            pointLight.intensity = 0f;
        }
    }
    void BrightenForDusk()
    {
        if (!isIndoorObject)
        {
            pointLight.intensity = .25f;
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

