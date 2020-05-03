using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBMRandomInt : StateMachineBehaviour
{
    public int numberOfStates = 3;
    public int stateIndex;

    readonly int m_HashRandomAttack = Animator.StringToHash("RandomInt");


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If trainsitioning away from this state reset the random idle parameter to -1.
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash) {
            animator.SetInteger(m_HashRandomAttack, Random.Range(0, numberOfStates));
        }

    }
}