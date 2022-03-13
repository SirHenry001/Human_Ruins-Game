using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCollisionEnemy : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public MonsterAi monsterAi;

    public int dealDamage;
    public int scareSanity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            monsterAi.hitCount += 1;
            playerMovement.SanityLoss(scareSanity);
            playerMovement.GetHit();
            playerMovement.PlayerHealth(dealDamage);
            Instantiate(monsterAi.hitAudio, monsterAi.audioSpawnEnemy.transform.position, monsterAi.audioSpawnEnemy.transform.rotation);
            Instantiate(monsterAi.hitAudio2, monsterAi.audioSpawnEnemy.transform.position, monsterAi.audioSpawnEnemy.transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
