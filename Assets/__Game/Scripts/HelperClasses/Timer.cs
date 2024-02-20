using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer  
{
    float duration;
    float elapsedTime;
    public Timer(float duration)
    {
        this.duration = duration;
        elapsedTime = 0f;
        isActive = false;
    }

    public void Update(float deltaTime)
    {
        elapsedTime += deltaTime;
    }

    public bool IsFinished()
    {
        return elapsedTime >= duration;
    }

    public void Reset()
    {
        elapsedTime = 0f;
    }

    // part 2 of way to do timer. Uses Timer(float duration) as well

    float startTime;
    float targetTime;
    public event Action OnTimerDone;
    bool isActive;
   
    public void StartTimer()
    {
        startTime = Time.time;
        targetTime = startTime + duration;
        isActive = true;
    }
    

    public void StopTimer()
    {
        isActive = false;
    }
    public void Tick()
    {
        if (!isActive) return;

        if(Time.time >= targetTime)
        {
            OnTimerDone?.Invoke();
            StopTimer();

        }
    }
}
