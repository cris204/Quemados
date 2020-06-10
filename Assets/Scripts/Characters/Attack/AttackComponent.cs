using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public int attackPower;
    public float attackCoolDown;
    public GameObject spawnPointGO;
    public CharacterType character;

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
        if(this.character == CharacterType.Player) {
            this.attackCoolDown = 0.2f;
        }
        else{
            this.attackCoolDown = newCoolDown;
        }
        this.attackDelay = new WaitForSeconds(this.attackCoolDown);
    }

    public void Attack(PowerType powerType, CharacterComponents attacker, Vector3 direction)
    {
        if (!this.CanAttack)
            return;

        this.CanAttack = false;
        PowersBehaviour power = ResourcesManager.Instance.GetPowerBehaviourPrefab(powerType);
        PowersBehaviour powerInstantiated = PoolManager.Instance.Pop<PowersBehaviour>(Env.POWER_UP_BEHAVIOR_POOL_KEY + powerType.ToString());
        powerInstantiated.gameObject.transform.position = this.spawnPointGO.transform.position;
        powerInstantiated.characterThrew = this.character;
        powerInstantiated.SetPower(powerType, attacker);
        powerInstantiated.StartAttack(direction);

        StartCoroutine(this.AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        yield return this.attackDelay;
        this.CanAttack = true;
    }

    private void InitAllPowerUpsPools()
    {
        for (int i = 0; i < PowersDataBase.powers.Count; i++) {
            PowerType type = PowersDataBase.powers[i].powerType;
            string poolName = string.Format("{0}{1}", Env.POWER_UP_BEHAVIOR_POOL_KEY, type.ToString());
            SimplePool<PowersBehaviour> newPool = PoolManager.Instance.GetPool<PowersBehaviour>(poolName);
            newPool.CreateFunction = (item) => {
                item.gameObject.SetActive(false);
                item.poolName = poolName;
                return item;
            };

            newPool.OnPush = (item) => {
                item.Destroy();
            };

            newPool.OnPop = (item) => {
                item.GetFromPool();
            };
        }
    }
}
