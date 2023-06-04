using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
//the interface gives all inheriting components access
{
    protected CoreHandler core;
    protected Player player;
    protected Partner partner;
    protected SpriteRenderer SR;
    //?? is coalescing operator. if left is null, returns right.
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    protected Defeated Defeated { get => defeated ?? core.GetCoreComponent(ref defeated); }
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Stats stats;
    private Defeated defeated;
    private Particles particles;


    // TODO set references to other CoreComponents
    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
        partner = GetComponentInParent<Partner>();
        core = transform.parent.GetComponent<CoreHandler>();
        SR = GetComponentInParent<SpriteRenderer>();
        if (core == null) { Debug.LogError("There is no CoreHandler on the Parent"); }
        core.AddComponent(this);
    }
    protected virtual void Start()
    {

    }
    private void Update()
    {
        LogicUpdate();
    }

    public virtual void LogicUpdate() { }


}
