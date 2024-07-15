using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBridge : MonoBehaviour
{

    SpriteRenderer sr;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float frameDuration;
    PolygonCollider2D coll;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = null;// bridge is submersed
        coll = GetComponent<PolygonCollider2D>();
        coll.enabled = true;
    }

    IEnumerator RaiseBridge()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sr.sprite = sprites[i];
            yield return new WaitForSeconds(frameDuration);
        }

        // Ensure the last frame is shown
        sr.sprite = sprites[sprites.Length - 1];
        coll.enabled = false;

    }
    public void Execute()
    {
        StartCoroutine(RaiseBridge());
    }



}
