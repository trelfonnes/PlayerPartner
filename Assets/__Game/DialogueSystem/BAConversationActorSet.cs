using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BAConversationActorSet : MonoBehaviour
{//This class is attached to the DST Game Object in the BA scene.
    [SerializeField] Transform malePlayerTransform;
    [SerializeField] Transform femalePlayerTransform;
    [SerializeField] DialogueSystemTrigger DST;
    private void Start()
    {
        DST = GetComponent<DialogueSystemTrigger>();
        if(GameManager.Instance.chosenPlayer == PlayerType.Male)
        {
            DST.conversationActor = malePlayerTransform;
        }
        else
        {
            DST.conversationActor = femalePlayerTransform;
        }
    }
}
