using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProjIgniteSA : ISpecialAbility
{
  
    public void ExecuteSpecialAbility(Collider2D collider)
    {
        if(collider.TryGetComponent(out ILightable lightable))
        {
            lightable.Light();
        }
    }
}
