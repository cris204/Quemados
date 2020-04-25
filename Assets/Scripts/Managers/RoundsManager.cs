using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    private static RoundsManager instance;
    public static RoundsManager Instance
    {
        get
        {
            return instance;
        }
    }
    [Header("Game Status")]
    private GameState currentGameState;

    [Header("Enemies Spawn")]
    private int enemiesCount;
    private int enemiesSpawned;
    private int enemiesKilled;
    private int totalEnemiesRound;
    private int enemiesToSpawn;
    private int enemiesToSpawnCount = 1;
    private float enemySpawnDelay = 2;
    private float enemySpawnCurrentTime = 0;

    private int roundsCount = 0;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
    }

    public void PlusEnemies()
    {
        this.enemiesCount++;
        this.enemiesSpawned++;
    }

    private void Start()
    {
        EventManager.Instance.AddListener<KilledEnemyEvent>(this.OnKilledEnemyEvent);
        EventManager.Instance.AddListener<ChangeGameStateEvent>(this.ChangeGameState);
        this.SetNextRound();
    }

    private void Update()
    {
        if (this.currentGameState==GameState.playing || this.currentGameState == GameState.start) {
            this.SpawnEnemy();
        }

    }

    public void SpawnEnemy()
    {
        if (this.enemySpawnCurrentTime > this.enemySpawnDelay) {

              this.enemiesToSpawn = (this.enemiesCount + this.enemiesToSpawnCount) < Env.MAX_ENEMIES_ALIVE_COUNT ? this.enemiesToSpawnCount : (Env.MAX_ENEMIES_ALIVE_COUNT - this.enemiesToSpawnCount);

            if ((this.enemiesSpawned + this.enemiesToSpawn) > this.totalEnemiesRound) {
                this.enemiesToSpawn = this.totalEnemiesRound - this.enemiesSpawned;
            }

            if (this.enemiesCount < Env.MAX_ENEMIES_ALIVE_COUNT) { 
                EventManager.Instance.Trigger(new SpawnEnemiesEvent
                {
                    enemiesCount = this.enemiesToSpawn,
                });
            }

            this.enemySpawnCurrentTime = 0;
        }
        this.enemySpawnCurrentTime += Time.deltaTime;
    }

    public void WinCondition()
    {
        if (this.enemiesKilled >= this.totalEnemiesRound) {

            EventManager.Instance.Trigger(new OnNextRoundEvent());
            this.roundsCount++;
            this.SetNextRound();
        }
    }

    public void SetNextRound()
    {
        this.enemiesCount = 0;
        this.enemiesKilled = 0;
        this.enemiesSpawned = 0;
        this.totalEnemiesRound = Env.INITIAL_ENEMIES_ROUND + (this.roundsCount * Env.PLUS_ENEMIES_PER_ROUND);
        this.enemiesToSpawnCount++;
        this.enemySpawnCurrentTime = 0;

    }

    #region Events
    private void OnKilledEnemyEvent(KilledEnemyEvent e)
    {
        this.enemiesCount--;
        this.enemiesKilled++;
        this.WinCondition();
    }
    private void ChangeGameState(ChangeGameStateEvent e)
    {
        this.currentGameState = e.currentGameState;
        if (e.currentGameState == GameState.ended) {
            this.roundsCount = 0;
        }
    }

    #endregion
    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<KilledEnemyEvent>(this.OnKilledEnemyEvent);
            EventManager.Instance.RemoveListener<ChangeGameStateEvent>(this.ChangeGameState);
        }
    }
}
