using System;
using UnityEngine;
using System.Collections;
[Serializable]
public class PlayerData : MonoBehaviour
{
    [SerializeField] public StatEvents statEvents;
    [SerializeField] EPDisplayUI UIForEP;
    public float ep;
    public int currentExperience = 0;
    public int experienceToNextLevel = 10;
    public int currentBondLevel = 1;
    public float maxEp;
    public bool deviceOneCollected;
    public bool deviceTwoCollected;
    public bool StartEPTimer;
    int roundedAmount;
    public bool partnerIsDefeated;
    public Vector2 lastPositionsInScene;

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

    public void GainExperience(int exp)
    {
        currentExperience += exp;
        Debug.Log(currentExperience);
        if(currentExperience >= experienceToNextLevel)
        {
            Level1Up();
        }
    }

    void Level1Up()
    {
        currentBondLevel++;
        currentExperience = 0;
        experienceToNextLevel = CalculateNextLevelExperience();

    }
    int CalculateNextLevelExperience()
    {
        float baseExperience = 10;
        float growthFactor = 1.2f;
        return Mathf.RoundToInt(baseExperience * Mathf.Pow(growthFactor, currentBondLevel));


    }
}
