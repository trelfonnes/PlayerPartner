using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeOfDayManager : MonoBehaviour
{
    public static TimeOfDayManager Instance { get; private set; }
    [SerializeField] Light2D globalLight;
    [SerializeField] float targetIntensity;
    [SerializeField] float transitionDuration = 3f;
    float transitionTimer = 0f;
    private int currentHour = 2;
    [SerializeField] bool isIndoors; //access this and change to true when indoors. When back outdoors, switch to false first, then executetimevisuallogic
    public TimeOfDay CurrentTimeOfDay { get; private set; }
    public enum TimeOfDay
    {
        Morning,
        Evening,
        Day,
        Night
    }

    public int HoursPassed { get; private set; }
    int nightHour = 14;
    int eveningHour = 10;
    int morningHour = 2;
    int dayHour = 6;

    public event Action onMorning;
    public event Action onDay;
    public event Action onEvening;
    public event Action onNight;
    
    
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
        if (globalLight == null)
        {
            globalLight = GetComponentInChildren<Light2D>();
        }
    }


    private void Start()
    {
            ExecuteTimeVisualLogic();
        
    }
    private void Update()
    {
                
            ChangeLight();
        
    }
    public int GetCurrentHour()
    {
        return currentHour;
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
            currentHour = 0;
        }
        HoursPassed++;
        currentHour++;

       
            ExecuteTimeVisualLogic();
        
       
    }
    public void ChangeGlobalLightIntensity(float targetIntensity)
    {
        this.targetIntensity = targetIntensity;
    }
    public void ChangeToIndoorLight(bool isIndoors)
    {
        this.isIndoors = isIndoors;
        if (!this.isIndoors)
        {
            ExecuteTimeVisualLogic();
        }
    }
    private void ExecuteTimeVisualLogic()
    {
        if (!isIndoors)
        {
            if (HoursPassed >= nightHour || HoursPassed < morningHour) // Night condition
            {//turn dark
                targetIntensity = .7f;
                transitionTimer = 0f;
                // change color to white
                globalLight.color = new Color(1f, 1f, 1f, 0f);
                SetTimeOfDay(TimeOfDay.Night);
            }
            else if (HoursPassed >= morningHour && HoursPassed < dayHour) // morning condition
            {
                //change color to sunrise and return to day intensity
                targetIntensity = 1f;
                transitionTimer = 0f;
                globalLight.color = new Color(209f / 255f, 234f / 255f, 1, 0f);
                SetTimeOfDay(TimeOfDay.Morning);

            }

            else if (HoursPassed >= dayHour && HoursPassed < eveningHour) //daytime condition
            {

                // change color to white. should already be at correct intensity
                globalLight.color = new Color(1f, 1f, 1f, 0f);
                SetTimeOfDay(TimeOfDay.Day);

            }
            else if (HoursPassed >= eveningHour && HoursPassed < nightHour) //evening condition
            {
                // change color to sunset, should already be correct intensity
                globalLight.color = new Color(1f, 224f / 255f, 204f / 255f, 0f);
                SetTimeOfDay(TimeOfDay.Evening);


            }
        }
    }


    void SetTimeOfDay(TimeOfDay newTimeOfDay)
    {
        CurrentTimeOfDay = newTimeOfDay;

        switch (CurrentTimeOfDay)
        {
            case TimeOfDay.Morning:
                if (onMorning != null) onMorning();
                break;
            case TimeOfDay.Evening:
                if (onEvening != null) onEvening();
                break;
            case TimeOfDay.Day:
                if (onDay != null) onDay();
                
                break;
            case TimeOfDay.Night:
                if (onNight != null) onNight();
                break;
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
    public void DisableGlobalLight() // call from scene end
    {
        if (globalLight != null)
        {
            globalLight.enabled = false;
        }
    }
    public void EnableGlobalLight() //call from scene start
    {
        if (globalLight)
        {
            globalLight.enabled = true;
        }
    }
    private void OnDisable()
    {
        UnSubscribeToHourlyTickEvent();
    }
}
