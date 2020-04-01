using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesController : MonoBehaviour
{
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;

    public float distanceAttack;
    public Transform currentTarget;

    public Transform ownTransform;

    private bool isInit;

    public void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.currentTarget = GameManager.Instance.GetPlayerTransform();
            this.ownTransform = this.gameObject.transform;
        }
    }

    public void InitStates(CharacterComponents components)
    {
        this.moveState.m_Components = components;
        this.attackState.m_Components = components;

        this.moveState.ChangeAttackTarget(this.currentTarget);
        this.attackState.ChangeAttackTarget(this.currentTarget);
    }

    private void Update()
    {
        if (Vector3.Distance(this.ownTransform.position, this.currentTarget.position) < this.distanceAttack) {
            this.moveState.enabled = false;
            this.attackState.enabled = true;
        } else {
            this.moveState.enabled = false;
            this.attackState.enabled = true;
        }
    }

    private void ChangeMoveTarget(Transform newTarget)
    {
        this.moveState.ChangeAttackTarget(newTarget);
        this.attackState.ChangeAttackTarget(newTarget);
    }

    private void ChangeAttackTarget(Transform newTarget)
    {

    }
}
