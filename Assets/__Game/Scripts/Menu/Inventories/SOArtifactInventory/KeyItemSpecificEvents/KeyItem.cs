using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyItem 
{
    public string Name;
    public string Description;
  public ArtifactInventoryItems keyItemInventoryDescriptionSO;

    //additional properties related to key items can be added here

    public KeyItem(string name, string description)
    {
        Name = name;
        Description = description;
    }

}
