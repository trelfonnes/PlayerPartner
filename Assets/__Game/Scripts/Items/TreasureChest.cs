using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour, IInteractable
{
    [SerializeField] Sprite closed;
    [SerializeField] Sprite open;
    SpriteRenderer sr;
    [SerializeField] GameObject containedItemPrefab;
    [SerializeField] Transform itemSpawnPoint;
    private GameObject containedItemInstance;
    BoxCollider2D boxCollider2D;
    float yOffset = .7f;
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public void Interact()
    {
        OpenChest();
    }

    private void OpenChest()
    {
        sr.sprite = open;
        Vector2 colliderPosition = boxCollider2D.offset;
        colliderPosition.y += yOffset;
        boxCollider2D.offset = colliderPosition;
        AudioManager.Instance.PlayAudioClip("OpenChest");

        if (containedItemInstance == null)
        {
          
            containedItemInstance = Instantiate(containedItemPrefab, itemSpawnPoint.position, itemSpawnPoint.rotation);
        }
    }
}