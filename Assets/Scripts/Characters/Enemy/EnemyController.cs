using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterComponents m_Components;
    public Rigidbody rigidbody;
    public Collider collider;
    public EnemyStatesController states;

    public bool isInit;

    private void Start()
    {
        this.Init();
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.InitStates();
        }
    }

    private void InitStates()
    {
        this.states.InitStates(this.m_Components);
    }

}
