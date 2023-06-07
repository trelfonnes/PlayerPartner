using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryableItem : MonoBehaviour, ICarry
{
    private GameObject item;
    private Transform itemTransform;

    private void Start()
    {
        itemTransform = transform;
    }
    public void Carry(Transform CarryPoint)
    {
        Debug.Log("Carrying");
        itemTransform.position = CarryPoint.position;
        itemTransform.parent = CarryPoint;
    }

  

}
