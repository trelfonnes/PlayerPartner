using System;
using UnityEngine;
using System.Collections;
[Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] public StatEvents statEvents;
    [SerializeField] EPDisplayUI UIForEP;
    public float ep;
    public float maxEp;
    public bool deviceOneCollected;
    public bool deviceTwoCollected;
    public bool StartEPTimer;
    int roundedAmount;
    public bool partnerIsDefeated;

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
        roundedAmount = Mathf.RoundToInt(ep);
    }
    private void OnEnable()
    {
        UIForEP.UpdateEPDisplayUI(roundedAmount);

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
         roundedAmount = Mathf.RoundToInt(ep);
        UIForEP.UpdateEPDisplayUI(roundedAmount);
            if (ep <= 0)
            {
                ep = 0;
            StartEPTimer = false;
            statEvents.CurrentEPZero();
                
           }
        
    }
    public void LowerEPOnInjury()
    {
        ep = 1;
        roundedAmount = Mathf.RoundToInt(ep);
        UIForEP.UpdateEPDisplayUI(roundedAmount);
    }

    
}
