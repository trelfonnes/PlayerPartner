using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombableWall : MonoBehaviour, IBombable
{
    bool isOpened;
    BoxCollider2D triggerColl;
   [SerializeField] Sprite cracked;
   [SerializeField] Sprite open;
    SpriteRenderer sr;
    [SerializeField] string sceneToLoad;
    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();
    [SerializeField] Transform ReloadPlayerPosition;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        triggerColl = GetComponent<BoxCollider2D>();
        sr.sprite = cracked;
        triggerColl.enabled = false;
    }

    public void Explode()
    {
        OpenEntrance();
    }

    void OpenEntrance()
    {
        isOpened = true;
        sr.sprite = open;
        triggerColl.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.sceneLoadPosition = ReloadPlayerPosition;
            sceneLoader.LoadScene(sceneToLoad);
        }
        
    }

}
