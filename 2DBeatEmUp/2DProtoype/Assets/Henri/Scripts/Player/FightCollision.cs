using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FightCollision : MonoBehaviour
{

    public MonsterAi enemyScript;
    public VillainAi villainAi;
    public BigEnemyAi bigMonsterAi;
    public PlayerMovement playerMovement;
    public FirstBossScript firstBossScript;
    public SecondBossScript secondBossScript;
    public ThirdBossScript thirdBossScript;
    public FinalBossScript finalBossScript;
    public PostProcessScript postProcess;
    public AudioManager audioManager;
    public GameManager gameManager;
    public LevelHighScore levelScore;
    public FirstHighScore levelScore1;
    public ThirdHighScore levelScore3;



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
        postProcess = GameObject.Find("PostProcess").GetComponent<PostProcessScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        levelScore = GameObject.Find("LevelHighScore").GetComponent<LevelHighScore>();
        levelScore1 = GameObject.Find("LevelHighScore").GetComponent<FirstHighScore>();
        levelScore3 = GameObject.Find("LevelHighScore").GetComponent<ThirdHighScore>();
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

            //CREATE HIT EFFECT
            Instantiate(playerMovement.hitEffect1, playerMovement.hitSpawn.transform.position, playerMovement.hitSpawn.transform.rotation);

            //IF HITTED TO MONSTER ENEMY, SANITY GAIN 100 POINTS
            playerMovement.SanityGain(gainSanityMonster);
            enemyScript.getHittedCount += 1;


            if (enemyScript.facingLeft == true && enemyScript.getHittedCount < 5)
            {
                enemyScript.isActive = false;
                enemyScript.monsterRigidbody.AddForce(Vector3.right * 2000f);          
                enemyScript.GetHitted();

            }

            if (enemyScript.facingLeft == false && enemyScript.getHittedCount < 5)
            {
                enemyScript.isActive = false;
                enemyScript.monsterRigidbody.AddForce(Vector3.left * 2000f);
                enemyScript.GetHitted();

            }

            enemyScript.MonsterHealth(dealDamageMonster);

            //ACCES TO AUDIO SPAWN OBJECT TO PLAYER MOVEMENT

            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);

            // CONNECT GAMEMANAGER SCORE
            levelScore.AddScore(10);
            levelScore1.AddScore(10);
            levelScore3.AddScore(10);

        }

        // VILLAIN AI RELATED FUNCTION
        if (collision.gameObject.tag == "Villain")
        {
            //CONNECT TO VILLAIN AI SCRIPT
            villainAi = collision.gameObject.GetComponent<VillainAi>();
            playerMovement.villainAi = collision.gameObject.GetComponent<VillainAi>();

            //CREATE HIT EFFECT
            Instantiate(playerMovement.hitEffect1, playerMovement.hitSpawn.transform.position, playerMovement.hitSpawn.transform.rotation);

            //IF HITTED TO MONSTER ENEMY, SANITY GAIN 100 POINTS
            playerMovement.SanityGain(gainSanityVillain);

            //ACCESS TO VILLAIN GETHIT FUNCTIONS
            villainAi.getHittedCount += 1;

            // CONNECT GAMEMANAGER SCORE
            levelScore.AddScore(25);
            levelScore1.AddScore(25);
            levelScore3.AddScore(25);

            //ACCESS TO VILLAIN HEALTH LOSS FUNCTION
            villainAi.VillainHealth(dealDamageVillain);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);

            if (villainAi.facingRight == true && villainAi.getHittedCount < 5)
            {
                villainAi.isActive = false;
                villainAi.villainRigidbody.AddForce(Vector3.left * 2000f);
                villainAi.Gethit();
            }

            if (villainAi.facingRight == false && villainAi.getHittedCount < 5)
            {
                villainAi.isActive = false;
                villainAi.villainRigidbody.AddForce(Vector3.right * 2000f);
                villainAi.Gethit();
            }

        }

        // BIGENEMY AI RELATED FUNCTION
        if (collision.gameObject.tag == "BigEnemy")
        {
            //CONNECT TO BIGENEMY AI SCRIPT
            bigMonsterAi = collision.gameObject.GetComponent<BigEnemyAi>();
            playerMovement.bigMonsterAi = collision.gameObject.GetComponent<BigEnemyAi>();

            //CREATE HIT EFFECT
            Instantiate(playerMovement.hitEffect1, playerMovement.hitSpawn.transform.position, playerMovement.hitSpawn.transform.rotation);

            //IF HITTED TO MONSTER ENEMY, SANITY GAIN 100 POINTS
            playerMovement.SanityGain(gainSanityBig);

            //ACCESS TO VILLAIN GETHIT FUNCTIONS

            if (bigMonsterAi.facingLeft == true)
            {
                bigMonsterAi.bigRigidbody.AddForce(Vector3.left * 1500f);
                bigMonsterAi.GetHit();

            }

            if (enemyScript.facingLeft == false)
            {
                bigMonsterAi.bigRigidbody.AddForce(Vector3.left * 1500f);
                bigMonsterAi.GetHit();

            }

            bigMonsterAi.GetHit();

            //ACCESS TO VILLAIN HEALTH LOSS FUNCTION
            bigMonsterAi.BigMonsterHealth(1);

            // CONNECT GAMEMANAGER SCORE
            levelScore.AddScore(50);
            levelScore1.AddScore(50);
            levelScore3.AddScore(50);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);
            Destroy(playerMovement.audioEffect[0],2f);
            Destroy(playerMovement.audioEffect[1],2f);
            Destroy(playerMovement.audioEffect[2],2f);
        }

        // BOSS LEVEL 1 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss")
        {
            firstBossScript = collision.gameObject.GetComponent<FirstBossScript>();
            firstBossScript.BossOneHealth(dealDamageBoss1);

            //CREATE HIT EFFECT
            Instantiate(playerMovement.hitEffect1, playerMovement.hitSpawn.transform.position, playerMovement.hitSpawn.transform.rotation);

            // CONNECT GAMEMANAGER SCORE
            levelScore1.AddScore(75);

            //ACCESS TO BOSS 1 COUNTERS
            firstBossScript.getHitCount += 1;

            // ACCES TO BOSS1 GETHIT FUNCTION
            firstBossScript.Gethit();

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);
            Destroy(playerMovement.audioEffect[0], 2f);
            Destroy(playerMovement.audioEffect[1], 2f);
            Destroy(playerMovement.audioEffect[2], 2f);
        }

        // BOSS LEVEL 2 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss2")
        {
            secondBossScript = collision.gameObject.GetComponent<SecondBossScript>();
            secondBossScript.BossTwoHealth(dealDamageBoss2);

            //CREATE HIT EFFECT
            Instantiate(playerMovement.hitEffect1, playerMovement.hitSpawn.transform.position, playerMovement.hitSpawn.transform.rotation);

            // ACCES TO BOSS2 GETHIT FUNCTION
            secondBossScript.getHitCount += 1;
            secondBossScript.Gethit();

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);

            // CONNECT GAMEMANAGER SCORE
            levelScore.AddScore(75);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);
            Destroy(playerMovement.audioEffect[0], 2f);
            Destroy(playerMovement.audioEffect[1], 2f);
            Destroy(playerMovement.audioEffect[2], 2f);
        }

        // BOSS LEVEL 3 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss3")
        {
            thirdBossScript = collision.gameObject.GetComponent<ThirdBossScript>();
            thirdBossScript.BossThreeHealth(dealDamageBoss3);

            //CREATE HIT EFFECT
            Instantiate(playerMovement.hitEffect1, playerMovement.hitSpawn.transform.position, playerMovement.hitSpawn.transform.rotation);

            // ACCES TO BOSS2 GETHIT FUNCTION
            thirdBossScript.getHitCount += 1;
            thirdBossScript.Gethit();

            // CONNECT GAMEMANAGER SCORE
            levelScore3.AddScore(75);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);
            Destroy(playerMovement.audioEffect[0], 2f);
            Destroy(playerMovement.audioEffect[1], 2f);
            Destroy(playerMovement.audioEffect[2], 2f);
        }

        // BOSS LEVEL 4 RELATED FUNCTIONS
        if (collision.gameObject.tag == "Boss4")
        {
            finalBossScript = collision.gameObject.GetComponent<FinalBossScript>();
            finalBossScript.BossFinalHealth(dealDamageBoss4);

            //CREATE HIT EFFECT
            Instantiate(playerMovement.hitEffect1, playerMovement.hitSpawn.transform.position, playerMovement.hitSpawn.transform.rotation);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);

            //ACCESS TO AUDIOSPAWN IN PLAYERMOVEMENT SCRIPT
            Instantiate(playerMovement.audioEffect[Random.Range(0, 2)], playerMovement.audioSpawn.transform.position, playerMovement.audioSpawn.transform.rotation);
            Destroy(playerMovement.audioEffect[0], 2f);
            Destroy(playerMovement.audioEffect[1], 2f);
            Destroy(playerMovement.audioEffect[2], 2f);
        }

    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }

}
