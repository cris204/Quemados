using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int initialHealth;
    public int maxHealth;
    public int currentlHealth;

    public int GetCurrentHealth()
    {
        return this.currentlHealth;
    }

    public void SetHeatlh(int newHealth)
    {
        this.currentlHealth = newHealth;
    }
}
