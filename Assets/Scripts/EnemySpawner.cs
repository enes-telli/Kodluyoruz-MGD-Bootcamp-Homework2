using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float minSpawnRate = 3f;
    [SerializeField] float maxSpawnRate = 6f;

    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject player;
    [SerializeField] Transform parent;

    private Coroutine coroutine;

    private void OnEnable()
    {
        coroutine = StartCoroutine(SpawnTheEnemies());
    }
    
    IEnumerator SpawnTheEnemies()
    {
        while (true)
        {
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            Vector3 enemyPosition = spawnPoint + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
            GameObject enemy = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity, parent);
            enemy.GetComponent<Enemy>().MoveTowardsThePlayer(player);
            yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
        }
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
}
