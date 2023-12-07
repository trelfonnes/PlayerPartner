using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IProjCorrode : ISpecialAbility
{
    public void ExecuteSpecialAbility(Collider2D collider)
    {
        if (collider.TryGetComponent(out ICorrode corrode))
        {
            corrode.Corrode();
        }
    }
}
