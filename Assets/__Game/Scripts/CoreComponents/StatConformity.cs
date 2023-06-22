using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatConformity: MonoBehaviour
{
    public PlayerSOData partnerData1;
    public PlayerSOData partnerData2;
    public PlayerSOData partnerData3;



    private void OnEnable()
    {
        partnerData1.isSickChanged.AddListener(OnDataIsSickChanged);
        // is injured cannot occur at 2 and 3. Is injured means once devolved, can't evolve. or can't evolve at all.
        // if 1 is injured, already can't evolve, therefore 2 and 3 dont need to know.
        // if 2 or 3 is injured, only 1 needs to know. Once devolved, only 1 must recover from it
        partnerData2.isSickChanged.AddListener(OnDataIsSickChanged);
        partnerData3.isSickChanged.AddListener(OnDataIsSickChanged);    
        partnerData2.isInjuredChanged.AddListener(OnDataIsInjuredChanged);
        partnerData3.isInjuredChanged.AddListener(OnDataIsInjuredChanged);
    }
    private void OnDisable()
    {
        partnerData1.isSickChanged.RemoveListener(OnDataIsSickChanged);
        partnerData2.isSickChanged.RemoveListener(OnDataIsSickChanged);
        partnerData3.isSickChanged.RemoveListener(OnDataIsSickChanged);
        partnerData2.isInjuredChanged.RemoveListener(OnDataIsInjuredChanged);
        partnerData3.isInjuredChanged.RemoveListener(OnDataIsInjuredChanged);

    }
    void OnDataIsSickChanged(bool isSick)
    {
        partnerData1.IsSick = isSick;
        partnerData2.IsSick = isSick;
        partnerData3.IsSick = isSick;
    }
    
    void OnDataIsInjuredChanged(bool isInjured)
    {
        partnerData1.IsInjured = isInjured;
    }

}
