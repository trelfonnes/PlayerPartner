using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackState
{
    EnemyWeapon weapon;
    IEnemyMelee meleeStrategy;
    private Dictionary<int, WeaponDataSO> weaponDatas;

    public EnemyMeleeAttackState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName, EnemyWeapon weapon, IEnemyMelee meleeStrategy, Dictionary<int, WeaponDataSO> weaponDatas) : base(enemy, ESM, enemySoData,data, animBoolName, weapon)
    {
        this.weapon = weapon;
        this.meleeStrategy = meleeStrategy;
        this.weaponDatas = weaponDatas;
        weapon.onExit += ExitHandler; // TODO: Where to I unsub from. this will bite me eventually...
    }
    void ExitHandler()
    {
        isAttackDone = true;
        AnimationFinishTrigger();

    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        meleeStrategy.Attack(weapon, data, weaponDatas);//now it can communicate with weapon game object and pass any needed data.

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //might need to do this from enter??
        //if(!isAttackDone)
          
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
