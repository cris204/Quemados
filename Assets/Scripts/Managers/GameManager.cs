using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [Header("Rounds")]
    private int enemiesCount;

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
        EventManager.Instance.AddListener<KilledEnemyEvent>(this.OnKilledEnemyEvent);
    }

    private void Init()
    {
        if (!this.isInit) {
            this.isInit = true;
            this.playerController = FindObjectOfType<PlayerController>();
            this.enemiesCount = Env.START_ENEMIES_COUNT;

            //TEMP
            EventManager.Instance.Trigger(new SpawnEnemiesEvent
            {
                enemiesCount = this.enemiesCount,
            });
        }
    }

    public void StartGame()
    {
        EventManager.Instance.Trigger(new StartGameEvent());
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

    #region Events
    private void OnKilledEnemyEvent(KilledEnemyEvent e)
    {
        this.enemiesCount--;
        if (this.enemiesCount <= 0) {
            EventManager.Instance.Trigger(new GameFinishEvent
            {
                isWinner = true
            });
        }
    }

    #endregion

    private void CreateEnemies() //Probably we need to move this when implement the waves
    {

    }


    public void FinisGameContinue()
    {
        OwnSceneLoadManager.Instance.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<KilledEnemyEvent>(this.OnKilledEnemyEvent);
        }
    }

}
