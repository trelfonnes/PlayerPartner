using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseSOPlayerData : ScriptableObject
{
    public event System.Action<bool> onInjuredChanged; //listened to by displays in status bar
    public event System.Action<bool> onSickChanged;
    public BoolEvent isSickChanged = new BoolEvent();
    public BoolEvent isInjuredChanged = new BoolEvent();
    
    [SerializeField] bool isSick;
    [SerializeField] bool isPlayerForHealthUI;
    public event System.Action<float> OnCurrentHealthValueChanged; //listened to by displays bool dictates player vs partner health
    public event System.Action<float> OnCurrentPlayerHealthValueChanged; //listened to by displays bool dictates player vs partner health
    public event System.Action<float> OnStaminaValueChanged;

    // Example health variable
    [SerializeField] private float currentHealth;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                // Raise the event to notify listeners that the float value has changed
                if (isPlayerForHealthUI)
                {
                    OnCurrentPlayerHealthValueChanged?.Invoke(currentHealth);
                }
                else
                {
                    OnCurrentHealthValueChanged?.Invoke(currentHealth);
                }
            }
        }
    }

    // Example method that modifies the float value
    public void SetPartnerHealthFromItem(float value, bool isPlayerHealth)
    {
        CurrentHealth = value;
       
    }  
    public void SetPlayerHealthFromItem(float value, bool isPlayerHealth)
    {
            CurrentHealth = value;
       
    }  


    // Example Stamina variable
    [SerializeField] private float stamina;

    public float Stamina
    {
        get { return stamina; }
        set
        {
            if (stamina != value)
            {
                stamina = value;

                // Raise the event to notify listeners that the float value has changed
                OnStaminaValueChanged?.Invoke(stamina);
            }
        }
    }

    // Example method that modifies the float value
    public void SetStamina(float value)
    {
        Stamina = value;
    }




    public bool IsSick
    {
        get { return isSick; }
        set
        {
            if (isSick != value)
            {
                isSick = value;
                onSickChanged?.Invoke(isSick);
            }
        }
    }

    [SerializeField] bool isInjured;
    public bool IsInjured
    {
        get { return isInjured; }
        set
        {
           
            if (isInjured != value)
           {
                isInjured = value;
                Debug.Log("IsInjured event should be invoking" + isInjured);
                onInjuredChanged?.Invoke(isInjured);
            }
        }
    }



}
public class BoolEvent : UnityEvent<bool> { }
