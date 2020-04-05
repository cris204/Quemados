using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int initialHealth;
    public int maxHealth;
    public int currentlHealth;

    private Action deathAction;

    public HealthComponent()
    {
        this.initialHealth = 100;
        this.maxHealth = 100;
        this.currentlHealth = this.initialHealth;
    }

    private void Start()
    {
        this.currentlHealth = this.initialHealth;
    }

    public void SuscribeToDeathAction(Action action)
    {
        this.deathAction += action;
    }

    public void DesuscribeToDeathAction(Action action)
    {
        this.deathAction -= action;
    }

    public int GetCurrentHealth()
    {
        return this.currentlHealth;
    }

    public void SetHeatlh(int newHealth)
    {
        this.currentlHealth = newHealth;
        if(this.currentlHealth == 0) {
            this.deathAction?.Invoke();
        }
    }
}
