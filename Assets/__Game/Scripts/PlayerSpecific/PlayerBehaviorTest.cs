using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorTest : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rb;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector2.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        Debug.Log(direction);


        
    }
}
