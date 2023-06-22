using System;
using UnityEngine;
using System.Collections;
[Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] public StatEvents statEvents;

    public float ep;
    public float maxEp;
    public bool deviceOneCollected;
    public bool deviceTwoCollected;
    public bool StartEPTimer;
  

    private static PlayerData instance;

    public static PlayerData Instance
    {
        get { return instance; } }
      


     void Awake()
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
     void Update()
    {
        if (StartEPTimer)
        {
            CountDownEPTimer();
        }
    }

     void CountDownEPTimer()
    {
            ep = Mathf.Clamp(ep - Time.deltaTime, 0, maxEp);
            if (ep <= 0)
            {
                ep = 0;
            StartEPTimer = false;
            statEvents.CurrentEPZero();
                
           }
        
    }

    
}
