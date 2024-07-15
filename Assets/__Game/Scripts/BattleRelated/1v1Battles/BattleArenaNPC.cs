using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BattleArenaNPC : BasicNPC
{
    [SerializeField] BattleArenaDataSO battleArenaData;
   // [SerializeField] NPCDataSO nPCData;
   // [SerializeField] string npcID;
    [SerializeField] int stageToChallenge = 1;
    
    PartnerType GetChosenPartner()
    {
       return GameManager.Instance.partnerFirstStageType;
    }


    //this class goes on the actual npc. TODO: functionality for dialogue and any other behaviors.
    public override void Interact()
    {
        base.Interact();
        //TODO refactor this out to worth with the dialogue system. Interact => dialogue => UI selection => CheckIfCanBattle();
        
       
    }

    public void CheckIfCanBattle()
    {
        if (!battleArenaData.hasBeenDefeated)
        {
            Debug.Log("Inside start battle logic");
            GameManager.Instance.currentNPCToBattle = battleArenaData;
            OnStartTheArenaBattle();
        }
    }
    bool HasBattled() //FOR Lua
    {
        if (battleArenaData.hasBeenDefeated)
        {
            return true;
        }
        else return false;
    }
    protected override void Start()
    {
        base.Start();
        //GetData on initialize
        NPCDataManager.Instance.GetNPCData(npcID);
        SetPartner();

    }
    void SetPartner()
    {
        PartnerType partner = GetChosenPartner();
        var partnerMapping = new Dictionary<PartnerType, PartnerType[]>
        {
            { PartnerType.DinoOne, new PartnerType[] { PartnerType.DinoOne, PartnerType.DinoTwo, PartnerType.DinoThree } },
            { PartnerType.BearOne, new PartnerType[] { PartnerType.BearOne, PartnerType.BearTwo, PartnerType.BearThree } },
            { PartnerType.RabbitOne, new PartnerType[] { PartnerType.RabbitOne, PartnerType.RabbitTwo, PartnerType.RabbitThree } },
            { PartnerType.AxelOne, new PartnerType[] { PartnerType.AxelOne, PartnerType.AxelTwo, PartnerType.AxelThree } },
        };

        if (partnerMapping.ContainsKey(partner) && stageToChallenge >= 1 && stageToChallenge <= 3)
        {
            battleArenaData.partnerType = partnerMapping[partner][stageToChallenge - 1];
        }
    }
    private void OnStartTheArenaBattle()
    {
        

        SceneLoaderUtility sceneLoad = new SceneLoaderUtility();
        sceneLoad.LoadScene("BattleArena");

    }

    private void OnEnable()
    {
        Lua.RegisterFunction("CheckIfCanBattle", this, SymbolExtensions.GetMethodInfo(() => CheckIfCanBattle())); 
        Lua.RegisterFunction("HasBattled", this, SymbolExtensions.GetMethodInfo(() => HasBattled())); 
        
    }
    private void OnDisable()
    {
        Lua.UnregisterFunction(nameof(CheckIfCanBattle));
        Lua.UnregisterFunction(nameof(HasBattled));
    }

}
