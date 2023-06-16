using System;
using UnityEngine;
using System.Collections;
[Serializable]
public class PlayerData : MonoBehaviour
{
    public float ep;
    public float maxEp;
    public bool deviceOneCollected;
    public bool deviceTwoCollected;

    private static PlayerData instance;

    public static PlayerData Instance
    {
        get { return instance; } }
      


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
   
}
