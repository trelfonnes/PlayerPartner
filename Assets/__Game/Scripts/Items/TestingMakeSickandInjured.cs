using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMakeSickandInjured : MonoBehaviour
{
    bool makeSickandInjured = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner"))
        {
            collision.GetComponentInChildren<ISick>().SickONandOFF(makeSickandInjured);
            collision.GetComponentInChildren<IInjured>().InjuredONandOFF(makeSickandInjured);
        }
    }
}
