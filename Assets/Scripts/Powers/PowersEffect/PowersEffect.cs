using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowersEffect : MonoBehaviour
{
    public CharacterComponents attacker;
    public CharacterComponents victim;
    public abstract void TriggerPowerEffect();
    public abstract void Effect();
    public abstract void FillData(Power data);
}
