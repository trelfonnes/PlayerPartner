using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnItem : MonoBehaviour
{
    [SerializeField] bool on;
    [SerializeField] bool isRegular;
    [SerializeField] bool isRare;
    [SerializeField] bool isExtraRare;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            if (isRegular)
            {
                GameManager.Instance.SwitchToRegularStrategy();
                ItemSpawnSystem.Instance.SpawnItem(transform);
                gameObject.SetActive(false);
            }
            if (isRare)
            {
                GameManager.Instance.SwitchToRareStrategy();
                ItemSpawnSystem.Instance.SpawnItem(transform);
                gameObject.SetActive(false);


            }
            if (isExtraRare)
            {
                GameManager.Instance.SwitchToExtraRareStrategy();
                ItemSpawnSystem.Instance.SpawnItem(transform);
                gameObject.SetActive(false);

            }

        }

        
    }
}
