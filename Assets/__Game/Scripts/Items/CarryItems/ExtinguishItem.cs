using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IExtinguishable extinguishable))
        {
            extinguishable.Extinguish();
        }
    }
}
