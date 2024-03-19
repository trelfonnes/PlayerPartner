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
    public PartnerType partnerFirstStageType;
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
                AssignData();
                                            
                

            }
        }

    }
    void AssignData()
    {
        if (ES3.KeyExists("chosenPartner"))
        {
            partnerFirstStageType = ES3.Load<PartnerType>("chosenPartner");
        }

        playerSOData = partnerDatas.playerSOData;
        if (partnerFirstStageType == PartnerType.DinoOne)
        {
            partner1SOData = partnerDatas.Dino1SOData;
            partner2SOData = partnerDatas.Dino2SOData;
            partner3SOData = partnerDatas.Dino3SOData;
        }
        if (partnerFirstStageType == PartnerType.BearOne)
        {
            partner1SOData = partnerDatas.Bear1SOData;
            partner2SOData = partnerDatas.Bear2SOData;
            partner3SOData = partnerDatas.Bear3SOData;
        }  
        if (partnerFirstStageType == PartnerType.AxelOne)
        {
            partner1SOData = partnerDatas.Axel1SOData;
            partner2SOData = partnerDatas.Axel2SOData;
            partner3SOData = partnerDatas.Axel3SOData;
        } 
        if (partnerFirstStageType == PartnerType.RabbitOne)
        {
            partner1SOData = partnerDatas.Rabbit1SOData;
            partner2SOData = partnerDatas.Rabbit2SOData;
            partner3SOData = partnerDatas.Rabbit3SOData;
        }
      
    }
    
}
