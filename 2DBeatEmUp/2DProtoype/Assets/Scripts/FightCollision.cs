using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FightCollision : MonoBehaviour
{

    public EnemyAI enemyScript;
    public PlayerMovement playerMovement;
    public FirstBossScript firstBossScript;

    //public bool punchCollision;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        firstBossScript = GameObject.Find("Level1Boss").GetComponent<FirstBossScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {

            enemyScript = collision.gameObject.GetComponent<EnemyAI>();
            playerMovement.enemyScript = collision.gameObject.GetComponent<EnemyAI>();

            enemyScript.getHittedCount += 1;    
            enemyScript.GetHitted();       
            enemyScript.getHitted = true;
            enemyScript.isActive = false;
            enemyScript.EnemyHealth();

            //enemyScript.isActive = true;
            //StartCoroutine(enemyScript.GetHitted());

        }

        if (collision.gameObject.tag == "Boss")
        {
            //StartCoroutine(firstBossScript.BossOneHealth(10));
            firstBossScript.BossOneHealth(10);
        }

        /*
        if(playerMovement.gameObject.tag == "ComboTrigger")
        {
            playerMovement.Combo();
        }
        */


    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
