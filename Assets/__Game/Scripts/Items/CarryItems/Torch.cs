using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    List<Sprite> sprites = new List<Sprite>();
    SpriteRenderer sr;
    float timeToSpriteSwitch;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ILightable lightable))
        {
            lightable.Light(); //TODO create the "Fire" object that will implement this interface and do the lighting behavior.
        }
    }


IEnumerator SwitchSprite()
    {

        while (true)
        {
            // Loop through the list of sprites
            for (int i = 0; i < sprites.Count; i++)
            {
                sr.sprite = sprites[i];

                // Wait for the specified duration
                yield return new WaitForSeconds(timeToSpriteSwitch);
            }
        }
    }

}
