using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inital Wave Spawner Script from https://www.youtube.com/watch?v=Vrld13ypX_I

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    private int currentWave;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    public bool isCompleted, isGameOver;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    public Text roundText, roundCompleteText;

    EggControl eggControl;

    void Start()
    {
        isCompleted = false;
        isGameOver = false;

        

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Points Referenced!");
        }

        waveCountdown = timeBetweenWaves;

        roundCompleteText.text = "";
    }

    void Update()
    {
        if (!isCompleted && !isGameOver)
        {
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }

                roundCompleteText.text = "";
            }
            else
            {
                waveCountdown -= Time.deltaTime;
                currentWave = nextWave + 1;

                if (currentWave == 1)
                {
                    roundText.text = "Round " + currentWave + " incoming - " + waveCountdown.ToString("F1");
                }
                else
                {
                    roundCompleteText.text = "Round " + currentWave + " incoming - " + waveCountdown.ToString("F1");
                }
            }
        }
        else
        {
            roundText.text = "";
            roundCompleteText.text = "";
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        roundText.text = _wave.name;

        Debug.Log("Spawning Wave " + _wave.name);

        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);

        //_enemy.transform.parent = GameObject.Find("Total Enemies Spawned").transform;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            { 
                return false;
            }
        }
        return true;
    }

    void WaveCompleted()
    { 
        Debug.Log("Round Completed");

        roundText.text = "Round Completed";

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            LevelCompleted();

            //nextWave = 0;
            Debug.Log("Completed All Waves! Looping!");
        }
        else
        {
            nextWave++;
        }
    }

    void LevelCompleted()
    {
        isCompleted = true; 

        GameManager.instance.GameWin();
    }

    public void LevelIncompleted()
    {
        isGameOver = true;
    }
}
