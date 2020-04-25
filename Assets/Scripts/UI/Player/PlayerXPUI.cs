using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG;
using DG.Tweening;

public class PlayerXPUI : MonoBehaviour
{
    private const float IMAGE_MAX_SIZE = 280;

    [SerializeField] private Image xpFillerImage;
    private RectTransform fillerImageRT;

    [SerializeField] private TextMeshProUGUI xpProgresText;
    private RectTransform progresTextRT;

    [SerializeField] private TextMeshProUGUI levelText;
    private RectTransform levelTextRT;

    //Need to know the next level experience !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private int nextLevelXP;
    private int lastLevelXP;
    private int currentLevel;
    private int currentXp;
    private bool isAnimating;
    private Queue<ChangeValuesAnimated> animatedChanges = new Queue<ChangeValuesAnimated>();

    private void Awake()
    {
        EventManager.Instance.AddListener<OnPlayerLevelUpEvent>(this.OnPlayerLevelUp);
        this.fillerImageRT = this.xpFillerImage.gameObject.GetComponent<RectTransform>();
        this.progresTextRT = this.xpProgresText.gameObject.GetComponent<RectTransform>();

        this.SetXPText(0);
        this.SetLevelText(1);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<OnPlayerLevelUpEvent>(this.OnPlayerLevelUp);
        }
    }

    private void ResetUI()
    {
        this.UpdateXP(0);
        this.SetLevelText(this.currentLevel);
    }

    private void OnPlayerLevelUp(OnPlayerLevelUpEvent e)
    {
        this.lastLevelXP = this.nextLevelXP;
        this.nextLevelXP = e.nextLevelXP;
        this.currentLevel = e.newLevel;
        if (!e.shouldTriggerVFX) { //NEED TO IMPROVE TO GAME FINISH WHEN MERGE WITH CRIS ROUNDS FEATURE
            this.lastLevelXP = 0;
            this.ResetUI();
        } 
    }

    public void UpdateXP(int newValue)
    {
        ChangeValuesAnimated newChange = new ChangeValuesAnimated(this.currentXp, newValue, this.nextLevelXP, this.currentLevel);
        this.currentXp = newValue;
        this.animatedChanges.Enqueue(newChange);

        if (!this.isAnimating) {
            this.isAnimating = true;
            this.AnimateChanges();
        }
    }

    private void AnimateChanges()
    {
        Sequence xpSequence = DOTween.Sequence();
        ChangeValuesAnimated values = this.animatedChanges.Dequeue();

        int currentValue = (int)values.currentValue;
        int finalValue = (int)values.finalValue;
        int nextLevelValue = (int)values.nextLevelValue;
        int levelValue = values.levelValue;

        //Text
        Tween textTween = DOTween.To(() => currentValue, x => currentValue = x, finalValue, 0.5f).
            OnUpdate(delegate ()
            {
                this.SetXPText(currentValue);
            });

        //Image
        float xSize = 0;
        xSize = (((float)finalValue - 0) / ((float)this.nextLevelXP - 0));
        Vector2 newSize = this.fillerImageRT.sizeDelta;
        newSize.x = xSize * IMAGE_MAX_SIZE;
        Tween imageTween = this.fillerImageRT.DOSizeDelta(newSize, 0.5f);

        xpSequence.OnStart(() => this.SetLevelText(levelValue));

        xpSequence.OnComplete(delegate ()
        {
            this.SetLevelText(levelValue);

            if (finalValue >= nextLevelValue) {
                this.SetXPText(0);
                newSize.x = 0;
                this.fillerImageRT.sizeDelta = newSize;
            }

            if(this.animatedChanges != null) {
                if(this.animatedChanges.Count > 0) {
                    this.AnimateChanges();
                } else {
                    this.isAnimating = false;
                }
            }
        });

        xpSequence.Append(textTween);
        xpSequence.Join(imageTween);
    }

    private void SetXPText(int current)
    {
        this.xpProgresText.text = string.Format("{0}/{1}", current, this.nextLevelXP);
    }

    private void SetLevelText(int currentLevel)
    {
        this.levelText.text = string.Format("{0}\nLevel", currentLevel);
    }
}

public struct ChangeValuesAnimated
{
    public ChangeValuesAnimated(float currentValue, float finalValue, float nextLevel, int levelValue)
    {
        this.currentValue = currentValue;
        this.finalValue = finalValue;
        this.nextLevelValue = nextLevel;
        this.levelValue = levelValue;
    }

    public int levelValue;
    public float currentValue;
    public float finalValue;
    public float nextLevelValue;
}
