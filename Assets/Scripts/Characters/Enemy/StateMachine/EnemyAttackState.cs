using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAttackState : EnemyState
{
    public float attackCoolDown;

    private Random randomCD;
    private int maxCD;
    private int minCD;
    private Vector3 aimDirection;
    private bool canAttack;

    private void Start()
    {
        this.maxCD = 3;
        this.minCD = 1;
        this.canAttack = true;
        this.randomCD = new Random();
        this.attackCoolDown = this.randomCD.Next(this.minCD, this.maxCD + 1);
    }

    private void Update()
    {
        this.Attack();
    }

    private void Attack()
    {
        if (!this.canAttack)
            return;

        this.canAttack = false;
        this.m_Components.Attack.Attack(PowerType.BasicThrowBall, this.m_Components, this.attackTarget.localPosition.normalized);
        StartCoroutine(AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        yield return this.attackCoolDown;
        this.canAttack = true;
    }
}
