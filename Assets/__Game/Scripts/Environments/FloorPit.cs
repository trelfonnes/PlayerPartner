using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPit : MonoBehaviour
{
    [SerializeField] Sprite crackedSprite;
    [SerializeField] Sprite pitSprite;
    [SerializeField] Transform areaSpawnPoint;
    [SerializeField] Transform fallPoint;
    [SerializeField] float damageAmount = 2f;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = crackedSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Player") || !collision.isTrigger && collision.CompareTag("Partner"))
        {
            spriteRenderer.sprite = pitSprite;
           // collision.GetComponentInChildren<IHealthChange>().DecreaseHealth(damageAmount);
           StartCoroutine(RepositionCharacter(collision));
            
        }
    }

    IEnumerator RepositionCharacter(Collider2D collision)
    {
        yield return new WaitForSeconds(1.25f);
        collision.transform.position = areaSpawnPoint.position;

    }

}
