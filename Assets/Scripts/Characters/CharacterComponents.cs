using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (HealthComponent), typeof (AttackComponent))]
public class CharacterComponents : MonoBehaviour, IEqualityComparer<CharacterComponents>
{
    public string id = string.Empty;

    private HealthComponent m_Health;
    private AttackComponent m_Attack;

    private bool isInit;

    public HealthComponent Health { get => m_Health;}
    public AttackComponent Attack { get => m_Attack;}

    private void Awake()
    {
        this.Init();
        if (string.IsNullOrEmpty(this.id)){
            this.id = Guid.NewGuid().ToString();
        }
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.m_Health = GetComponent<HealthComponent>();
            this.m_Attack = GetComponent<AttackComponent>();
        }
    }

    public bool Equals(CharacterComponents x, CharacterComponents y)
    {
        if(y == null) {
            if(x == null) {
                return true;
            } else {
                return false;
            }
        }
        if(x.id == y.id) {
            return true;
        }
        return false;
    }

    public int GetHashCode(CharacterComponents obj)
    {
        return this.gameObject.GetHashCode();
    }
}
