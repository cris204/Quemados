using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceComponent : MonoBehaviour
{
    private int nextLevelXp = 0;
    private int currentLevelXp = 0;

    private int level = 1;
    private int maxLevel = 100;

    private int xpPointsPerLevel = 1;
    private int xpPoints = 0;

    private void Start()
    {
        this.ResetStats();
    }

    private void ResetStats()
    {
        this.level = 1;
        this.nextLevelXp = GetXPByLevel(this.level);
        this.xpPoints = 0;
        this.currentLevelXp = 0;

        EventManager.Instance.Trigger(new OnPlayerLevelUpEvent() { //Maybe change to game finish (or start) when merge with cris
            newLevel = this.level,
            nextLevelXP = this.nextLevelXp,
            shouldTriggerVFX = false,
        });
    }

    public int GetCurrentCumulativeXP()
    {
        return this.currentLevelXp;
    }

    public void GiveXP(int newValue)
    {
        if (this.level >= this.maxLevel)
            return;

        this.currentLevelXp = newValue;


        if(this.currentLevelXp >= this.nextLevelXp) {
            int remainXP = this.currentLevelXp - this.nextLevelXp;
            EventManager.Instance.Trigger(new OnPlayerXPUpdatedEvent {
                newXpValue = this.currentLevelXp - remainXP,
            });
            this.LevelUp();
            this.GiveXP(remainXP);
        } else {
            EventManager.Instance.Trigger(new OnPlayerXPUpdatedEvent {
                newXpValue = this.currentLevelXp,
            });
        }
    }

    public void GiveXpPoints(int pointsToGive)
    {
        this.xpPoints += pointsToGive;
    }

    private void LevelUp()
    {
        this.level++;
        if (this.level >= this.maxLevel) {
            this.level = this.maxLevel;
            return;
        }
        this.xpPoints += this.xpPointsPerLevel;
        this.currentLevelXp = 0;
        this.nextLevelXp = GetXPByLevel(this.level);

        EventManager.Instance.Trigger(new OnPlayerLevelUpEvent() {
            newLevel = this.level,
            nextLevelXP = this.nextLevelXp,
        });

    }

    private int GetXPByLevel(int level)
    {
        int xp = Mathf.CeilToInt(Mathf.Pow(level, 2) + 20);
        return xp;
    }
}
