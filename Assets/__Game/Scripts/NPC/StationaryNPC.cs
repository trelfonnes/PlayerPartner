using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryNPC : MonoBehaviour
{

    [SerializeField] Sprite sprite1; // Reference to the first sprite
    [SerializeField] Sprite sprite2; // Reference to the second sprite
    [SerializeField] float timeToSwitch = 2.0f; // Time interval between sprite switches

    private SpriteRenderer spriteRenderer;
    private bool isSprite1Active = true;
    private float timer = 0.0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprite1 == null || sprite2 == null)
        {
            Debug.LogError("Please assign both sprites in the Inspector.");
        }
        else
        {
            spriteRenderer.sprite = sprite1;
        }
    }

    private void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if it's time to switch sprites
        if (timer >= timeToSwitch)
        {
            // Reset the timer
            timer = 0.0f;

            // Switch to the other sprite
            if (isSprite1Active)
            {
                spriteRenderer.sprite = sprite2;
            }
            else
            {
                spriteRenderer.sprite = sprite1;
            }

            isSprite1Active = !isSprite1Active;
        }
    }
}
