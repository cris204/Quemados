using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicThrowBallPowerEffect : PowersEffect
{
    public int attackPower;
    public override void FillData(Power data)
    {
        this.attackPower = data.attackPower;
    }

    public override void Effect()
    {
        int newHealt = Mathf.Clamp(this.attackPower + this.attacker.m_Attack.attackPower, 0, this.victim.m_Health.maxHealth);
        this.victim.m_Health.SetHeatlh(newHealt); 
    }

    public override void TriggerPowerEffect() //Could use a coroutine for delay effect
    {
        this.Effect();
    }
}
