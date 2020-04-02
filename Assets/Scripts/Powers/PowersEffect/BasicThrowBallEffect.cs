using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicThrowBallEffect : PowersEffect
{
    public int attackPower;
    public Collider collider;

    public override void InitEffect(Power data, CharacterComponents attacker)
    {
        this.attackPower = data.attackPower;
        this.attacker = attacker;
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
        int damage = this.attackPower + this.attacker.Attack.attackPower;
        int newHealt = Mathf.Clamp(this.victim.Health.currentlHealth - damage, 0, this.victim.Health.maxHealth);
        this.victim.Health.SetHeatlh(newHealt); 
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
