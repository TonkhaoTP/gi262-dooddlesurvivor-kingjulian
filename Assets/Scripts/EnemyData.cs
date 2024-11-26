using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable] 
public class EnemyWave
{
    public int[] EnemyCounts;

    public EnemyWave(int[] enemyCounts)
    {
        EnemyCounts = enemyCounts;
    }
}

public class EnemyData : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform enemyParent;

    private bool spawning = false;

    public void StartWave(EnemyWave wave)
    {
        StartCoroutine(SpawnEnemiesInWave(wave));
    }

    IEnumerator SpawnEnemiesInWave(EnemyWave wave)
    {
        spawning = true;

        for (int i = 0; i < wave.EnemyCounts.Length; i++)
        {
            for (int j = 0; j < wave.EnemyCounts[i]; j++)
            {
                SpawnEnemy(i);
                yield return new WaitForSeconds(0.5f);
            }
        }

        spawning = false;
    }

    void SpawnEnemy(int enemyType)
    {
        if (enemyType >= 0 && enemyType < enemyPrefabs.Length && spawnPoints.Length > 0)
        {
            Transform chosenSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefabs[enemyType], chosenSpawnPoint.position, Quaternion.identity);

            if (enemyParent != null)
            {
                enemy.transform.SetParent(enemyParent);
            }
        }
    }

    public bool AreEnemiesRemaining()
    {
        return enemyParent.childCount > 0 || spawning;
    }
}
