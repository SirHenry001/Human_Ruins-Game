using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCollision : MonoBehaviour
{

    public EnemyAI enemyScript;
    public PlayerMovement playerMovement;

    public bool punchCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyScript.EnemyHealth();      
        }

        if(playerMovement.gameObject.tag == "ComboTrigger")
        {
            playerMovement.Combo();
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GameObject.Find("Monster").GetComponent<EnemyAI>();
        playerMovement = GameObject.Find("Character").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
