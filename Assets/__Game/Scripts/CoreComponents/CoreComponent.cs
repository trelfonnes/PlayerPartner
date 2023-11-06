using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour, ILogicUpdate
//the interface gives all inheriting components access
{
    protected CoreHandler core;
    protected Player player;
    protected Partner partner;
    protected Enemy enemy;
    protected SpriteRenderer SR;
    //?? is coalescing operator. if left is null, returns right.
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    protected PlayerCollisionSenses PlayerCollisionSenses
    {
        get
        {
            if (playerCollisionSenses == null)
            {
                playerCollisionSenses = core.GetCoreComponent(ref playerCollisionSenses);
            }
            return playerCollisionSenses;
        }
    }
    protected PartnerCollisionSenses PartnerCollisionSenses { get => partnerCollisionSenses ?? core.GetCoreComponent(ref partnerCollisionSenses); }
    protected Stats Stats { get => stats ?? core.GetCoreComponent(ref stats); }
    protected Defeated Defeated { get => defeated ?? core.GetCoreComponent(ref defeated); }
    protected Particles Particles { get => particles ?? core.GetCoreComponent(ref particles); }
    protected EnemyCollisionSenses EnemyCollisionSenses { get => enemyCollisionSenses ?? core.GetCoreComponent(ref enemyCollisionSenses); }
    protected EnemyMovement EnemyMovement { get => enemyMovement ?? core.GetCoreComponent(ref enemyMovement); }
    protected EnemyStats EnemyStats { get => enemyStats ?? core.GetCoreComponent(ref enemyStats); }

    private Movement movement;
    private PlayerCollisionSenses playerCollisionSenses;
    private PartnerCollisionSenses partnerCollisionSenses;
    private Stats stats;
    private Defeated defeated;
    private Particles particles;
    private EnemyCollisionSenses enemyCollisionSenses;
    private EnemyMovement enemyMovement;
    private EnemyStats enemyStats;
    // TODO set references to other CoreComponents
    protected virtual void Awake()
    {
        player = GetComponentInParent<Player>();
        partner = GetComponentInParent<Partner>();
        enemy = GetComponentInParent<Enemy>();
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
