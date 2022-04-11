using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{
    // ENEMY TARGET TO ALL FUNCTIONS
    public Transform player;

    // ENEMY HEALTH
    public int enemyHealth = 10;

    // ENEMY COUNTER WHEN GOT HIT
    public int hitCount = 0;
    public int getHittedCount = 0;

    // VARIABLES FOR DISTANCES
    public float aggroRange;
    public float faceToFaceRange;

    // VARIABLE FOR AUDIO EFFECTS
    public GameObject hitAudio;
    public GameObject hitAudio2;
    public GameObject deadAudio;
    public GameObject audioSpawnEnemy;

    //VARIABLES FOR MOVEMENT
    public float moveSpeed;
    public float fleeSpeed;
    public float minX, maxX;
    public float minY, maxY;

    // VARIABLES FOR TIMERS
    public float attackTimer;
    public float cooldownTimer;

    // BOOLEANS
    public bool isActive;
    public bool facingLeft;
    public bool isFleeing;

    //COMPONENTS
    public Rigidbody2D monsterRigidbody;
    public Animator myAnimator;

    public EnemySpawnerScript enemySpawner;
    public AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        // GET COMPONENTS WHICH ARE NEEDED
        monsterRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        enemySpawner = GameObject.Find("EnemySpawnerObject").GetComponent<EnemySpawnerScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        EnemyBoundaries();

        // DISTANCE TO PLAYER
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        // IDLE STATE
        if (distToPlayer > aggroRange)
        {
            //Idle();
        }

        //START CHASING PLAYER
        if (distToPlayer < aggroRange && distToPlayer > faceToFaceRange && isFleeing == false && isActive == true) // IN THESE TERMS THIS WONT RESET ATTACKTIMER WHEN FACETOFACE DISTANCE
        {
            ChasePlayer();
        }

        //STOP IN FRONT OF THE PLAYER
        if (distToPlayer < faceToFaceRange && isFleeing == false) // ONLY THIS DISTANCE ATTACKTIMER RUNS FROM 0 TO 2 AND REPEAT
        {
            StopChasing();
        }

        //FLEE FROM PLAYER
        if (distToPlayer < faceToFaceRange && hitCount == 2) // IF ENEMY IS CLOSE AND HITCOUNT REACHES 2, FLEEING STARTS
        {
            StartCoroutine(Flee());
        }

        if (getHittedCount >= 5)
        {
            StartCoroutine(Knocked());
            getHittedCount = 0;
        }

        if (distToPlayer < faceToFaceRange && isFleeing == false)
        {
            monsterRigidbody.velocity = Vector2.zero;
        }

    }

    public void ChasePlayer()
    {
        
        myAnimator.SetBool("MonsterWalk", true); // SET WALK ANIMATION ACTIVE
        attackTimer = 0; // RESETS THE ATTACK TIMER

        // ENEMY IS ON THE LEFT SIDE, GO RIGHT
        if (transform.position.x < player.position.x)
        {
            monsterRigidbody.velocity = new Vector2(moveSpeed, monsterRigidbody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingLeft = false;
            CompareY();
        }
        // ENEMY IS ON THE RIGHT SIDE, GO LEFT
        else
        {
            monsterRigidbody.velocity = new Vector2(-moveSpeed, monsterRigidbody.velocity.y);
            transform.localScale = new Vector2(1, 1);
            facingLeft = true;
            CompareY();
        }
    }
    void CompareY()
    {
        if (transform.position.y < player.position.y)
        {
            monsterRigidbody.velocity = new Vector2(monsterRigidbody.velocity.x, moveSpeed);
        }

        else if (transform.position.y >= player.position.y)
        {
            monsterRigidbody.velocity = new Vector2(monsterRigidbody.velocity.x, -moveSpeed);
        }
    }

    public void StopChasing()
    {

        // STARTS THE ATTACKTIMER AND STOPS THE PLAYER TO IDLEANIMATION MODE IN FRONT OF THE PLAYER
        attackTimer += Time.deltaTime;
        myAnimator.SetBool("MonsterWalk", false);
        myAnimator.SetBool("MonsterHit", false);
        monsterRigidbody.velocity = Vector2.zero;

        if(attackTimer > 0.4f) // WHEN ATTACKTIMER REACHES ONE SECOND, ENEMY GOES TO ATTTACK MODE
        {
            Attack();
        }
    }

    public void Attack()
    {
        //ATTACKTIMER RESET AND ENEMY ANIMATION SET ACTIVE AN ENEMY HIT PLAYER
        attackTimer = 0;
        myAnimator.SetBool("MonsterHit", true);
    }

    public IEnumerator Flee()
    {
        // HITCOUNT RESETS WHEN FLEE STARTS
        hitCount = 0;

        //IF ENEMY IS FACING RIGHT WHEN START FLEEING, ENEMY FLEES LEFT
        if( facingLeft == false)
        {
            monsterRigidbody.velocity = new Vector2(-fleeSpeed, 0);
            isFleeing = true;
            myAnimator.SetTrigger("Flee");
            yield return new WaitForSeconds(1f);
            myAnimator.SetTrigger("Reset");  
            yield return new WaitForSeconds(0.2f);
            monsterRigidbody.velocity = Vector2.zero;
            isFleeing = false;
        }

        //IF ENEMY IS FACING lEFT WHEN START FLEEING, ENEMY FLEES RIGHT
        if (facingLeft == true)
        {
            monsterRigidbody.velocity = new Vector2(fleeSpeed, 0);
            isFleeing = true;
            myAnimator.SetTrigger("Flee");
            yield return new WaitForSeconds(1f);
            myAnimator.SetTrigger("Reset");
            yield return new WaitForSeconds(0.2f);
            monsterRigidbody.velocity = Vector2.zero;
            isFleeing = false;
        }

    }

    public void GetHitted()
    {

        myAnimator.SetTrigger("GetHitted");
        attackTimer = 0;
        hitCount = 0;
        isFleeing = false;
        isActive = true;

    }

    public IEnumerator Knocked()
    {
        myAnimator.SetTrigger("Knocked");
        GetComponent<CapsuleCollider2D>().enabled = false;
        monsterRigidbody.velocity = Vector2.zero;
        isFleeing = false;
        isActive = false;
        yield return new WaitForSeconds(2f);
        GetComponent<CapsuleCollider2D>().enabled = true;
        isActive = true;
    }

    public void MonsterHealth(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            myAnimator.SetTrigger("Dead");
            Instantiate(deadAudio, audioSpawnEnemy.transform.position, audioSpawnEnemy.transform.rotation);
            monsterRigidbody.velocity = Vector2.zero;
            moveSpeed = 0;
            GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject, 2f);


            if (enemySpawner.enabled == true)
            {
                enemySpawner.EnemyCounter();
                enemySpawner.WaweKillCounter();
            }

        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
    }

    void EnemyBoundaries()
    {
        //SET Y & X AXIS BOUNDARIES FOR MOVEMENT OF THE PLAYER
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }
}
