using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public CharacterComponents m_Components;
    private Transform ownTransform;
    private Transform attackTarget;
    private Transform moveTarget;

    public void ChangeAttackTarget(Transform newTarget)
    {
        this.attackTarget = newTarget;
    }

    public void ChangeMoveTarget(Transform newTarget)
    {
        this.moveTarget = newTarget;
    }

    public void SetOwnTransform(Transform newTarget)
    {
        this.ownTransform = newTarget;
    }
}
