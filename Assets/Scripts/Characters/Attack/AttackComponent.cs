using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public int attackPower;

    public void Attack(PowerType powerType)
    {
        GameObject prefab = ResourcesManager.Instance.GetPowerEffectPrefab(powerType);
        PowersBehaviour power = prefab.GetComponent<PowersBehaviour>();
        power.SetPowerData(powerType);
        power.StartAttack();
    }
}
