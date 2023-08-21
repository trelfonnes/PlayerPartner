using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PrimaryWeaponState
{
    MeleeBasic,
    MeleeHold,
    NA
}
public enum SecondaryWeaponState
{
    BasicProjectile,
    SpreadProjectile,
    ChargeProjectile,
    NA
}
public class PartnerWeaponState 
{
    

    PrimaryWeaponState currentPrimaryState;
    SecondaryWeaponState currentSecondaryState;

    private static PartnerWeaponState instance;

    public PartnerWeaponState()
    {
        currentPrimaryState = PrimaryWeaponState.MeleeBasic;
        currentSecondaryState = SecondaryWeaponState.NA;
    }
    public static PartnerWeaponState GetInstance()
    {
        if (instance == null)
        {
            instance = new PartnerWeaponState();
        }
        return instance;
    }
    public void SwitchPrimaryState(PrimaryWeaponState newState)
    {
        ExitPrimaryState(currentPrimaryState);
        currentPrimaryState = newState;
        EnterPrimaryState(newState);
    }

    
    public void SwitchSecondaryState(SecondaryWeaponState newState)
    {
        ExitSecondaryState(currentSecondaryState);
        currentSecondaryState = newState;
        EnterSecondaryState(newState);
    }


    private void ExitPrimaryState(PrimaryWeaponState currentPrimaryState)
    {

    }
    private void ExitSecondaryState(SecondaryWeaponState currentSecondaryState)
    {

    }
    private void EnterPrimaryState(PrimaryWeaponState newState)
    {
        switch (newState)
        {
            case PrimaryWeaponState.MeleeBasic:
                // Enter MeleeBasic state actions
                break;
            case PrimaryWeaponState.MeleeHold:
                // Enter MeleeHold state actions
                break;
            
        }
    }

    private void EnterSecondaryState(SecondaryWeaponState newState)
    {
        switch (newState)
        {
            case SecondaryWeaponState.BasicProjectile:
                // Enter BasicProjectile state actions
                break;
            case SecondaryWeaponState.SpreadProjectile:
                // Enter SpreadProjectile state actions
                break;
            case SecondaryWeaponState.ChargeProjectile:
                // Enter ChargeProjectile state actions
                break;
        }
    }

    //function to return what currentstate is
    
    public PrimaryWeaponState GetCurrentPrimaryState()
    {
        return currentPrimaryState;
    }

    public SecondaryWeaponState GetCurrentSecondaryState()
    {
        return currentSecondaryState;
    }


}
