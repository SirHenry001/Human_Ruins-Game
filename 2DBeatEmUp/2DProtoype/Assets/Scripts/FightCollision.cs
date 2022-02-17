using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FightCollision : MonoBehaviour
{

    public EnemyAI enemyScript;
    public PlayerMovement playerMovement;

    //public bool punchCollision;

    void Start()
    {
        enemyScript = GameObject.Find("Monster").GetComponent<EnemyAI>();
        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyScript.getHittedCount += 1;
            StartCoroutine(enemyScript.GetHitted());
            enemyScript.getHitted = true;
            enemyScript.isActive = false;
            enemyScript.EnemyHealth();

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
