using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float timeBetweenSpawn = 1;
    public int spawnLevel = 1;
    public Enemy[] enemies;
    public List<Enemy> wave = new List<Enemy>();

    private void CreateWave(int currentWave, int stage)
    {
        wave.Clear();
        int waveSize = Mathf.Clamp(currentWave, 1, 10);
        List<Enemy> availableEnemies = GetAvailableEnemies(stage);
        for (int i = 0; i < waveSize; i++)
        {
            wave.Add(availableEnemies[Random.Range(0, availableEnemies.Count)]);
        }
        Spawn();
    }

    private List<Enemy> GetAvailableEnemies(int stage)
    {
        List<Enemy> availableEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            if (enemy.properties.stage <= stage)
            {
                availableEnemies.Add(enemy);
            }
        }
        return availableEnemies;
    }

    private void OnEnable()
    {
        SpawnManager.OnSpawn += CreateWave;
    }

    private void OnDisable()
    {
        SpawnManager.OnSpawn -= CreateWave;
    }

    void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        foreach (Enemy enemy in wave)
        {
            Instantiate(enemy, transform.position, transform.rotation);
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
    }
}
