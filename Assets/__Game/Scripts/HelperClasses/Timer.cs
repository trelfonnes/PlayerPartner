using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer  
{
    float duration;
    float elapsedTime;
    public Timer(float duration)
    {
        this.duration = duration;
        elapsedTime = 0f;
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
}
