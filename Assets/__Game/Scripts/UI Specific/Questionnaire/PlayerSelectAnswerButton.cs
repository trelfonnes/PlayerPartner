using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectAnswerButton : MonoBehaviour
{
    public int answerIndex;
    public PlayerSelect manager;
    
    public void OnButtonClick()
    {
        manager.OnAnswerSelected(answerIndex);

    }



    
}
