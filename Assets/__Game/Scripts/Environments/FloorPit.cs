using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPit : MonoBehaviour
{
    [SerializeField] Sprite crackedSprite;
    [SerializeField] Sprite pitSprite;
    [SerializeField] Transform areaSpawnPoint;
    [SerializeField] float damageAmount = 2f;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = crackedSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PitfallCol"))
        {
            spriteRenderer.sprite = pitSprite;
           // collision.GetComponentInChildren<IHealthChange>().DecreaseHealth(damageAmount);
           StartCoroutine(RepositionCharacter(collision));
            //TODO: call FLASH interface to show damage. Play sound FX for hurt
            // Or, is this done congruent when health is decreased?
            //TODO: go the extra mile by player a "fall animation" and reposition once that has played.
        }
    }

    IEnumerator RepositionCharacter(Collider2D collision)
    {
        yield return new WaitForSeconds(1.25f);
        collision.transform.parent.parent.parent.position = areaSpawnPoint.position;

    }

}
