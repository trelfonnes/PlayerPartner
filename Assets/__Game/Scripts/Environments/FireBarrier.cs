using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBarrier : MonoBehaviour, IExtinguishable
{



    [SerializeField] List<Sprite> fireSprites;
    [SerializeField] List<Sprite> smokeSprites;
    SpriteRenderer sr;
    private int currentSpriteIndex = 0; // Index of the current sprite in the list
    bool isPlaying;
    bool isPlayingSmoke;
    float timer = 0f;
    [SerializeField] float frameDuration = .075f;
    BoxCollider2D boxCollider;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        Play();
    }

    private void Update()
    {
        if (isPlaying && fireSprites.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= frameDuration)
            {
                NextFireSprite();
                timer = 0f;
            }

        }
        if (isPlayingSmoke && smokeSprites.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= frameDuration)
            {
                NextSmokeSprite();
                timer = 0f;
            }

        }

        //change sprites, set collider to innactive
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {if (collision.isTrigger && collision.CompareTag("Player") || collision.isTrigger && collision.CompareTag("Partner"))
        {
            if (collision.TryGetComponent(out IDamageable damage))
            {
                damage.Damage(2f, AttackType.Fire);
            }
            if (collision.TryGetComponent(out IKnockBackable knockback))
            {
                knockback.KnockBack(new Vector2(0, -1), 5f, 0, -1);
            }
        }
    }
    public void Play()
    {
        isPlaying = true;
    } 
    public void PlaySmoke()
    {
        isPlayingSmoke = true;
    }

    // Method to stop playing the particle
    public void Stop()
    {
        isPlaying = false;
        isPlayingSmoke = false;
    }
    private void NextFireSprite()
    {
        currentSpriteIndex++;

        if (currentSpriteIndex >= fireSprites.Count)
        {
            currentSpriteIndex = 0;
             // Stop playing when we reach the end of the list
        }

        sr.sprite = fireSprites[currentSpriteIndex];


    } 
    private void NextSmokeSprite()
    {
        currentSpriteIndex++;

        if (currentSpriteIndex >= smokeSprites.Count)
        {
            currentSpriteIndex = smokeSprites.Count - 1;
            Stop(); // Stop playing when we reach the end of the list
            gameObject.SetActive(false);
        }

        sr.sprite = smokeSprites[currentSpriteIndex];


    }


    public void Extinguish()
    {
        Stop();
        currentSpriteIndex = 0;
        frameDuration = .175f;
        PlaySmoke();

    }
}
