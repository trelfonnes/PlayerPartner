using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeOfDayManager : MonoBehaviour
{
    public static TimeOfDayManager Instance { get; private set; }
    Light2D globalLight;
    [SerializeField] float targetIntensity = 1f;
    [SerializeField] float transitionDuration = 3f;
    float transitionTimer = 0f;

    public bool isIndoors; //access this and change to true when indoors. When back outdoors, switch to false first, then executetimevisuallogic

    public int HoursPassed { get; private set; }
    int nightHour = 14;
    int eveningHour = 10;
    int morningHour = 2;
    int dayHour = 6;
    //FFE0CC color to simulate a sunset
    // D1EAFF sunrise

    private void Awake()
    {
        
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        HoursPassed = 2;

    }



    private void Start()
    {
        globalLight = GetComponent<Light2D>();
        //then checked and set. e.g. works with entering and exiting indoors and outdoors.
       // This script continues to be subscribed to ontick indoors. However, use flag isIndoors
       // before RunTimeLogic is executed. That way HoursPassed will increment but lighting wont change
        ExecuteTimeVisualLogic();

    }
    private void Update()
    {
        ChangeLight();
    }

    private void ChangeLight()
    {
        if (transitionTimer < transitionDuration)
        {
            // Increment the timer based on real-time
            transitionTimer += Time.deltaTime;

            // Calculate the progress (a value between 0 and 1)
            float progress = Mathf.Clamp01(transitionTimer / transitionDuration);

            // Interpolate the intensity smoothly from the current value to the target value
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, targetIntensity, progress);
        }
    }

    void HandleTimeTick(object sender, ClockManager.OnTickEventArgs e)
    {
        if (HoursPassed >= 16)
        {
            HoursPassed = 0;
        }
        HoursPassed++;

        if (!isIndoors)
        {
            ExecuteTimeVisualLogic();
        }
       
    }

    private void ExecuteTimeVisualLogic()
    {
        
        if (HoursPassed >= nightHour || HoursPassed < morningHour) // Night condition
        {//turn dark
            targetIntensity = .7f;
            transitionTimer = 0f;
            // change color to white
            globalLight.color = new Color(1f, 1f, 1f, 0f);
        }
        else if (HoursPassed >= morningHour && HoursPassed < dayHour) // morning condition
        {
            //change color to sunrise and return to day intensity
            targetIntensity = 1f;
            transitionTimer = 0f;
            globalLight.color = new Color(209f / 255f, 234f / 255f, 1, 0f);

        }

        else if (HoursPassed >= dayHour && HoursPassed < eveningHour) //daytime condition
        {

            // change color to white. should already be at correct intensity
            globalLight.color = new Color(1f, 1f, 1f, 0f);
        }
        else if (HoursPassed >= eveningHour && HoursPassed < nightHour) //evening condition
        {
            // change color to sunset, should already be correct intensity
            globalLight.color = new Color(1f, 224f / 255f, 204f / 255f, 0f);
        }
    }

    void SubscribeToTimeTickEvent()
    {
        ClockManager.OnTick += HandleTimeTick;

    }

    void UnSubscribeToHourlyTickEvent()
    {
        ClockManager.OnTick -= HandleTimeTick;
    }
    private void OnEnable()
    {
        SubscribeToTimeTickEvent();
    }

    private void OnDisable()
    {
        UnSubscribeToHourlyTickEvent();
    }
}
