using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClockHandUI : MonoBehaviour
{
    Transform clockHandTransform;
    float totalRotation = -45f;
   [SerializeField] List<Image> DayCycles = new List<Image>();
    int currentImageIndex = 0;
    private void Awake()
    {
        clockHandTransform = transform;
        InitializeClock();
       
    }

    private void InitializeClock()
    {
        totalRotation = -45f; // start at morning on clock
        clockHandTransform.eulerAngles = new Vector3(0, 0, totalRotation);
        foreach (Image image in DayCycles) //set all to false
        {
            image.gameObject.SetActive(false);
        }
        DayCycles[0].gameObject.SetActive(true); //set morning to active
        currentImageIndex = 0; //set image index to morning.
    }

    void HandleHourlyTick(object sender, ClockManager.OnTickEventArgs e)
    {
        // move clockHandTransform to next point + -22.5 degrees
        totalRotation -= 22.5f; // every 1/4 of a quarter day

        clockHandTransform.eulerAngles = new Vector3(0, 0, totalRotation);
        if(totalRotation <= -360)
        {
            totalRotation = 0f;
        }           

    }
    void HandleQuarterlyTick(object sender, ClockManager.OnTickEventArgs e)
    {

        // Check if the DayCycles list is empty, or if the index is out of bounds
        if (DayCycles.Count == 0 || currentImageIndex < 0 || currentImageIndex >= DayCycles.Count)
        {
            Debug.LogError("DayCycles list is empty or index out of bounds.");
            return;
        }

        // Set all images in the list to inactive
        foreach (Image image in DayCycles)
        {
            image.gameObject.SetActive(false);
        }
        //Increment the index,
        currentImageIndex++;
        Debug.Log(currentImageIndex + " is image index");
        // reset to 0 if it goes beyond the list size before setting to active
     
        if (currentImageIndex >= DayCycles.Count)
        {
            currentImageIndex = 0;
        }
        // Set the current image to active
        DayCycles[currentImageIndex].gameObject.SetActive(true);

    }
    private void OnEnable()
    {
        SubscribeToHourlyTickEvent();
        SubscribeToQuarterlyTickEvent();
    }
    private void OnDisable()
    {
        UnSubscribeToHourlyTickEvent();
        UnSubscribeToQuarterlyTickEvent();
    }
    void SubscribeToHourlyTickEvent()
    {
        ClockManager.OnTick += HandleHourlyTick;

    }
    void SubscribeToQuarterlyTickEvent() //probably can be used not in stamina, but is here for testing.
    {
        ClockManager.OnTick_6 += HandleQuarterlyTick;

    }
    void UnSubscribeToHourlyTickEvent()
    {
        ClockManager.OnTick -= HandleHourlyTick;
    }
    void UnSubscribeToQuarterlyTickEvent()//same here, probably put in another class like nightDay cycle
    {
        ClockManager.OnTick_6 -= HandleQuarterlyTick;
    }
}
