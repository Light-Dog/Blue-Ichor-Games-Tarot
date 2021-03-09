using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    //enum spwanstate {spawn, wait, count}
    public enum SpawnState { Spawning, Waiting, CountingDown }

    //timer for enemey check, timer between waves, timer for between waves, 
    public float timeBetweenWaves = 5f;
    private float enemyCheckCooldown = 1f;
    private float waveCooldown;

    //wave list, wave index, state of spawner
    public Wave[] roundList;
    private int roundIndex;
    private SpawnState currentState = SpawnState.CountingDown;

    public TMP_Text waveText;
    public TMP_Text livesText;
    public TMP_Text moneyText;

    public TMP_Text woodText;
    public TMP_Text stoneText;
    public TMP_Text ironText;

    //name, prefab, num to spawn, rate to spwan them, spawnLocations[]
    [System.Serializable]
    public class Wave
    {
        public string name;

        public GameObject enemy;
        public int numToSpawn;

        public float timeBetweenSpawn;

        public Transform[] spawnLocations;
    }

    void Start()
    {
        waveCooldown = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        waveText.text = roundIndex.ToString();
        livesText.text = PlayerStats.Lives.ToString();
        moneyText.text = PlayerStats.Money.ToString();
        woodText.text = PlayerStats.Wood.ToString();
        ironText.text = PlayerStats.Iron.ToString();
        stoneText.text = PlayerStats.Stone.ToString();

        //if waiting, GOES TO NEXT WAVE WHEN ALL ENEMIES ARE DEAD
        //if they are, Complete the wave, otherwise return
        if (currentState == SpawnState.Waiting)
        {
            if (EnemyCheck())
            {
                CompleteWave();
            }
            else
            {
                //print("Waiting");
                return;
            }
        }

        //if counting
        //if timer is complete && not spawning, spawn a wave
        if(currentState == SpawnState.CountingDown)
        {
            if (waveCooldown <= 0f)
            {
                if (SpawnState.Spawning != currentState)
                    StartCoroutine( SpawnWave(roundList[roundIndex]) );
            }
            else
                waveCooldown -= Time.deltaTime;
        }
        
    }

    //----------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------

    //complete wave
    //set state to counting, set time back, check if next wave exists, then call it
    void CompleteWave()
    {
        currentState = SpawnState.CountingDown;
        waveCooldown = timeBetweenWaves;

        if(roundIndex + 1 >= roundList.Length)
        {
            roundIndex = 0;
        }
        else
        {
            roundIndex++;
        }

    }

    //enemy check, find gameobject with tag "Enemy", only do search when timer ticks, return true or false
    bool EnemyCheck()
    {
        if(enemyCheckCooldown <= 0f)
        {
            enemyCheckCooldown = 1f;

            if (GameObject.FindWithTag("Enemy") == null)
                return true;
        }
        else
            enemyCheckCooldown -= Time.deltaTime;

        return false;
    }

    //spawn a wave as a Coroutine, taking a wave, set state to spawn at start and wait at end
    IEnumerator SpawnWave(Wave _wave)
    {
        currentState = SpawnState.Spawning;

        for(int i = 0; i < _wave.numToSpawn; i++)
        {
            SpawnEnemy(_wave);

            yield return new WaitForSeconds(_wave.timeBetweenSpawn);
        }

        if(currentState == SpawnState.Spawning)
            currentState = SpawnState.Waiting;
        yield break;
    }

    //function for spawn enemy, can do random.range form 0 to length of an array, for a random location in a given list of arrays
    void SpawnEnemy(Wave _wave)
    {
        Transform spawnLocation;

        if (_wave.spawnLocations.Length > 1)
            spawnLocation = _wave.spawnLocations[Random.Range(0, _wave.spawnLocations.Length-1)];
        else
            spawnLocation = _wave.spawnLocations[0];

        Instantiate(_wave.enemy, spawnLocation.position, spawnLocation.rotation);
    }

    //----------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------

    //Button to Trigger Next Wave Now
    public void TriggerNextWave()
    {
        enemyCheckCooldown = 1f;
        CompleteWave();
    }
}
