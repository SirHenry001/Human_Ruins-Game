using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FightCollision : MonoBehaviour
{

    public MonsterAi enemyScript;
    public VillainAi villainAi;
    public PlayerMovement playerMovement;
    public FirstBossScript firstBossScript;
    public SecondBossScript secondBossScript;
    public ThirdBossScript thirdBossScript;
    public FinalBossScript finalBossScript;

    // HOW MUCK DAMAGE DEALT AND SANITY GAIN TO/FROIM ENEMIES
    public int dealDamageMonster;
    public int gainSanityMonster;

    public int dealDamageVillain;
    public int gainSanityVillain;

    public int dealDamageBig;
    public int gainSanityBig;

    public int dealDamageBoss1;
    public int dealDamageBoss2;
    public int dealDamageBoss3;
    public int dealDamageBoss4;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        //firstBossScript = GameObject.Find("Level1Boss").GetComponent<FirstBossScript>();
        //thirdBossScript = GameObject.Find("Level33Boss").GetComponent<FirstBossScript>();
        //secondBossScript = GameObject.Find("Level2Boss").GetComponent<SecondBossScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // MONSTER AI RELATED FUNCTIONS
        if(collision.gameObject.tag == "Enemy")
        {
            //CONNECT TO ENEMY AI SCRIPT
            enemyScript = collision.gameObject.GetComponent<MonsterAi>();
            playerMovement.enemyScript = collision.gameObject.GetComponent<MonsterAi>();

            //IF HITTED TO MONSTER ENEMY, SANITY GAIN 100 POINTS
            playerMovement.SanityGain(gainSanityMonster);

            enemyScript.getHittedCount += 1;    
            enemyScript.GetHitted();
            //enemyScript.getHitted = true;
            enemyScript.MonsterHealth(dealDamageMonster);

        }

        // VILLAIN AI RELATED FUNCTION
        if (collision.gameObject.tag == "Villain")
        {
            //CONNECT TO VILLAIN AI SCRIPT
            villainAi = collision.gameObject.GetComponent<VillainAi>();
            playerMovement.villainAi = collision.gameObject.GetComponent<VillainAi>();

            //IF HITTED TO MONSTER ENEMY, SANITY GAIN 100 POINTS
            playerMovement.SanityGain(gainSanityVillain);

            //ACCESS TO VILLAIN GETHIT FUNCTIONS
            //villainAi.hittedTimer += Time.deltaTime;
            villainAi.getHittedCount += 1;
            villainAi.Gethit();

            //ACCESS TO VILLAIN HEALTH LOSS FUNCTION
            villainAi.VillainHealth(dealDamageVillain);
        }

        // BOSS LEVEL 1 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss")
        {
            firstBossScript = collision.gameObject.GetComponent<FirstBossScript>();
            firstBossScript.BossOneHealth(dealDamageBoss1);
        }

        // BOSS LEVEL 2 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss2")
        {
            secondBossScript = collision.gameObject.GetComponent<SecondBossScript>();
            secondBossScript.BossTwoHealth(dealDamageBoss2);
        }

        // BOSS LEVEL 3 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss3")
        {
            thirdBossScript = collision.gameObject.GetComponent<ThirdBossScript>();
            thirdBossScript.BossThreeHealth(dealDamageBoss3);
        }

        // BOSS LEVEL 4 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss4")
        {
            finalBossScript = collision.gameObject.GetComponent<FinalBossScript>();
            finalBossScript.BossFinalHealth(dealDamageBoss4);
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
