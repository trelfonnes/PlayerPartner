using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLocationWithTranslator : KeyItemEvent
{
    public EnterLocationWithTranslator(string keyItemName, Action action) : base(keyItemName, EnterLocationAction)
    {
    }

   private static void EnterLocationAction()
    {
        Debug.Log("you have used the key item to enter the location!");
    }

}
