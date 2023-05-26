using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
    //the interface gives all inheriting components access
{
    protected CoreHandler core;
    protected Player player;
    protected SpriteRenderer SR;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }

    private Movement movement;
    // TODO set references to other CoreComponents
    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
        core = transform.parent.GetComponent<CoreHandler>();
        SR = GetComponentInParent<SpriteRenderer>();
        if(core == null) { Debug.LogError("There is no CoreHandler on the Parent"); }
        core.AddComponent(this);
    }
    public virtual void LogicUpdate() { }
}
