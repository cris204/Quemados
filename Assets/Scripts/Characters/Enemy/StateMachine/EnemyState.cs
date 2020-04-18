using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public CharacterComponents m_Components;
    protected Transform ownTransform;
    protected Transform attackTarget;
    protected Transform moveTarget;

    public void ChangeAttackTarget(Transform newTarget)
    {
        this.attackTarget = newTarget;
    }

    public virtual void ChangeMoveTarget(Transform newTarget)
    {
        this.moveTarget = newTarget;
    }

    public void ChangeOwnTransform(Transform newTarget)
    {
        this.ownTransform = newTarget;
    }
}
