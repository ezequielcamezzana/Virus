using UnityEngine;
using System;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

    public float timeBetweenWave = 15;
    public float countDownTime = 5;
    public float wavesToBoss = 10;
    public int curretWave = 1;
    public int currentStage = 1;
    public int rotationSpeed = 5;
    public bool isActive = true;

    private float nextWaveTime;

    public static event Action<int, int> OnSpawn;

    void Update()
    {
        while (isActive && nextWaveTime <= Time.time)
        {
            StartCoroutine(StartWave());

        }
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    IEnumerator StartWave()
    {
        Debug.Log("Starting Wave");
        curretWave++;
        nextWaveTime = Time.time + timeBetweenWave;
        float countDown = countDownTime;
        while (countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }
        OnSpawn?.Invoke(curretWave, currentStage);
        nextWaveTime = Time.time + timeBetweenWave;
        if (curretWave == wavesToBoss)
        {
            curretWave = 0;
            currentStage += 1;
        }
    }
}
