using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    protected BossComponentLocator componentLocator;
    protected BossBlackboard blackboard;
    [SerializeField] protected BossStatsSO bossStats;
    protected Timer timer;
    protected BehaviorNode behaviorTreeFirstStageRoot;
    protected BehaviorNode behaviorTreeSecondStageRoot;
    protected BehaviorNode behaviorTreeThirdStageRoot;
  protected virtual void InitializeBehaviorTree()
    {

    }
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
    protected virtual void OnEnable()
    {

    }
    protected virtual void OnDisable()
    {

    }
}
