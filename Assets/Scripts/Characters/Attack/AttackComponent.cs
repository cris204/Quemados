using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public int attackPower;
    public GameObject spawnPointGO;

    public void Attack(PowerType powerType, CharacterComponents attacker)
    {
        PowersBehaviour power = ResourcesManager.Instance.GetPowerBehaviourPrefab(powerType);
        GameObject prefab = Instantiate(power.gameObject);
        prefab.transform.position = this.spawnPointGO.transform.position;
        PowersBehaviour powerInstantiated = prefab.GetComponent<PowersBehaviour>();
        powerInstantiated.SetPower(powerType, attacker);
        powerInstantiated.StartAttack();
    }
}
