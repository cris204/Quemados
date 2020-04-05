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
        EventManager.Instance.AddListener<GameFinishEvent>(this.OnGameFinish);
        EventSystem.current.SetSelectedGameObject(this.playButton);
    }

    #region Events
    private void OnGameFinish(GameFinishEvent e)
    {
        this.endGameContainer.SetActive(true);
        this.resultText.text = e.isWinner ? "Winner" : "Defeat";
        EventSystem.current.SetSelectedGameObject(continueButton);
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
            EventManager.Instance.RemoveListener<GameFinishEvent>(this.OnGameFinish);
        }
    }

}
