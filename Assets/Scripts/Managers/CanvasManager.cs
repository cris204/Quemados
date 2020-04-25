using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager instance;
    public static CanvasManager Instance
    {
        get
        {
            return instance;
        }
    }

    [Header("Start Game")]
    public GameObject playButton;

    [Header("End Game")]
    public GameObject endGameContainer;
    public TextMeshProUGUI resultText;
    public GameObject continueButton;

    [Header("Rounds")]
    public GameObject finishRoundContainer;

    [Header("Timer")]
    public TextMeshProUGUI timerTxt;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
    }

    private void Start()
    {
        EventManager.Instance.AddListener<ChangeGameStateEvent>(this.ChangeGameState);
        EventManager.Instance.AddListener<OnNextRoundEvent>(this.NextRound);
        EventSystem.current.SetSelectedGameObject(this.playButton);
    }

    #region Events
    private void ChangeGameState(ChangeGameStateEvent e)
    {
        if (e.currentGameState == GameState.ended) {
            this.endGameContainer.SetActive(true);
            this.resultText.text = "Game Finished";
            EventSystem.current.SetSelectedGameObject(continueButton);
        }
    }
    public void NextRound(OnNextRoundEvent e)
    {
        this.finishRoundContainer.SetActive(true);
        StartCoroutine(this.TurnOffRoundContainer());
    }
    #endregion
    public void ChangeTimer(string currentTime)
    {
        this.timerTxt.text = currentTime;
    }

    #region Coroutines
    public IEnumerator TurnOffRoundContainer()
    {
        yield return new WaitForSeconds(0.5f);
        this.finishRoundContainer.SetActive(false);
    }
    #endregion

    //Calling ByButton
    public void PlayPressed()
    {
        GameManager.Instance.StartGame();
        this.playButton.SetActive(false);
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<ChangeGameStateEvent>(this.ChangeGameState);
            EventManager.Instance.RemoveListener<OnNextRoundEvent>(this.NextRound);
        }
    }

}
