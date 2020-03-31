using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowersEffect : MonoBehaviour
{
    public CharacterComponents attacker;
    public CharacterComponents victim;
    public abstract void InitEffect(Power data);
    public abstract void StartToEffect();
    public abstract void TriggerPowerEffect();
    public abstract void Effect();
}
