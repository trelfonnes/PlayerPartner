using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableObstruction : MonoBehaviour, IBombable
{
    BoxCollider2D obstructionColl;
    [SerializeField] Sprite obstructedImage;
    [SerializeField] Sprite openImage;
    SpriteRenderer sr;

    public void Explode()
    {
        RemoveObstruction();
    }

    private void Start()
    {
        obstructionColl = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = obstructedImage;
        obstructionColl.enabled = true;
    }
    void RemoveObstruction()
    {
        sr.sprite = openImage;
        obstructionColl.enabled = false;
    }
}
