using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProjExtinguish : ISpecialAbility
{
    public void ExecuteSpecialAbility(Collider2D collider)
    {
        if (collider.TryGetComponent(out IExtinguishable extinguishable))
        {
            extinguishable.Extinguish();
        }
    }
}
