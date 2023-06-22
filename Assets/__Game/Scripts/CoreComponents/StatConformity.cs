using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatConformity: DataReferenceInheritor
{
    

    protected override void Awake()
    {
        base.Awake();
        partner1SOData.isSickChanged.AddListener(OnDataIsSickChanged);
        // is injured cannot occur at 2 and 3. Is injured means once devolved, can't evolve. or can't evolve at all.
        // if 1 is injured, already can't evolve, therefore 2 and 3 dont need to know.
        // if 2 or 3 is injured, only 1 needs to know. Once devolved, only 1 must recover from it
        partner2SOData.isSickChanged.AddListener(OnDataIsSickChanged);
        partner3SOData.isSickChanged.AddListener(OnDataIsSickChanged);    
        partner2SOData.isInjuredChanged.AddListener(OnDataIsInjuredChanged);
        partner3SOData.isInjuredChanged.AddListener(OnDataIsInjuredChanged);
    }
    private void OnDisable()
    {
        partner1SOData.isSickChanged.RemoveListener(OnDataIsSickChanged);
        partner2SOData.isSickChanged.RemoveListener(OnDataIsSickChanged);
        partner3SOData.isSickChanged.RemoveListener(OnDataIsSickChanged);
        partner2SOData.isInjuredChanged.RemoveListener(OnDataIsInjuredChanged);
        partner3SOData.isInjuredChanged.RemoveListener(OnDataIsInjuredChanged);

    }
    void OnDataIsSickChanged(bool isSick)
    {
        partner1SOData.IsSick = isSick;
        partner2SOData.IsSick = isSick;
        partner3SOData.IsSick = isSick;
    }
    
    void OnDataIsInjuredChanged(bool isInjured)
    {
        partner1SOData.IsInjured = isInjured;
    }

}
