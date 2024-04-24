using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : BossCoreComponent
{
    BossProjectileState currentState;

    [SerializeField] BossWeapon weapon;
    bool executeAttack;
    string blackboardAnimBoolName;
    Animator weaponAnim;
    Animator blackboardAnim;
    public Vector2 ShootDirection { get; private set; }

    private void Start()
    {
        weaponAnim = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<BossWeapon>();
        weapon.onExit += AnimationFinished;
        weapon.SetComponentLocator(componentLocator);
        
    }
    public void ShootProjectile(ProjectileType projectileType, Vector2 direction) //called from node
    {
        // send the type from the blackboard and this transform position to projectile handler
        currentState = BossProjectileState.active;
        ProjectileEventSystem.Instance.RaiseSetProjectileTypeEvent(projectileType);
        ProjectileEventSystem.Instance.RaiseBossDirectionSetEvent(transform.position, direction.normalized, 1, 5);
    }
    public void ShootDirectionalProjectile(ProjectileType projType, Vector2 target, Animator blackboardAnim, string animBoolName, float moveX, float moveY)
    {

        this.blackboardAnim = blackboardAnim;
        blackboardAnimBoolName = animBoolName;
        ShootDirection = target;
        Movement.MoveOnOff(false);
        int roundedMoveX = Mathf.RoundToInt(moveX);
        int roundedMoveY = Mathf.RoundToInt(moveY);
        blackboardAnim.SetBool(animBoolName, true);
        weaponAnim.SetBool("attack", true);
        weaponAnim.SetFloat("MoveX", roundedMoveX);
        weaponAnim.SetFloat("MoveY", roundedMoveY);
        currentState = BossProjectileState.active;
        weapon.Enter();


    }
    public void ProjectileAnimDirection(Animator blackboardAnim, string animBoolName, float moveX, float moveY)
    {
        this.blackboardAnim = blackboardAnim;
        blackboardAnimBoolName = animBoolName;
        blackboardAnim.SetBool(animBoolName, true);
        weaponAnim.SetBool("attack", true);
        weaponAnim.SetFloat("MoveX", moveX);
        weaponAnim.SetFloat("MoveY", moveY);
        executeAttack = true;
        currentState = BossProjectileState.active;
        weapon.Enter();
    }
    public void AnimationCoolDown()
    {
        currentState = BossProjectileState.coolDown;
    }
    public void AnimationFinished()
    {
        blackboardAnim.SetBool(blackboardAnimBoolName, false);
        weaponAnim.SetBool("attack", false);
        executeAttack = false;
        currentState = BossProjectileState.idle;
        Movement.MoveOnOff(true);
    }
    private void OnDisable()
    {
        weapon.onExit -= AnimationFinished;
    }
    public BossProjectileState GetCurrentProjectileState()
    {
        return currentState;
    }

}
public enum BossProjectileState
{
    idle,
    active,
    coolDown
}
