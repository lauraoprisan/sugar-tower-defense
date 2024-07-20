using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType {
    public Enemy enemyPrefab;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float initialDelay;

}

public class EnemySpawner : MonoBehaviour {

    public List<EnemyType> enemyTypes = new List<EnemyType>();
    public Dictionary<EnemyType, float> spawnTimers = new Dictionary<EnemyType, float>();
    public Dictionary<EnemyType, float> nextSpawnIntervals = new Dictionary<EnemyType, float>();
    public Dictionary<EnemyType, float> initialDelayTimers = new Dictionary<EnemyType, float>();
    private float gameTimer = 0f;
    private bool shouldSpawn = true;
    public float maxYLimit = -0.5f;
    public float minYLimit = -5.4f;



    void Start()
    {
        InitializeSpawnTimers();
        EventManager.Instance.OnTowerDestroyed += Instance_OnTowerDestroyed;
    }


    void Update()
    {
        if (!shouldSpawn) return;


        if (!shouldSpawn) return;

        gameTimer += Time.deltaTime;

        foreach (var enemyType in enemyTypes) {
           
            if (gameTimer >= enemyType.initialDelay) {
                spawnTimers[enemyType] += Time.deltaTime;

                if (spawnTimers[enemyType] > nextSpawnIntervals[enemyType]) {
                    SpawnEnemy(enemyType);
                    spawnTimers[enemyType] = 0;
                    nextSpawnIntervals[enemyType] = Random.Range(enemyType.minSpawnTime, enemyType.maxSpawnTime);
                }
            }
        }
    }



    private void InitializeSpawnTimers() {
        foreach (var enemyType in enemyTypes) {
            spawnTimers[enemyType] = 0;
            nextSpawnIntervals[enemyType] = Random.Range(enemyType.minSpawnTime, enemyType.maxSpawnTime);
            initialDelayTimers[enemyType] = enemyType.initialDelay;
        }
    }

    private void SpawnEnemy(EnemyType enemyType) {
        float randomY = Random.Range(minYLimit, maxYLimit);
        Instantiate(enemyType.enemyPrefab, new Vector3(transform.position.x, randomY, 0), enemyType.enemyPrefab.transform.rotation);
    }

    private void Instance_OnTowerDestroyed(object sender, System.EventArgs e) {
        shouldSpawn = false;
    }

    private void OnDestroy() {
        EventManager.Instance.OnTowerDestroyed -= Instance_OnTowerDestroyed;
    }
}


