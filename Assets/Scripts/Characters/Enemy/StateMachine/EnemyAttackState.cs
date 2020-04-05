using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyAttackState : EnemyState
{
    private Vector3 aimDirection;
    private void Update()
    {
        this.Attack();
    }

    private void Attack()
    {
        if (this.attackTarget == null)
            return;

        this.m_Components.Attack.Attack(PowerType.BasicThrowBall, this.m_Components, (this.attackTarget.localPosition - this.transform.position).normalized);
    }
}
