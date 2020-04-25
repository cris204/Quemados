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

    [SerializeField] private TextMeshProUGUI xpProgressText;
    private RectTransform progressTextRT;

    //Need to know the next level experience !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private int nextLevelXP;
    private int lastLevelXP;

    private int currentXp;
    private bool isAnimating;
    private Queue<ChangeValuesAnimated> animatedChanges = new Queue<ChangeValuesAnimated>();

    private void Awake()
    {
        EventManager.Instance.AddListener<OnPlayerLevelUpEvent>(this.OnPlayerLevelUp);
        this.fillerImageRT = this.xpFillerImage.gameObject.GetComponent<RectTransform>();
        this.progressTextRT = this.xpProgressText.gameObject.GetComponent<RectTransform>();

        this.SetXPText(0);
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
    }

    private void OnPlayerLevelUp(OnPlayerLevelUpEvent e)
    {
        this.lastLevelXP = this.nextLevelXP;
        this.nextLevelXP = e.nextLevelXP;
        if (!e.shouldTriggerVFX) { //NEED TO IMPROVE TO GAME FINISH WHEN MERGE WITH CRIS ROUNDS FEATURE
            this.lastLevelXP = 0;
            this.ResetUI();
        } 
    }

    public void UpdateXP(int newValue)
    {
        ChangeValuesAnimated newChange = new ChangeValuesAnimated(this.currentXp, newValue);
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

        //Text
        Tween textTween = DOTween.To(() => currentValue, x => currentValue = x, finalValue, 0.5f).
            OnUpdate(delegate ()
            {
                this.SetXPText(currentValue);
            });

        //Image
        float xSize = 0;
        if(this.lastLevelXP <= 0) {
            xSize = 0;
        } else {
            xSize = ((finalValue - this.lastLevelXP) / (this.nextLevelXP - this.lastLevelXP));
        }
        Vector2 newSize = this.fillerImageRT.sizeDelta;
        newSize.x = xSize * 100;
        Tween imageTween = this.fillerImageRT.DOSizeDelta(newSize, 0.5f);

        xpSequence.OnComplete(delegate ()
        {
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
        this.xpProgressText.text = string.Format("{0}/{1}", current, this.nextLevelXP);
    }
}

public struct ChangeValuesAnimated
{
    public ChangeValuesAnimated(float currentValue,float finalValue)
    {
        this.currentValue = currentValue;
        this.finalValue = finalValue;
    }

    public float currentValue;
    public float finalValue;
}
