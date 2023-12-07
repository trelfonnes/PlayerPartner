using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProjPowerOn : ISpecialAbility
{
    public void ExecuteSpecialAbility(Collider2D collider)
    {
        if (collider.TryGetComponent(out IPowerOn powerOn))
        {
            powerOn.PowerOn();
        }
    }
}
