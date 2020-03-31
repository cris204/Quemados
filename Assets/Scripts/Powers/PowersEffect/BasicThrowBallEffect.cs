using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicThrowBallEffect : PowersEffect
{
    public int attackPower;
    public Collider collider;

    public override void InitEffect(Power data)
    {
        this.attackPower = data.attackPower;
        collider.enabled = false;
    }

    public override void StartToEffect()//Could use a coroutine for delay effect
    {
        collider.enabled = true;
    }

    public override void TriggerPowerEffect() 
    {
        this.Effect();
    }

    public override void Effect()
    {
        int newHealt = Mathf.Clamp(this.attackPower + this.attacker.m_Attack.attackPower, 0, this.victim.m_Health.maxHealth);
        this.victim.m_Health.SetHeatlh(newHealt); 
    }
    int colliders = 1;
    private void OnTriggerEnter(Collider other)
    {
        CharacterComponents components = other.GetComponent<CharacterComponents>();
        if(components != null) {
            this.victim = components;
            this.TriggerPowerEffect();
        }
    }
}
