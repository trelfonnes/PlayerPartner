using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateForSwitches : MonoBehaviour
{
    BoxCollider2D barrierCol;
    SpriteRenderer sr;
    [SerializeField] ObstacleRaceManager orm;
    [SerializeField] Sprite barrierUp;
    [SerializeField] Sprite barrierDown;
    [SerializeField] List<SpriteRenderer> srList = new List<SpriteRenderer>();
    bool isGateOn = true;
    // Start is called before the first frame update
    void Start()
    {
        barrierCol = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void GateOnOff()
    {
        isGateOn = !isGateOn; // Toggle the state

        if (isGateOn)
        {
            barrierCol.enabled = true;
            sr.sprite = barrierUp;
            sr.sortingLayerName = "MiddleGround";
            foreach (SpriteRenderer spriteRenderer in srList)
            {
                spriteRenderer.sprite = barrierUp;
                spriteRenderer.sortingLayerName = "MiddleGround";
            }
        }
        else
        {
            barrierCol.enabled = false;
            sr.sprite = barrierDown;
            sr.sortingLayerName = "Default";

            foreach (SpriteRenderer spriteRenderer in srList)
            {
                spriteRenderer.sprite = barrierDown;
                spriteRenderer.sortingLayerName = "Default";

            }
        }
    }
    void ResetGate()
    {
        barrierCol.enabled = true;
        sr.sprite = barrierUp;
        sr.sortingLayerName = "MiddleGround";
        foreach (SpriteRenderer spriteRenderer in srList)
        {
            spriteRenderer.sprite = barrierUp;
            spriteRenderer.sortingLayerName = "MiddleGround";
        }
    }
    void ResetForObstacle()
    {
        if (orm)
        {
            ResetGate();
        }
    }
    private void OnEnable()
    {
        if (orm)
        {
            orm.onResetObstacles += ResetForObstacle;
        }
    }

    private void OnDisable()
    {
        if (orm)
        {
            orm.onResetObstacles -= ResetForObstacle;
        }
    }
}
