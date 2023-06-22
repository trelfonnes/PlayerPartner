using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseSOPlayerData : ScriptableObject
{
    public BoolEvent isSickChanged = new BoolEvent();
    public BoolEvent isInjuredChanged = new BoolEvent();

    [SerializeField] bool isSick;
    public bool IsSick
    {
        get { return isSick; }
        set
        {
            if (isSick != value)
            {
                isSick = value;
                isSickChanged.Invoke(isSick);
            }
        }
    }

    [SerializeField] bool isInjured;
    public bool IsInjured
    {
        get { return isInjured; }
        set
        {
            if(isInjured != value)
            {
                isInjured = value;
                isInjuredChanged.Invoke(isInjured);
            }
        }
    }



}
public class BoolEvent : UnityEvent<bool> { }
