using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EvolveBehavior : StateMachineBehaviour
{
    public class OnEvolutionEventArgs : EventArgs 
    {
        public int evolutionStage;
        public bool isEvolving;
    }
    public static event EventHandler<OnEvolutionEventArgs> OnStartEvolution;
    public int evolutionStage = 2;
    public bool isEvolving = false;
    public static event EventHandler<OnEvolutionEventArgs> OnEndEvolution;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(OnStartEvolution != null) OnStartEvolution(this, new OnEvolutionEventArgs{ evolutionStage = evolutionStage});
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (OnEndEvolution != null) OnEndEvolution(this, new OnEvolutionEventArgs { evolutionStage = evolutionStage, isEvolving = isEvolving });
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
