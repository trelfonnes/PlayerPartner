using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Collider2D sceneSwitchCollider;
    SceneLoaderUtility sceneLoader;
    [SerializeField] Transform ReloadPlayerPosition;

    void Start()
    {
        sceneLoader = new SceneLoaderUtility();
        if(ReloadPlayerPosition == null)
        {
            Debug.LogError("No Reload Position Specified for " + sceneName);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            //Play enter door sound
            Player player = collision.GetComponent<Player>();
            player.sceneLoadPosition = ReloadPlayerPosition;
          sceneLoader.LoadScene(sceneName);
        }
    }

}
