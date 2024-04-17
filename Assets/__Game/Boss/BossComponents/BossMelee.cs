using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : BossCoreComponent
{
   [SerializeField] BossWeapon weapon;
        public BossMeleeState currentState { get; private set; }
    bool executeAttack;
    string blackboardAnimBoolName;
    Animator weaponAnim;
    Animator blackboardAnim;
   
    private void Start()
    {
        weaponAnim = GetComponentInChildren<Animator>();
        weapon = GetComponentInChildren<BossWeapon>();
        weapon.onExit += AnimationFinished;
        weapon.SetComponentLocator(componentLocator);
    }
    private void Update()
    {
            Debug.Log(currentState);


    }
    public void ExecuteAttack(Animator blackboardAnim, string animBoolName) // called by node and pass in reg. anim ref.
    {
        this.blackboardAnim = blackboardAnim;
        blackboardAnimBoolName = animBoolName;
        blackboardAnim.SetBool(animBoolName, true);
        weaponAnim.SetBool("attack", true);
        executeAttack = true;
        
        currentState = BossMeleeState.active;
        weapon.Enter();
    } public void ExecuteDirectionalAttack(Animator blackboardAnim, string animBoolName, float moveX, float moveY) // called by node and pass in reg. anim ref.
    {
        this.blackboardAnim = blackboardAnim;
        blackboardAnimBoolName = animBoolName;
        Movement.MoveOnOff(false);
        blackboardAnim.SetBool(animBoolName, true);
        weaponAnim.SetBool("attack", true);
        weaponAnim.SetFloat("MoveX", moveX);
        weaponAnim.SetFloat("MoveY", moveY);
        executeAttack = true;
        currentState = BossMeleeState.active;
        weapon.Enter();
    }
    public void AnimationFinished() //called by anim event
    {
        blackboardAnim.SetBool(blackboardAnimBoolName, false);
        weaponAnim.SetBool("attack", false);
        executeAttack = false;
        currentState = BossMeleeState.idle;
        Movement.MoveOnOff(true);
    }

    public BossMeleeState GetCurrentMeleeState()
    {
        return currentState;
    }
    private void OnDisable()
    {
        weapon.onExit -= AnimationFinished;

    }
}
public enum BossMeleeState
{
    idle,
    active,
    coolDown
   
}

