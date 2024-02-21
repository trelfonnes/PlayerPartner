using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildActivationTracker : MonoBehaviour
{
    [SerializeField] GameObject[] childObjects;

    [SerializeField] BossStunned bossStunned;
    private void AllChildrenActive()
    {
        if (bossStunned)
        {
            bossStunned.ChangeStunState(StunState.active);
        }
    }
    private void OneOrMoreInnactive()
    {
        if (bossStunned)
        {
            bossStunned.ChangeStunState(StunState.idle);
        }
    }
    private void Update()
    {
        bool allActive = true;
        foreach (GameObject child in childObjects)
        {
            if (!child.activeSelf)
            {
                OneOrMoreInnactive();
                allActive = false;
            }
            if (allActive)
            {
                AllChildrenActive();
            }
        }
    }

}
