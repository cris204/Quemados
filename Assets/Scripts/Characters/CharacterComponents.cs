using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponents : MonoBehaviour, IEqualityComparer<CharacterComponents>
{
    public HealthComponent m_Health;
    public AttackComponent m_Attack;
    public string id = string.Empty;

    private void Awake()
    {
        if (string.IsNullOrEmpty(this.id)){
            this.id = Guid.NewGuid().ToString();
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
