using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawnerScript : MonoBehaviour
{
    // VARIABLES FOR QUANTITY OF ENEMIES IN GAME
    public int enemiesInGame;
    public int maxEnemiesInGame;
    public int enemiesKilled;

    public GameObject waweCounterText;
    public GameObject waweNumberText;
    public GameObject completeText;
    public GameObject[] enemiesInScene; // jono objekteja eli viholliset

    public GameObject waweAreaCollider;
    public GameObject followCamTrigger;

    // TIMER VARIABLE
    public float spawnTimer = 5f;

    // LIST OF ENEMIES WHICH ARE SPAWNED
    public GameObject[] enemies;

    // LIST OF SPAWNPOINT WHERE ENEMIES ARE SPAWNED
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        waweCounterText.SetActive(true);
        waweNumberText.SetActive(true);
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
        Instantiate(enemies[Random.Range(0,1)], spawnPoints[Random.Range(0, 3)].transform.position, spawnPoints[Random.Range(0, 3)].transform.rotation);
        //Instantiate(enemies[1], spawnPoints[Random.Range(0, 3)].transform.position, spawnPoints[Random.Range(0, 3)].transform.rotation);
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
            waweNumberText.SetActive(false);
            completeText.SetActive(true);
            followCamTrigger.SetActive(true);

            
            enemiesInScene = GameObject.FindGameObjectsWithTag("Villain"); //muuttujalle arvo, etsii viholliset tagin avulla

            for (int i = 0; i < enemiesInScene.Length; i++)
            {
                Destroy(enemiesInScene[i]);

            }

        }
    }

}
