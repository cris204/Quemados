using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public AttackComponent m_Attack;

    private void Attack()
    {
        this.m_Attack.Attack(PowerType.BasicThrowBall, this.m_Components);
    }
}
