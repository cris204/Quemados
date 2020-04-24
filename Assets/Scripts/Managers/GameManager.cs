using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    pending,
    start,
    preparing,
    playing,
    ended
}
public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerController playerController;

    private bool isInit;
    private bool isPaused = false;

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
        this.Init();
        EventManager.Instance.AddListener<OnNextRoundEvent>(this.NextRound);
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.playerController = FindObjectOfType<PlayerController>();
        }
    }

    public void StartGame()
    {
        EventManager.Instance.Trigger(new ChangeGameStateEvent
        {
            currentGameState=GameState.playing,
        });
    }

    public Transform GetPlayerTransform()
    {
        return this.playerController.gameObject.transform;
    }

    public void TogglePause()
    {
        Time.timeScale = this.isPaused ? 1 : 0;
        this.isPaused = !this.isPaused;
        EventManager.Instance.Trigger(new TogglePauseEvent
        {
            isPaused = this.isPaused
        });
    }

    public void FinisGameContinue()
    {
        OwnSceneLoadManager.Instance.LoadScene("MainMenu");
    }

    #region Events

    public void NextRound(OnNextRoundEvent e)
    {
        EventManager.Instance.Trigger(new ChangeGameStateEvent
        {
            currentGameState = GameState.preparing,
        });

        StartCoroutine(this.WaitToNextRound());
    }

    #endregion


    private IEnumerator WaitToNextRound()
    {
        yield return new WaitForSeconds(3);
        EventManager.Instance.Trigger(new ChangeGameStateEvent
        {
            currentGameState = GameState.playing,
        });
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<OnNextRoundEvent>(this.NextRound);
        }
    }

}
