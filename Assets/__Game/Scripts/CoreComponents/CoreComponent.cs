using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
    //the interface gives all inheriting components access
{
    protected CoreHandler core;
    private void Awake()
    {
        core = transform.parent.GetComponent<CoreHandler>();
        if(core == null) { Debug.LogError("There is no CoreHandler on the Parent"); }
        core.AddComponent(this);
    }
    public virtual void LogicUpdate()
    {

    }
}
