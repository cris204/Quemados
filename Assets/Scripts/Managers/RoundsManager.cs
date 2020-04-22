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
    private bool isStartedGame;

    [Header("Enemies Spawn")]
    private int enemiesCount;
    private int enemiesSpawned;
    private int enemiesKilled;
    private int totalEnemiesRound;
    private int enemiesToSpawn;
    private int enemiesToSpawnCount = 1;
    private float enemySpawnDelay = 2;
    private float enemySpawnCurrentTime = 0;

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
        EventManager.Instance.AddListener<StartGameEvent>(this.StartGame);
        this.totalEnemiesRound = Env.INITIAL_ENEMIES_ROUND;
    }

    private void Update()
    {
        if (this.isStartedGame) {
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

    public void UpgradeRound()
    {
        this.enemiesToSpawnCount += 2;
    }

    public void WinCondition()
    {
        if (this.enemiesKilled >= this.totalEnemiesRound) {
            Debug.LogError("Finish");
        }
    }

    #region Events
    private void OnKilledEnemyEvent(KilledEnemyEvent e)
    {
        this.enemiesCount--;
        this.enemiesKilled++;
        this.WinCondition();
    }
    private void StartGame(StartGameEvent e)
    {
        this.isStartedGame = true;
    }
    #endregion
    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<KilledEnemyEvent>(this.OnKilledEnemyEvent);
            EventManager.Instance.RemoveListener<StartGameEvent>(this.StartGame);
        }
    }
}
