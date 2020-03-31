using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public int attackPower;
    public GameObject spawnPointGO;

    public void Attack(PowerType powerType)
    {
        GameObject prefab = Instantiate(ResourcesManager.Instance.GetPowerEffectPrefab(powerType));
        prefab.transform.position = this.spawnPointGO.transform.position;
        PowersBehaviour power = prefab.GetComponent<PowersBehaviour>();
        power.SetPowerData(powerType);
        power.StartAttack();
    }
}
