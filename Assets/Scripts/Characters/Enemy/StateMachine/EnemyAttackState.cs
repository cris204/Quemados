using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAttackState : EnemyState
{
    public AttackComponent m_Attack;
    public float attackCoolDown;

    private Random randomCD;
    private int maxCD;
    private int minCD;

    private void Start()
    {
        this.maxCD = 3;
        this.maxCD = 1;
        this.randomCD = new Random();
        this.attackCoolDown = this.randomCD.Next(this.minCD, this.maxCD + 1);
    }

    private void Attack()
    {
        this.AimToTarget();
        this.m_Attack.Attack(PowerType.BasicThrowBall, this.m_Components);
    }

    private void AimToTarget()
    {
    }
}
