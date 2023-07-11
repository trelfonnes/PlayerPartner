using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData 
{
    // name needs to be first variable to change in inspector
    [SerializeField] string name;

    public void SetAttackName(int i) => name = $"Attack{i}"; //formatted string with $ to allow input parameters in string
}
