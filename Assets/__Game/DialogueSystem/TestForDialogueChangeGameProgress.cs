using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForDialogueChangeGameProgress : MonoBehaviour
{

    [SerializeField] bool act1;
    [SerializeField] bool act2;
    [SerializeField] bool act3;
    [SerializeField] bool act4;

    void Start()
    {
        SetAct();
    }

    void SetAct()
    {
        if (act1)
        {
            GameManager.Instance.ChangeCurrentGameProgress(ProgressMarker.act1);
        }
        if (act2)
        {
            GameManager.Instance.ChangeCurrentGameProgress(ProgressMarker.act2);
        }
        if (act3)
        {
            GameManager.Instance.ChangeCurrentGameProgress(ProgressMarker.act3);
        }
        if (act4)
        {
            GameManager.Instance.ChangeCurrentGameProgress(ProgressMarker.act4);
        }
    }
}
