using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvolutionPower 
{
   public void DecreaseEP(int amount) { }
    public void IncreaseEP(int amount) { }

    public void IncreaseMaxEP(int amount) { }
    public void StartEvolutionTimer()
    {

    }
    public void StopEvolutionTimer()
    {

    }
}
