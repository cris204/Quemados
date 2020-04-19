using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public CharacterComponents m_Components;
    protected EnemyStateEvent enemyStateEvent;
    protected Transform ownTransform;
    protected Transform attackTarget;
    protected Transform moveTarget;

    private void Update()
    {
        if (this.enemyStateEvent == EnemyStateEvent.Update) {
            this.UpdateState();
        }
    }

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

    public virtual void EnterState() {
        if (this.enemyStateEvent == EnemyStateEvent.Update) {
            return;
        }
        this.enemyStateEvent = EnemyStateEvent.Enter;
    }
    public virtual void UpdateState() { 
        this.enemyStateEvent = EnemyStateEvent.Update;
    }
    public virtual void ExitState() {
        if (this.enemyStateEvent == EnemyStateEvent.Exit) {
            return;
        }
        this.enemyStateEvent = EnemyStateEvent.Exit;
    }
}

public enum EnemyStateEvent
{
    Enter,
    Update,
    Exit,
}
