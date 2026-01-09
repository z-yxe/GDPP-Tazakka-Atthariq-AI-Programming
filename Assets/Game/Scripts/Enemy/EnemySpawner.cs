using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private PlayerController player;
    [SerializeField] private float enemySpawnCooldwon = 15f;

    private float lastTimeSpawn = 0;

    private void Update()
    {
        lastTimeSpawn += Time.deltaTime;

        if (lastTimeSpawn >= enemySpawnCooldwon)
        {
            int randomSpawner = Random.Range(0, spawnPoints.Count);
            EnemyController enemy = Instantiate(enemyPrefab, spawnPoints[randomSpawner].position, Quaternion.identity).GetComponent<EnemyController>();
            enemy.Setup(waypoints, player);

            lastTimeSpawn = 0;
        }
    }
}
