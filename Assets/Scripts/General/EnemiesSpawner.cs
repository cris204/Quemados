using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{

    public Vector3 center;
    public Vector3 size;
    public List<GameObject> spawnPoints;
    private GameObject enemyPrefab;
    private List<EnemyController> enemiesSpawned = new List<EnemyController>();

    private void Awake()
    {
        EventManager.Instance.AddListener<SpawnEnemiesEvent>(this.SpawnEnemies); //Sometimes it is not suscribing in time, could be another solution than put it in Awake
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddListener<StartGameEvent>(this.StartGame);
    }

    public void SpawnEnemies(SpawnEnemiesEvent e)
    {
        if(this.enemyPrefab == null) {
            this.enemyPrefab = ResourcesManager.Instance.GetEnemy("1");
        }
        this.SpawnEnemiesInSpawnPoint(e.enemiesCount);
    }

    private void SpawnEnemiesInSpawnPoint(int enemiesCount)
    {
        int randomIndex = 0;
        for (int i = 0; i < enemiesCount; i++) {
            randomIndex = Random.Range(0, this.spawnPoints.Count);
            this.SpawnEnemy(this.spawnPoints[randomIndex].transform.position);
        }
    }

    private void SpawnEnemiesInArea(int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++) {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
            this.SpawnEnemy(pos);
        }
    }

    private void SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject enemySpawned = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyController newEnemy = enemySpawned.GetComponent<EnemyController>();
        newEnemy.DisactiveEnemy();
        enemiesSpawned.Add(newEnemy);
    }

    private void StartGame(StartGameEvent e)
    {
        for (int i = 0; i < this.enemiesSpawned.Count; i++) {
            this.enemiesSpawned[i].ActiveEnemy();
        }
    }

    private void OnDestroy()
    {
        if (EventManager.HasInstance()) {
            EventManager.Instance.RemoveListener<SpawnEnemiesEvent>(this.SpawnEnemies);
            EventManager.Instance.RemoveListener<StartGameEvent>(this.StartGame);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }

}
