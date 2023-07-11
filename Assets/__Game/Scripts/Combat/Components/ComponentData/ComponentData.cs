using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ComponentData 
{
    [SerializeField, HideInInspector] private string name;


    public Type ComponentDependency { get; protected set; }


    public void SetComponentName() => name = GetType().Name;

    public ComponentData()
    {
        SetComponentName(); //construction passing in this component on initialization and creating name in inspector
    }
    
    public virtual void SetAttackDataNames() { }
    public virtual void InitializeAttackData(int numberOfAttacks) { }

}
[Serializable]
public class ComponentData<T> : ComponentData where T : AttackData 
{
    [SerializeField] private T[] attackData;

    public T[] AttackData { get => attackData; private set => attackData = value; }
    //function to loop trhough array and call the "name Attack Function in AttackData for every attack
    public override void SetAttackDataNames()
    {
        base.SetAttackDataNames();
        for (int i = 0; i < AttackData.Length; i++)
        {
            AttackData[i].SetAttackName(i + 1);
        }
    }
    public override void InitializeAttackData(int numberOfAttacks) //makes sure attackdata array has correct length. mine only have one attack so may not be of a lot of use in this project.
    {
        base.InitializeAttackData(numberOfAttacks);
        var oldLength = attackData != null ? attackData.Length : 0; // oldLength equals attack data if not null otherwise, if it is, set to 0
        
        if(oldLength == numberOfAttacks)
        {
            return;
        }
        Array.Resize(ref attackData, numberOfAttacks);
        if(oldLength < numberOfAttacks)
        {
            for (var i = oldLength; i < attackData.Length; i++)
            {
                var newObj = Activator.CreateInstance(typeof(T)) as T;
                attackData[i] = newObj;
            }
        }
        SetAttackDataNames();
    }
}
 