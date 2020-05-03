using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterType character;
    private CharacterComponents m_Components;
    private Rigidbody rigidbody;
    private Collider collider;
    private EnemyStatesController states;
    private EnemyData data;
    private Animator enemyAnimator;
    private bool isInit;

    private void Awake()
    {
        this.Init();
    }

    private void Start()
    {
        this.InitStates();
        this.m_Components.Health.SuscribeToDeathAction(this.Death);
        this.enemyAnimator = GetComponent<Animator>();
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.TakeRandomData();
            this.m_Components = this.gameObject.GetComponent<CharacterComponents>();
            this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
            this.collider = this.gameObject.GetComponent<Collider>();
            this.states = this.gameObject.GetComponent<EnemyStatesController>();
        }
    }

    private void TakeRandomData()
    {
        int randomIndex = Random.Range(0, EnemiesDataBase.enemies.Count);
        this.data = EnemiesDataBase.enemies[randomIndex];
    }

    public void ActiveEnemy()
    {
        this.states.SetIsActive(true);
    }

    public void DisactiveEnemy()
    {
        this.states.SetIsActive(false);
    }

    private void InitStates()
    {
        this.m_Components.character = this.character;
        this.m_Components.ChangeCharacterType();
        this.states.InitStates(this.m_Components, this.data);
    }

    private void Death()
    {
        EventManager.Instance.Trigger(new KilledEnemyEvent() { 
            enemyData = this.data,
        });
        this.m_Components.Health.DesuscribeToDeathAction(this.Death);
        Destroy(this.gameObject);
    }

    #region TriggerAnimations
    public void TriggerAnimation(string animationTrigger)
    {
        this.enemyAnimator.SetTrigger(animationTrigger);
    }

    #endregion
}
