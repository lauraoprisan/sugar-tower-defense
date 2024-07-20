using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    private float spawnInterval = 3f;
    private float timer;
    private bool shouldSpawn = true;

    void Start()
    {
        timer = spawnInterval;
        EventManager.Instance.OnTowerDestroyed += Instance_OnTowerDestroyed;
    }


    void Update()
    {
        if (!shouldSpawn) return;


        timer -= Time.deltaTime;
        if (timer < 0) {   
            SpawnEnemies();
            timer = spawnInterval;
        }
    }

    void SpawnEnemies() {
        //if tower is destroyed, return
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }


 
    private void Instance_OnTowerDestroyed(object sender, System.EventArgs e) {
        shouldSpawn = false;
    }

    private void OnDestroy() {
        EventManager.Instance.OnTowerDestroyed -= Instance_OnTowerDestroyed;
    }
}


