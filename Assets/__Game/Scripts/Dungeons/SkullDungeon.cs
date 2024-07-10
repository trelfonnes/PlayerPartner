using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkullDungeon : EventTriggerAbstractClass
{
    [SerializeField] Sprite[] animationSprites; // Array of sprites for the animation
    public float animationSpeed = 0.2f; // Time between frames
    [SerializeField] BoxCollider2D entranceTriggerCol;
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private bool isAnimating = false; 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        entranceTriggerCol.enabled = false;
        if (animationSprites.Length == 0)
        {
            Debug.LogError("No animation sprites assigned.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
       // if (other.CompareTag("Player")) // TODO: Change the condition for the animation to play to more than just a player triggering.
      //  {
         //   if (!isAnimating)
         //   {
        //        StartCoroutine(PlayAnimation());
       //     }
       // }
    }
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        StartCoroutine(PlayAnimation());

    }

    void OpenEntrance()
    {
        entranceTriggerCol.enabled = true;

    }
    IEnumerator PlayAnimation()
    {
        isAnimating = true;
        while(currentFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[currentFrame];
            currentFrame++;
            yield return new WaitForSeconds(animationSpeed);
        }
        spriteRenderer.sprite = animationSprites[animationSprites.Length - 1];
        isAnimating = false;
        OpenEntrance();
    }

    
}
