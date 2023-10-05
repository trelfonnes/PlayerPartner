using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSortingOrder : MonoBehaviour
{ 
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        // Assuming the grass has a higher Sorting Order
        int grassSortingOrder = 1; // Adjust this value as needed

        // Get the pivot point's position in local space
        float pivotY = spriteRenderer.sprite.bounds.extents.y * transform.localScale.y;

        // Calculate the sorting order based on the pivot point's Y position
        int playerSortingOrder = Mathf.RoundToInt(transform.position.y - pivotY);

        // Apply the sorting order
        spriteRenderer.sortingOrder = playerSortingOrder + grassSortingOrder;
    }

}
