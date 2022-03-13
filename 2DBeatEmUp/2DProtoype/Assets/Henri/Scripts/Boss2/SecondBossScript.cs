using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossScript : MonoBehaviour
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

    public float aggroRange;
    public float faceToFaceRange;

    public float moveSpeed;
    public float spotSpeed;
    public float stanceSpeed;
    public float timer;
    public float attackTimer;
    public float hittedTimer;
    public float knockTimer;

    public float minX, maxX;
    public float minY, maxY;

    public bool isActive = true;
    public bool shortAttacking = true;

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

        EnemyBoundaries();

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer > aggroRange && isActive)
        {
            Idle();
        }

        if (distToPlayer < aggroRange && isActive)
        {
            //START CHASING PLAYER
            ChasePlayer();
        }

        if (distToPlayer < faceToFaceRange && shortAttacking == true)
        {
            //ATTACK TO PLAYER
            isActive = false;
            StartCoroutine(Attack());
        }

        if (10 <= getHitCount)
        {
            StartCoroutine(ChargeAttack());
            getHitCount = 0;
        }

        if (distToPlayer > faceToFaceRange && isActive)
        {
            //ENEMY STOPS ATTACKING WHEN GET FURTHER FROM FRONT OF PLAYER
            myAnimator.SetBool("Attack", false);
        }

    }

    public void Idle()
    {
        timer += Time.deltaTime;
        myAnimator.SetBool("Approach", false);
        villainRigidbody.velocity = new Vector2(stanceSpeed, 0);

        if (timer > 2)
        {
            stanceSpeed = -stanceSpeed;
            timer = 0;
        }

        // ENEMY IS ON THE LEFT SIDE, TURN RIGHT
        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(-1, 1);

        }

        // ENEMY IS ON THE RIGHT SIDE, TURN LEFT
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void ChasePlayer()
    {
        myAnimator.SetBool("Approach", true);

        // ENEMY IS ON THE LEFT SIDE, GO RIGHT
        if (transform.position.x < player.position.x)
        {
            villainRigidbody.velocity = new Vector2(moveSpeed, villainRigidbody.velocity.y);
            transform.localScale = new Vector2(-1, 1);

            CompareY();

        }

        // ENEMY IS ON THE RIGHT SIDE, GO LEFT
        else
        {
            villainRigidbody.velocity = new Vector2(-moveSpeed, villainRigidbody.velocity.y);
            transform.localScale = new Vector2(1, 1);

            CompareY();
        }

    }

    void CompareY()
    {

        // tietty pointti y missä ei liiku memo

        if (transform.position.y < player.position.y)
        {
            villainRigidbody.velocity = new Vector2(villainRigidbody.velocity.x, moveSpeed);
        }

        else if (transform.position.y >= player.position.y)
        {
            villainRigidbody.velocity = new Vector2(villainRigidbody.velocity.x, -moveSpeed);
        }
    }
    public IEnumerator Attack()
    {
        myAnimator.SetBool("Attack", true);
        villainRigidbody.velocity = Vector2.zero;
        moveSpeed = 0;

        yield return new WaitForSeconds(3f);

        isActive = true;
        moveSpeed = 2;

    }

    public IEnumerator ChargeAttack()
    {
        print("menee tänne");

        myAnimator.SetBool("Charge",true);
        villainRigidbody.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        isActive = false;
        shortAttacking = false;

        yield return new WaitForSeconds(3f);

        isActive = true;
        shortAttacking = true;
        myAnimator.SetBool("Charge", false);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Gethit()
    {
        myAnimator.SetTrigger("GetHitted");
    }

    void EnemyBoundaries()
    {
        //SET Y & X AXIS BOUNDARIES FOR MOVEMENT OF THE PLAYER
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }

    public void BossTwoHealth(int damage)
    {
        bossHealth -= damage;
        gameManager.bossHealthImage.fillAmount = bossHealth * 0.01f;

        if (bossHealth <= 0)
        {
            myAnimator.SetTrigger("Dead");
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(gameMenuScreen.ScoreScreen());
        }
    }


}
