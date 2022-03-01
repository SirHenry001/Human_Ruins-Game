using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLE FOR HEALTH
    public int playerHealth = 100;
    public int playerSanity = 1000;



    //VARIABLE FOR TIMERS
    public float stunnedTimer;

    // VARIABLE FOR HITCOMBOS
    public int maxCombo = 5;
    public int combo = 0;
    public float cooldown = 0.5f;
    public float maxTime = 0.8f;
    public float lastTime;

    // VARIABLES FOR EFFECTS & SPAWNPOINTS
    public GameObject hitEffect1;
    public GameObject hitEffect2;
    public GameObject hitEffect3;

    public GameObject hit1Spawn;
    public GameObject hit2Spawn;
    public GameObject hit3Spawn;

    // VARIABLES FOR MOVEMENT
    public float speed = 5f;
    public float x;
    public float y;

    // PLAYER BOUNDARIES
    public float minY = -0.3f, maxY = 3f;
    public float minX = -25f, maxX = 55f;

    // BOOLEAN ON/OFF FUNCTIONS
    public bool facingRight = true;
    public bool getStunned = false;
    public bool canPunch = false;
    public bool canPunch2 = false;
    public bool canPunch3 = false;
    public bool canKick = false;
    public bool canKick2 = false;

    // VARIABLES FOR COMPONENTS
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public Image healthImage;
    public Image sanityImage;

    // VARIABLES FOR OTHER SCRIPTS
    public VillainAi villainAi;
    public MonsterAi enemyScript;
    public BigEnemyAi bigMonsterAi;
    public GameManager gameManager;
    public GameMenuScreen gameMenuScreen;

    // Start is called before the first frame update
    void Start()
    {

        

        // CALL PHYSICS COMPONENT FROM UNITY TO CODE
        myRigidbody = GetComponent<Rigidbody2D>();
        // CALL ANIMATOR COMPONENT FROM UNITY TO CODE
        myAnimator = GetComponentInChildren<Animator>();
        // CALL ENEMY AI SCRIPT
        enemyScript = GameObject.Find("Monster").GetComponent<MonsterAi>();
        villainAi = GameObject.Find("EvilTeddy").GetComponent<VillainAi>();
        bigMonsterAi = GameObject.Find("BigEnemy").GetComponent<BigEnemyAi>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        // GET PLAYER TO PUNCH WITH ANIMATION 
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Combo());
        }

    }

    private void FixedUpdate()
    {

        SanityLoss(4);

        // GOES FUNCTION AND SET BOUNDARIES FOR Y AXIS FOR MOVEMENT OF THE PLAYER
        PlayerBoundaries();

        // PLAYER MOVEMENT BASED ON RIGIDBODY PHYSICS IN UNITY

        if (getStunned == false)
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");
            myRigidbody.velocity = new Vector2(x, y).normalized * speed;

            

            myAnimator.SetFloat("Horizontal", x);
            myAnimator.SetFloat("Vertical", y);
        }

        if(getStunned == true)
        {
            myRigidbody.velocity = Vector2.zero;

            myAnimator.SetFloat("Horizontal", 0);
            myAnimator.SetFloat("Vertical", 0);
        }

        // IF PLAYER IS MOVING RIGHT CHARACTER ALSO FLIPS FACING RIGHT IN UNITY
        if (x > 0.01f && facingRight == false)
        {
            Flip();
            facingRight = !facingRight;
        }
        // IF PLAYER IS MOVING LEFT CHARACTER ALSO FLIPS FACING LEFT IN UNITY
        if (x < -0.01f && facingRight == true)
        {
            Flip();
            facingRight = !facingRight;
        }



        
    }

    void Flip()
    {
        // FACING THE PLAYER TO WALKING DIRECTION REGARDING TO PLAYERS HORIZONTAL MOVEMENT 
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
    }

    
    void PlayerBoundaries()
    {
        //SET Y & X AXIS BOUNDARIES FOR MOVEMENT OF THE PLAYER
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,minX,maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }

    // PLAYER HIT ACTIONS IF BUTTON IS PRESSED AND ENEMY IS HITTED
    public IEnumerator Combo()
    {
        //myAnimator.SetTrigger("Hit1");
        myAnimator.SetBool("Punch", true);
        canPunch = true;
        lastTime = Time.time;
        combo++;

        while (true)
        {
            while ((Time.time - lastTime) <  maxTime && combo < maxCombo)
            {

                if(combo == 2)
                {
                    canPunch2 = true;
                    //myAnimator.SetTrigger("Hit2");
                    //myAnimator.SetBool("Punch", false);
                    myAnimator.SetBool("Punch2", true);
                    myAnimator.SetBool("Punch", false);
                }

                if(combo == 3)
                {
                    canPunch3 = true;
                    myAnimator.SetBool("Punch3", true);
                
                    myAnimator.SetBool("Punch", false);
                }

                if(combo == 4)
                {
                    canKick = true;
                    myAnimator.SetBool("Kick", true);
                    
                    myAnimator.SetBool("Punch", false);

                }

                if (combo ==5)
                {

                    canKick2 = true;
                    myAnimator.SetBool("Kick2", true);
                    
                    myAnimator.SetBool("Punch", false);
                }

                yield return null;
            }

            //myAnimator.SetTrigger("Reset");
            myAnimator.SetBool("Punch", false);
            myAnimator.SetBool("Punch2", false);
            myAnimator.SetBool("Punch3", false);
            myAnimator.SetBool("Kick", false);
            myAnimator.SetBool("Kick2", false);
            canPunch = false;
            canPunch2 = false;
            canPunch3 = false;
            canKick = false;
            canKick2 = false;
            villainAi.getHittedCount = 0; // nollaa vain jos scenessä on teityn niminen vihu. korjaa myöhemmin toimivammaksi "EvilTeddy"
            enemyScript.getHittedCount = 0; // sama homma ku ylempänä "monster"

            /*
            if (!enemyScript == null)
            {
                villainAi.getHittedCount = 0;
                enemyScript.getHittedCount = 0;

            }
            */

            combo = 0;
            
            yield return new WaitForSeconds(cooldown - (Time.time - lastTime));
            
        }
    }

    public void GetHit()
    {
        
        getStunned = true;
        myAnimator.SetTrigger("GetHit");
        StartCoroutine(Activation());
        
    }

    public IEnumerator Activation()
    {
        yield return new WaitForSeconds(0.75f);
        getStunned = false;
    }

    public void PlayerHealth(int damage)
    {
        playerHealth -= damage;
        healthImage.fillAmount = playerHealth * 0.01f;

        if (playerHealth <= 0)
        {
            Time.timeScale = 0.2f;
            myAnimator.SetTrigger("Dead");
            gameMenuScreen.GetComponent<GameMenuScreen>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(gameManager.PLayerDeath());

        }
    }
    
    public void SanityLoss(int loss)
    {

        playerSanity -= loss;
        sanityImage.fillAmount = playerSanity * 0.0001f;

        if(playerSanity <= 0)
        {
            playerSanity = 0;
        }

        if (playerSanity >= 10000)
        {
            playerSanity = 10000;
        }

        //tähän että sanity kuluu koko ajan
        //osumat viholliseen nostattaa sitä ja kuolemat vähän enemmän
    }

    public void SanityGain(int gain)
    {
        playerSanity += gain;
        sanityImage.fillAmount = playerSanity * 0.0001f;
    }
}
