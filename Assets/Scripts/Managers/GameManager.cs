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
    private GameState curretGameState;

    [Header("Time")]
    private float currentTime;
    private string timeString;

    public Transform GetPlayerTransform()
    {
        return this.playerController.gameObject.transform;
    }
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
        EventManager.Instance.AddListener<ChangeGameStateEvent>(this.ChangeGameState);
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

    private void Update()
    {
        if (this.curretGameState == GameState.playing) {
            this.currentTime += Time.deltaTime;
        }else if(this.curretGameState == GameState.preparing) {
            this.currentTime -= Time.deltaTime;
            if (this.currentTime < 0) {
                EventManager.Instance.Trigger(new ChangeGameStateEvent
                {
                    currentGameState = GameState.playing,
                });
            }
        }
        CanvasManager.Instance.ChangeTimer(string.Format("{0}{1}",this.timeString, this.currentTime.ToString("F0")));
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
        this.currentTime = Env.TIME_BETWEEN_ROUNDS;
    }

    private void ChangeGameState(ChangeGameStateEvent e)
    {
        this.curretGameState = e.currentGameState;
        if (e.currentGameState == GameState.playing) {
            this.timeString = "Time: ";
        } else if (e.currentGameState == GameState.preparing) {
            this.timeString = "Preparing Time: ";
        }
    }

    #endregion

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<OnNextRoundEvent>(this.NextRound);
            EventManager.Instance.RemoveListener<ChangeGameStateEvent>(this.ChangeGameState);
        }
    }

}
