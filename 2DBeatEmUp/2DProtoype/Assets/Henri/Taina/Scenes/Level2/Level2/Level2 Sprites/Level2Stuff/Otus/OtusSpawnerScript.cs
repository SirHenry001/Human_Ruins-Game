using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtusSpawnerScript : MonoBehaviour
{
    public GameObject enemy;

    public float minWait;
    public float maxWait;

    private bool isSpawning;


    // Start is called before the first frame update
    void Start()
    {
        isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnEnemy", timer);
            isSpawning = true;
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy,transform.position, transform.rotation);
        isSpawning = false;
    }
}
