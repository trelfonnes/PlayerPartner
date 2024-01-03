using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrodeDoor : MonoBehaviour, ICorrode
{
    [SerializeField] List<Sprite> doorSprites;
    SpriteRenderer sr;
    private int currentSpriteIndex = 0; // Index of the current sprite in the list
    bool isPlaying;
    float timer = 0f;
    [SerializeField] float frameDuration = .075f;
    BoxCollider2D boxCollider;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void Update()
    {
        if (isPlaying && doorSprites.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= frameDuration)
            {
                NextSprite();
                timer = 0f;
            }

        }

        //change sprites, set collider to innactive
    }
    public void Play()
    {
        isPlaying = true;
    }

    // Method to stop playing the particle
    public void Stop()
    {
        isPlaying = false;
    }
    private void NextSprite()
    {
        currentSpriteIndex++;

        if (currentSpriteIndex >= doorSprites.Count)
        {
            currentSpriteIndex = doorSprites.Count - 1;
            Stop(); // Stop playing when we reach the end of the list
        }

        sr.sprite = doorSprites[currentSpriteIndex];


    }
    public void Corrode()
    {

        Play();
        boxCollider.enabled = false;
    }
}
