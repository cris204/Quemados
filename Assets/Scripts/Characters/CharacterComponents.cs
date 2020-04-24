using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { 
    Player,
    Enemy
}

[RequireComponent (typeof (HealthComponent), typeof (AttackComponent))]
public class CharacterComponents : MonoBehaviour, IEqualityComparer<CharacterComponents>
{
    public string id = string.Empty;

    private HealthComponent m_Health;
    private AttackComponent m_Attack;
    private DashComponent m_Dash;
    private ExperienceComponent m_Experience;

    public CharacterType character;

    private bool isInit;

    public HealthComponent Health { get => m_Health;}
    public AttackComponent Attack { get => m_Attack;}
    public DashComponent Dash { get => m_Dash;}
    public ExperienceComponent Experience { get => m_Experience;}

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
            this.m_Dash = GetComponent<DashComponent>();
            this.m_Experience = GetComponent<ExperienceComponent>();
        }
    }

    public void ChangeCharacterType()
    {
        this.m_Attack.character = this.character;
    }

    public void SuscribeDeathAction(Action deathAction)
    {
        this.Health.SuscribeToDeathAction(deathAction);
    }

    public void DesuscribeDeathAction(Action deathAction)
    {
        this.Health.DesuscribeToDeathAction(deathAction);
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
