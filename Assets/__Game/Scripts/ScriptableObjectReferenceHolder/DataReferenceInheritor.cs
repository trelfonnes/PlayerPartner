using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReferenceInheritor : MonoBehaviour
{
   
    [SerializeField] protected PlayerSOData playerSOData;
    [SerializeField] protected PlayerSOData partner1SOData;
    [SerializeField] protected PlayerSOData partner2SOData;
    [SerializeField] protected PlayerSOData partner3SOData;
    SORefHolder partnerDatas;

    protected virtual void Awake()
    {
        GameObject SORefHolderGO = GameObject.Find("ScriptableObjectReferenceHolder");

        if (SORefHolderGO != null)
        {
            partnerDatas = SORefHolderGO.GetComponent<SORefHolder>();

            if (partnerDatas == null)
            {
                Debug.LogError("SORefHolder component not found on game object");
            }
            else
            {
                //TODO create some logic that dictates which partnerDatas to match up with
                playerSOData = partnerDatas.playerSOData;
                partner1SOData = partnerDatas.Dino1SOData;
                partner2SOData = partnerDatas.Dino2SOData;
                partner3SOData = partnerDatas.Dino3SOData;

            }
        }

    }
    
}
