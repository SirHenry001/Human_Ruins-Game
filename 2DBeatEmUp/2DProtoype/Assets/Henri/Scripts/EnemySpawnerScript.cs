using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawnerScript : MonoBehaviour
{
    // WAWE SECTION  VARIABLES
    // VARIABLES FOR QUANTITY OF ENEMIES IN GAME
    public int enemiesInGame;
    public int maxEnemiesInGame;
    public int enemiesKilled;
    //TRIGGERS AND UI
    public GameObject waweCounterText;
    public GameObject waweNumberText;
    public GameObject completeText;
    public GameObject[] enemiesInScene;
    public GameObject[] enemiesInScene2;
    public GameObject[] enemiesInScene3;
    public GameObject waweAreaCollider;
    public GameObject followCamTrigger;
    public GameObject smokeEffect;
    // TIMER VARIABLE WAWE
    public float spawnTimer = 5f;
    // LIST OF ENEMIES WHICH ARE SPAWNED WAWE 
    public GameObject[] enemies;
    // LIST OF SPAWNPOINT WHERE ENEMIES ARE SPAWNED WAWE
    public GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        waweCounterText.SetActive(true);
        waweNumberText.SetActive(true);

        enemiesInGame = 0;
        enemiesKilled = 0;
        waweNumberText.GetComponent<TextMeshProUGUI>().text = enemiesKilled.ToString() + " of 5";

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
        Instantiate(enemies[Random.Range(0,2)], spawnPoints[Random.Range(0, 3)].transform.position, spawnPoints[Random.Range(0, 3)].transform.rotation);
        enemiesInGame++;
    }

    public void EnemyCounter()
    {
        enemiesInGame--;
    }

    public void WaweKillCounter()
    {
        enemiesKilled += 1;
        waweNumberText.GetComponent<TextMeshProUGUI>().text = enemiesKilled.ToString() + " of 5";

        if(enemiesKilled >= 5)
        {
            Destroy(waweAreaCollider);
            Destroy(spawnPoints[0]);
            Destroy(spawnPoints[1]);
            Destroy(spawnPoints[2]);
            Destroy(spawnPoints[3]);
            waweNumberText.SetActive(false);
            completeText.SetActive(true);
            followCamTrigger.SetActive(true);
            
            enemiesInScene = GameObject.FindGameObjectsWithTag("Villain"); //muuttujalle arvo, etsii viholliset tagin avulla
            enemiesInScene2 = GameObject.FindGameObjectsWithTag("Enemy"); //muuttujalle arvo, etsii viholliset tagin avulla
            enemiesInScene3 = GameObject.FindGameObjectsWithTag("BigEnemy"); //muuttujalle arvo, etsii viholliset tagin avulla

            for (int i = 0; i < enemiesInScene2.Length; i++)
            {
                Destroy(enemiesInScene2[i]);
                //Instantiate(smokeEffect, transform.position, transform.rotation);

            }

            for (int i = 0; i < enemiesInScene3.Length; i++)
            {
                Destroy(enemiesInScene3[i]);
                //Instantiate(smokeEffect, transform.position, transform.rotation);

            }

            for (int i = 0; i < enemiesInScene.Length; i++)
            {
                Destroy(enemiesInScene[i]);
                //Instantiate(smokeEffect, transform.position, transform.rotation);
                
            }

        }
    }

}
