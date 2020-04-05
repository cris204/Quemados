using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public int attackPower;
    public float attackCoolDown;
    public GameObject spawnPointGO;

    private bool canAttack=true;
    private bool isInit;
    private WaitForSeconds attackDelay;

    public bool CanAttack {
        get => this.canAttack;
        set => this.canAttack = value;
    }

    public AttackComponent()
    {
        this.attackPower = 10;
        this.attackCoolDown = 1;
    }


    private void Start()
    {
        this.Init();
        this.SetAttackCoolDown(1);
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.attackDelay = new WaitForSeconds(this.attackCoolDown);
            if(this.spawnPointGO == null) {
                Debug.LogWarning("No spawn point assigned. Assigning GameObject itself as spawn point");
                this.spawnPointGO = this.gameObject;
            }
        }
    }

    public void SetAttackCoolDown(float newCoolDown)
    {
        this.attackCoolDown = newCoolDown;
        this.attackDelay = new WaitForSeconds(newCoolDown);
    }

    public void Attack(PowerType powerType, CharacterComponents attacker, Vector3 direction)
    {
        if (!this.CanAttack)
            return;

        this.CanAttack = false;
        PowersBehaviour power = ResourcesManager.Instance.GetPowerBehaviourPrefab(powerType);
        GameObject prefab = Instantiate(power.gameObject);
        prefab.transform.position = this.spawnPointGO.transform.position;
        PowersBehaviour powerInstantiated = prefab.GetComponent<PowersBehaviour>();
        powerInstantiated.SetPower(powerType, attacker);
        powerInstantiated.StartAttack(direction);

        StartCoroutine(this.AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        yield return this.attackDelay;
        this.CanAttack = true;
    }
}
