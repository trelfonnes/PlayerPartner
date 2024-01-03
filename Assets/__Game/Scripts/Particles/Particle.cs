using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> spriteList = new List<Sprite>(); // List of sprites to cycle through

    [SerializeField]
    private bool isPlaying = false; // Flag to determine if the particle is currently playing

    private int currentSpriteIndex = 0; // Index of the current sprite in the list
    private float timer = 0f;
   [SerializeField] private float frameDuration = 0.3f; // Duration each frame should be displayed
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (isPlaying && spriteList.Count > 0)
        {
            timer += Time.deltaTime;

            // Check if it's time to switch to the next sprite
            if (timer >= frameDuration)
            {
                NextSprite();
                timer = 0f;
            }
        }
    }

    // Method to start playing the particle with a given speed
    public void Play()
    {
        isPlaying = true;
    }

    // Method to stop playing the particle
    public void Stop()
    {
        isPlaying = false;
        // gameObject.SetActive(false);
        Destroy(gameObject);
    }

    // Method to set the position of the particle and start playing
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        currentSpriteIndex = 0;
        Play();
    }

    // Method to switch to the next sprite in the list
    private void NextSprite()
    {
        currentSpriteIndex++;

        if (currentSpriteIndex >= spriteList.Count)
        {
            currentSpriteIndex = 0;
            Stop(); // Stop playing when we reach the end of the list
        }

        sr.sprite = spriteList[currentSpriteIndex];
    }
}