using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FightCollision : MonoBehaviour
{

    public EnemyAI enemyScript;
    public VillainAi villainAi;
    public PlayerMovement playerMovement;
    public FirstBossScript firstBossScript;
    public SecondBossScript secondBossScript;
    public ThirdBossScript thirdBossScript;
    public FinalBossScript finalBossScript;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        //firstBossScript = GameObject.Find("Level1Boss").GetComponent<FirstBossScript>();
        //thirdBossScript = GameObject.Find("Level33Boss").GetComponent<FirstBossScript>();
        //secondBossScript = GameObject.Find("Level2Boss").GetComponent<SecondBossScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //CONNECT TO ENEMY AI SCRIPT
            enemyScript = collision.gameObject.GetComponent<EnemyAI>();
            playerMovement.enemyScript = collision.gameObject.GetComponent<EnemyAI>();

            enemyScript.getHittedCount += 1;    
            enemyScript.GetHitted();
            enemyScript.getHitted = true;
            enemyScript.EnemyHealth();



        }

        if (collision.gameObject.tag == "Villain")
        {
            //CONNECT TO VILLAIN AI SCRIPT
            villainAi = collision.gameObject.GetComponent<VillainAi>();
            playerMovement.villainAi = collision.gameObject.GetComponent<VillainAi>();

            villainAi.getHittedCount += 1;
            villainAi.VillainHealth(10);
        }

        if (collision.gameObject.tag == "Boss")
        {
            firstBossScript = collision.gameObject.GetComponent<FirstBossScript>();
            firstBossScript.BossOneHealth(10);
        }

        if (collision.gameObject.tag == "Boss2")
        {
            secondBossScript = collision.gameObject.GetComponent<SecondBossScript>();
            secondBossScript.BossTwoHealth(10);
        }

        if (collision.gameObject.tag == "Boss3")
        {
            thirdBossScript = collision.gameObject.GetComponent<ThirdBossScript>();
            thirdBossScript.BossThreeHealth(10);
        }

        if (collision.gameObject.tag == "Boss4")
        {
            finalBossScript = collision.gameObject.GetComponent<FinalBossScript>();
            finalBossScript.BossFinalHealth(10);
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
