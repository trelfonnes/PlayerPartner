using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAttackState : EnemyAttackState
{
    EnemyWeapon weapon;
    IEnemyProjectile projectileStrategy;
    Dictionary<int, WeaponDataSO> weaponDatas;
    public EnemyProjectileAttackState(Enemy enemy, EnemyStateMachine ESM, EnemySOData enemySoData, EnemyData data, string animBoolName, EnemyWeapon weapon, IEnemyProjectile projectileStrategy, Dictionary<int, WeaponDataSO> weaponDatas) : base(enemy, ESM, enemySoData, data, animBoolName, weapon)
    {
        this.weapon = weapon;
        this.projectileStrategy = projectileStrategy;
        this.weaponDatas = weaponDatas;
        weapon.onExit += ExitHandler;
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
        projectileStrategy.ShootProjectile(weapon, data, weaponDatas);//now it can communicate with weapon game object and pass any needed data.

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //might need to do this from enter?? //call weapon.enter from concrete strategy
       // if(!isAttackDone)

       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
