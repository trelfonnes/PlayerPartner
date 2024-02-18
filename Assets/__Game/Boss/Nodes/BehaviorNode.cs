using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorNode
{
    public abstract NodeState Execute();
    protected bool singleExecute;
}
public enum NodeState
{
    success,
    running,
    failure
}
