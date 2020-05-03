using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBMRandomInt : StateMachineBehaviour
{
    public int numberOfStates = 2;
    public int stateIndex;
    private int repeatCounter;
    //public float minNormTime = 0f;
    //public float maxNormTime = 5f;

    //protected float m_RandomNormTime;

    readonly int m_HashRandomAttack = Animator.StringToHash("RandomInt");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Randomly decide a time at which to transition.
        //m_RandomNormTime = Random.Range(minNormTime, maxNormTime);
        int newIndex = this.GetNewIndex();
        animator.SetInteger(m_HashRandomAttack, newIndex);
    }

    private int GetNewIndex()
    {
        if (numberOfStates <= 1)
            return 0;

        int newIndex = Random.Range(0, numberOfStates);
        if (newIndex == this.stateIndex) {
            if (this.repeatCounter > 0) {
                int tries = 0;
                while (newIndex == this.stateIndex) {
                    newIndex = Random.Range(0, numberOfStates);
                    tries++;
                    if (tries > 5)
                        break;
                }
                this.repeatCounter = 0;
            }
            this.repeatCounter++;
        } else {
            this.repeatCounter = 0;
        }

        return newIndex;
    }

    /*
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // If trainsitioning away from this state reset the random idle parameter to -1.
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash) {
            animator.SetInteger(m_HashRandomAttack, -1);
        }

        // If the state is beyond the randomly decided normalised time and not yet transitioning then set a random idle.
        if (stateInfo.normalizedTime > m_RandomNormTime && !animator.IsInTransition(0)) {
            animator.SetInteger(m_HashRandomAttack, Random.Range(0, numberOfStates));
        }
    }
    */
}