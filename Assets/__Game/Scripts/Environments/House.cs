using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Collider2D sceneSwitchCollider;
    SceneLoaderUtility sceneLoader;

    void Start()
    {
        sceneLoader = new SceneLoaderUtility();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // if (collision.CompareTag("Player"))
     //   {
            //Play enter door sound
     //       sceneLoader.LoadScene(sceneName);
    //    }
    }

}
