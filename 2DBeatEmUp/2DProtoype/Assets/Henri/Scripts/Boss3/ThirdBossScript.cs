using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdBossScript : MonoBehaviour
{


    // OLD
    public int bossHealth = 100;

    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;
    public GameManager gameManager;

    public Animator bossTwoAnimator;
    //OLD

    // BOSS TARGET
    public Transform player;

    public int enemyHealth = 10;
    //public int getHittedCount = 0;
    public int getHitCount = 0;
    public int hitCount = 0;

    public float aggroRange;
    public float attackRange;
    public float faceToFaceRange;

    public float moveSpeed;
    public float fleeSpeed;
    public float stanceSpeed;
    public float timer;
    public float attackTimer;
    public float hittedTimer;
    public float attackTimer2;

    public float minX, maxX;
    public float minY, maxY;

    public bool isActive = true;
    public bool isFleeing = true;
    public bool shortAttacking = true;
    public bool isDead = false;

    public Rigidbody2D villainRigidbody;
    public Animator myAnimator;

    public EnemySpawnerScript enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        villainRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        enemySpawner = GameObject.Find("EnemySpawnerObject").GetComponent<EnemySpawnerScript>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        attackTimer2 += Time.deltaTime;

        EnemyBoundaries();

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < aggroRange && isActive)
        {
            ChasePlayer();
        }

        if (attackTimer2 >= 3 && isActive == true)
        {
            StartCoroutine(Attack());
            
        }

        if (10 <= getHitCount && isDead == false)
        {
            StartCoroutine(Flee());
            getHitCount = 0;
        }
    }

    void ChasePlayer()
    {
        myAnimator.SetBool("Approach", true);

        // ENEMY IS ON THE LEFT SIDE, GO RIGHT
        if (transform.position.x < player.position.x)
        {
            villainRigidbody.velocity = new Vector2(moveSpeed, villainRigidbody.velocity.y);

            CompareY();

        }

        // ENEMY IS ON THE RIGHT SIDE, GO LEFT
        else
        {
            villainRigidbody.velocity = new Vector2(-moveSpeed, villainRigidbody.velocity.y);

            CompareY();
        }
    }

    void CompareY()
    {
        if (transform.position.y < player.position.y)
        {
            villainRigidbody.velocity = new Vector2(villainRigidbody.velocity.x, moveSpeed);
        }

        else if (transform.position.y >= player.position.y)
        {
            villainRigidbody.velocity = new Vector2(villainRigidbody.velocity.x, -moveSpeed);
        }
    }

    public void StopChasing()
    {

        // STARTS THE ATTACKTIMER AND STOPS THE PLAYER TO IDLEANIMATION MODE IN FRONT OF THE PLAYER
        attackTimer += Time.deltaTime;
        myAnimator.SetBool("Approach", false);
        myAnimator.SetBool("Attack", false);
        

        if (attackTimer > 0.4f) // WHEN ATTACKTIMER REACHES ONE SECOND, ENEMY GOES TO ATTTACK MODE
        {
            Attack();
        }
    }

    public IEnumerator Attack()
    {
        myAnimator.SetBool("Attack", true);
        villainRigidbody.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider2D>().enabled = true;
        myAnimator.SetBool("Attack", false);
        attackTimer2 = 0;

    }

    public IEnumerator Flee()
    {
        
        isActive = false;
        myAnimator.SetTrigger("Flee");
        GetComponent<BoxCollider2D>().enabled = false;
        villainRigidbody.velocity = new Vector2(fleeSpeed, 0);

        yield return new WaitForSeconds(1f);
        myAnimator.SetBool("LongAttack", true);

        yield return new WaitForSeconds(2f);

        myAnimator.SetBool("LongAttack", false);
        GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.5f);
        isActive = true;
    }

    public void Gethit()
    {
        myAnimator.SetTrigger("GetHitted");
    }

    void EnemyBoundaries()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }
    public void BossThreeHealth(int damage)
    {
        bossHealth -= damage;
        gameManager.bossHealthImage.fillAmount = bossHealth * 0.01f;

        if (bossHealth <= 0)
        {
            isDead = true;
            Time.timeScale = 0.2f;
            myAnimator.SetTrigger("Dead");
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(gameMenuScreen.ScoreScreen());
        }
    }

}
