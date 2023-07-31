using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnItem : MonoBehaviour
{
    [SerializeField] bool on;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            ItemSpawnSystem.Instance.SpawnItem(transform);
            gameObject.SetActive(false);
        }
        
    }
}
