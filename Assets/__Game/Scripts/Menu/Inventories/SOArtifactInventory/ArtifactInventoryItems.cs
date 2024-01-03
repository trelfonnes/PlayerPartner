using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Artifact", menuName = "Inventory/Artifacts")]

public class ArtifactInventoryItems : ScriptableObject
{
    public string artifactName;
    public string artifactDescription;
    public Sprite artifactImage;
    public Sprite largeDescriptionImage;
    public bool isKeyItem;
    public bool isLoreItem;
   
    
   
}
