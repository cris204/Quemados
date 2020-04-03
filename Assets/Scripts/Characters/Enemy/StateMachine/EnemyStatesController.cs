using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesController : MonoBehaviour
{
    private EnemyMoveState moveState;
    private EnemyAttackState attackState;
    public Transform testPlayer;

    public float distanceAttack;
    private Transform currentTarget;
    private Transform ownTransform;

    private bool isInit;

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.moveState = this.gameObject.GetComponent<EnemyMoveState>();
            this.attackState = this.gameObject.GetComponent<EnemyAttackState>();
            this.currentTarget = testPlayer;//GameManager.Instance.GetPlayerTransform(); Descomment when game manager is implemented
            this.ownTransform = this.gameObject.transform;

            this.moveState.enabled = false;
            this.attackState.enabled = false;
        }
    }

    public void InitStates(CharacterComponents components)
    {
        this.Init();
        this.moveState.m_Components = components;
        this.attackState.m_Components = components;

        this.ChangeMoveTarget(this.currentTarget);
        this.ChangeAttackTarget(this.currentTarget);

        this.moveState.ChangeOwnTransform(this.ownTransform);
        this.attackState.ChangeOwnTransform(this.ownTransform);
    }

    private void Update()
    {
        if (Vector3.Distance(this.currentTarget.position, this.ownTransform.position) < this.distanceAttack) {
            this.moveState.enabled = false;
            this.attackState.enabled = true;
        } else {
            this.moveState.enabled = true;
            this.attackState.enabled = false;
        }
    }

    private void ChangeMoveTarget(Transform newTarget)
    {
        this.moveState.ChangeMoveTarget(newTarget);
        this.attackState.ChangeMoveTarget(newTarget);
    }

    private void ChangeAttackTarget(Transform newTarget)
    {
        this.moveState.ChangeAttackTarget(newTarget);
        this.attackState.ChangeAttackTarget(newTarget);
    }
}
