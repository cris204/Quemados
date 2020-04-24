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

    private int currentXp;
    private bool isAnimating;
    private Queue<ChangeValuesAnimated> animatedChanges = new Queue<ChangeValuesAnimated>();

    private void Start()
    {
        this.fillerImageRT = this.xpFillerImage.gameObject.GetComponent<RectTransform>();
        this.progressTextRT = this.xpProgressText.gameObject.GetComponent<RectTransform>();

        this.SetXPText(0, this.nextLevelXP);
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
                this.SetXPText(currentValue, finalValue);
            });

        //Image
        float xSize = ((this.fillerImageRT.sizeDelta.x - 0) / (IMAGE_MAX_SIZE - 0));
        Vector2 newSize = this.fillerImageRT.sizeDelta;
        newSize.x = xSize;
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

    private void SetXPText(int current, int final)
    {
        this.xpProgressText.text = string.Format("{0}/{1}", current, final);
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
