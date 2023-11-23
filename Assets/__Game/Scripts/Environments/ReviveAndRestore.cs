using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveAndRestore : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IReviveAndRestore RAndR))
        {
                RAndR.ReviveAndRestore();
        }
    }
}
