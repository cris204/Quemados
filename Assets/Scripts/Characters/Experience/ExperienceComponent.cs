using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceComponent : MonoBehaviour
{
    private int nextLevelXp = 0;
    private int currentCumulativeXp = 0;

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
        this.currentCumulativeXp = 0;

        EventManager.Instance.Trigger(new OnPlayerLevelUpEvent() { //Maybe change to game finish (or start) when merge with cris
            nextLevelXP = this.nextLevelXp,
            shouldTriggerVFX = false,
        });
    }

    public int GetCurrentCumulativeXP()
    {
        return this.currentCumulativeXp;
    }

    public void GiveXP(int newValue)
    {
        if (this.level >= this.maxLevel)
            return;

        this.currentCumulativeXp = newValue;

        EventManager.Instance.Trigger(new OnPlayerXPUpdatedEvent {
            newXpValue = this.currentCumulativeXp,
        });

        if(this.currentCumulativeXp >= this.nextLevelXp) {
            int remainXP = this.currentCumulativeXp - this.nextLevelXp;
            this.GiveXP(remainXP);
            this.LevelUp();
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
        this.nextLevelXp = GetXPByLevel(this.level);

        EventManager.Instance.Trigger(new OnPlayerLevelUpEvent() {
            nextLevelXP = this.nextLevelXp,
        });

    }

    private int GetXPByLevel(int level)
    {
        int xp = Mathf.CeilToInt(Mathf.Log(level, 3) + 20);
        return xp;
    }
}
