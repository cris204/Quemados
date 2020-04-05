using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{

    public Vector3 center;
    public Vector3 size;
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

        for (int i = 0; i < e.enemiesCount; i++) {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));
            GameObject enemySpawned = Instantiate(enemyPrefab, pos, Quaternion.identity);
            EnemyController newEnemy = enemySpawned.GetComponent<EnemyController>();
            newEnemy.DisactiveEnemy();
            enemiesSpawned.Add(newEnemy);
        }
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
