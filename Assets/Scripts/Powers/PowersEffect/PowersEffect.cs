using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowersEffect : MonoBehaviour
{
    public CharacterComponents attacker;
    public CharacterComponents victim;
    public Collider ownCollider;
    public float aliveTime;
    protected WaitForSeconds destroyDelay;
    protected bool isInit;
    public abstract void InitCompoenents();
    public abstract void InitEffect(Power data, CharacterComponents attacker);
    public abstract void StartToEffect();
    public abstract void TriggerPowerEffect();
    public abstract void Effect();
}
