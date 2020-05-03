using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAttackState : EnemyState
{
    private Vector3 aimDirection;
    private EnemyController enemyController;
    private WaitForSeconds attackDelay;
    private float attackCoolDown = 1;
    private bool canAttack = true;
    public bool CanAttack
    {
        get => this.canAttack;
        set => this.canAttack = value;
    }
    private void Start()
    {
        this.enemyController = GetComponent<EnemyController>();
        this.attackDelay = new WaitForSeconds(this.attackCoolDown);
    }

    public override void EnterState()
    {
        base.EnterState();
        this.UpdateState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        this.Attack();
    }

    private void Attack()
    {
        if (this.attackTarget == null || !this.CanAttack)
            return;
        this.CanAttack = false;
        this.enemyController.TriggerAnimation("BasicAttack");
        this.m_Components.Attack.Attack(PowerType.BasicThrowBall, this.m_Components, (this.attackTarget.localPosition - this.transform.position).normalized);
        StartCoroutine(this.AttackCoolDown());
    }


    private IEnumerator AttackCoolDown()
    {
        yield return this.attackDelay;
        this.CanAttack = true;
    }


}
