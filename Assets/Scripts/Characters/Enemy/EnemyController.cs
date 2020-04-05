using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CharacterComponents m_Components;
    private Rigidbody rigidbody;
    private Collider collider;
    private EnemyStatesController states;

    private bool isInit;

    private void Awake()
    {
        this.Init();
    }

    private void Start()
    {
        this.InitStates();
        this.m_Components.Health.SuscribeToDeathAction(this.Death);
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.m_Components = this.gameObject.GetComponent<CharacterComponents>();
            this.rigidbody = this.gameObject.GetComponent<Rigidbody>();
            this.collider = this.gameObject.GetComponent<Collider>();
            this.states = this.gameObject.GetComponent<EnemyStatesController>();
        }
    }

    private void InitStates()
    {
        this.states.InitStates(this.m_Components);
    }

    private void Death()
    {
        this.m_Components.Health.DesuscribeToDeathAction(this.Death);
        Destroy(this.gameObject);
    }

}
