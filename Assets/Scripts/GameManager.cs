using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private EnemyData enemySpawner;
    private Queue<EnemyWave> waveQueue;

    private int currentWaveIndex = 0;

    [SerializeField] private Transform gameOver;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        EnemyInWave();
        StartCoroutine(WaveSpawn());
        gameOver.gameObject.SetActive(false);
    }

    private void Update()
    {
        GameOver();
    }

    void EnemyInWave()
    {
        waveQueue = new Queue<EnemyWave>();
        
        waveQueue.Enqueue(new EnemyWave(new int[] { 20, 0, 0, 0, 0 }));
        waveQueue.Enqueue(new EnemyWave(new int[] { 15, 15, 0, 0, 0 }));
        waveQueue.Enqueue(new EnemyWave(new int[] { 15, 15, 8, 0, 0 }));
        waveQueue.Enqueue(new EnemyWave(new int[] { 15, 15, 4, 3, 0 }));
        waveQueue.Enqueue(new EnemyWave(new int[] { 15, 15, 4, 3, 1 }));
    }

    IEnumerator WaveSpawn()
    {
        while (waveQueue.Count > 0)
        {
            currentWaveIndex++;
            EnemyWave currentWave = waveQueue.Dequeue();
            
            enemySpawner.StartWave(currentWave);
            
            while (enemySpawner.AreEnemiesRemaining())
            {
                yield return null;
            }

            Debug.Log($"Wave {currentWaveIndex} Complete");
        }
    }

    void GameOver()
    {
        Player player = FindObjectOfType<Player>();
        if (player.CurHP <= 0)
        {
            gameOver.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
