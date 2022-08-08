using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    public Text waveCountdownText;

    private int waveIndex = 0;

    void Update () 
    {
        //inital wave spawns after 'countdown' time value 
        //restarts using 'timeBetweenWaves' value. 
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    IEnumerator SpawnWave ()
    {
        waveIndex++;
        //Examples of wave logic
        //numOfEnemies = waves[waveNumber].count;     --advanced
        //numOfEnemies =  waveNumber * waveNumber + 1;
        //numOfEnemies = waveNumber;                  --simple
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.4f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
