using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public int attackPower;
    public GameObject spawnPointGO;

    public void Attack(PowerType powerType)
    {
        PowersBehaviour power = ResourcesManager.Instance.GetPowerBehaviourPrefab(powerType);
        GameObject prefab = Instantiate(power.gameObject);
        prefab.transform.position = this.spawnPointGO.transform.position;
        power.SetPowerData(powerType);
        power.StartAttack();
    }
}
