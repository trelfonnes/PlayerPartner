using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleItemReset : MonoBehaviour
{
    [SerializeField] ObstacleRaceManager orm;
    private Vector2 startingTransformPos;
    
    private void OnEnable()
    {
        orm.onResetObstacles += ResetObstacles;
    }
    private void OnDisable()
    {
        orm.onResetObstacles -= ResetObstacles;
    }
    void ResetObstacles()
    {
        transform.position = startingTransformPos;
       
    }
   

    private void Start()
    {
        startingTransformPos = transform.position;
    }

}
