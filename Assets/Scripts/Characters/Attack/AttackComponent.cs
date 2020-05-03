using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public int attackPower;
    //public float attackCoolDown;
    public GameObject spawnPointGO;
    public CharacterType character;

    private bool isInit;

    public AttackComponent()
    {
        this.attackPower = 10;
        //this.attackCoolDown = 1;
    }


    private void Start()
    {
        this.Init();
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            if(this.spawnPointGO == null) {
                Debug.LogWarning("No spawn point assigned. Assigning GameObject itself as spawn point");
                this.spawnPointGO = this.gameObject;
            }
        }
    }

    public void Attack(PowerType powerType, CharacterComponents attacker, Vector3 direction)
    {

        PowersBehaviour power = ResourcesManager.Instance.GetPowerBehaviourPrefab(powerType);
        GameObject prefab = Instantiate(power.gameObject);
        prefab.transform.position = this.spawnPointGO.transform.position;
        PowersBehaviour powerInstantiated = prefab.GetComponent<PowersBehaviour>();
        powerInstantiated.characterThrew = this.character;
        powerInstantiated.SetPower(powerType, attacker);
        powerInstantiated.StartAttack(direction);

    }

}
