using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Spawner : MonoBehaviour
{

    // VARIABLES FOR QUANTITY OF ENEMIES IN GAME
    public int enemiesInGame;
    public int maxEnemiesInGame;
    //TRIGGERS AND UI
    public GameObject[] enemiesInScene;
    // TIMER VARIABLE WAWE
    public float spawnTimer = 5f;
    // LIST OF ENEMIES WHICH ARE SPAWNED WAWE 
    public GameObject[] enemies;
    // LIST OF SPAWNPOINT WHERE ENEMIES ARE SPAWNED WAWE
    public GameObject[] spawnPoints;

    public SecondBossScript secondBossScript;

    // Start is called before the first frame update
    void Start()
    {
        secondBossScript = GameObject.Find("Level2Boss").GetComponent<SecondBossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0 && enemiesInGame < maxEnemiesInGame)
        {
            spawnTimer = 2;
            EnemySpawn();
        }

        if (spawnTimer <= 0)
        {
            spawnTimer = 2;
        }
    }

    void EnemySpawn()
    {
        Instantiate(enemies[0], spawnPoints[Random.Range(0, 3)].transform.position, spawnPoints[Random.Range(0, 3)].transform.rotation);
        enemiesInGame++;
    }

    public void EnemyCounter()
    {
        enemiesInGame--;
    }

    public void WaweKillCounter()
    {

        if (secondBossScript.bossHealth <= 0)
        {

            Destroy(spawnPoints[0]);
            Destroy(spawnPoints[1]);
            Destroy(spawnPoints[2]);
            Destroy(spawnPoints[3]);

            enemiesInScene = GameObject.FindGameObjectsWithTag("Villain"); //muuttujalle arvo, etsii viholliset tagin avulla

            for (int i = 0; i < enemiesInScene.Length; i++)
            {
                Destroy(enemiesInScene[i]);
                //Instantiate(smokeEffect, transform.position, transform.rotation);
            }

        }
    }
}
