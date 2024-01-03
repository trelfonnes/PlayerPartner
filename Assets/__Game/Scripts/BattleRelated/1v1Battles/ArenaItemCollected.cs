using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArenaItemCollected : MonoBehaviour
{
    public static Action onEnemyDefeated;
    [SerializeField] string sceneToLoad = "OverWorld";
    [SerializeField] float timeToEndArenaScene = 2f;
    bool isItemCollected = false;
    SceneLoaderUtility sceneLoader = new SceneLoaderUtility();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner"))
        {
            //perform an animation, make a sound, whatever before loading next scene
            //can set the partner game object to false and simply play an animation game object at this collision transform.position.
            // would need to be dynamic for all potential partners though...
            //just a single still frame of the partner doing a peace sign. 
            //set to innactave, rotate in a game object of corresponding partnerType and set to active, is a simple sprite of that partner
            //giving a peace sign. 
            onEnemyDefeated?.Invoke();
            StartCoroutine(EndTheBattle());

        }
    }
    private IEnumerator EndTheBattle()
    {
        yield return new WaitForSeconds(timeToEndArenaScene);

        sceneLoader.LoadScene(sceneToLoad);

    }

}
