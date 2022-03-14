using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{

    public int bossHealth = 100;

    public float chargeTimer;

    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;
    public GameManager gameManager;

    public Rigidbody2D villainRigidbody;
    public Animator myAnimator;

    public Transform player;

    public float aggroRange;
    public float faceToFaceRange;

    public int getHitCount;

    public float moveSpeed;
    public float fleeSpeed;
    public float spottingSpeed;
    public float chargeSpeed;
    public float minX, maxX;
    public float minY, maxY;

    public bool isActive;
    public bool facingRight;
    public bool shortAttack;
    public bool isCharging;
    public bool isFleeing;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        myAnimator = GetComponentInChildren<Animator>();
        villainRigidbody = GetComponent<Rigidbody2D>();

        playerMovement.SanityLoss(100000000);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        

        if(isDead == false)
        {

            chargeTimer += Time.deltaTime;

            if (chargeTimer >= 10)
            {
                isCharging = true;
            }

            else
            {
                isCharging = false;
            }
        }



        EnemyBoundaries();

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer > aggroRange && isActive)
        {
            Idle();
        }

        if (distToPlayer < aggroRange && isActive && isCharging == false)
        {
            //START CHASING PLAYER
            ChasePlayer();
        }

        if (distToPlayer < faceToFaceRange && shortAttack == true && isCharging == false)
        {
            isActive = false;
            StartCoroutine(Attack());
        }

        if (isCharging == true)
        {
            StartCoroutine(ChargeAttack());
        }

        if (distToPlayer < faceToFaceRange && isActive && isCharging == false)
        {
                villainRigidbody.velocity = Vector2.zero;
                myAnimator.SetBool("Attack", false);
        }


        if (shortAttack == false && isCharging == false)
        {
            Flee();
        }

    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
    }



    public void Idle()
    {
        myAnimator.SetBool("Approach", false);
    }

    public void ChasePlayer()
    {
        myAnimator.SetBool("Approach", true);

        // ENEMY IS ON THE LEFT SIDE, GO RIGHT
        if (transform.position.x < player.position.x)
        {
            villainRigidbody.velocity = new Vector2(moveSpeed, villainRigidbody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingRight = true;
            CompareY();

        }

        // ENEMY IS ON THE RIGHT SIDE, GO LEFT
        else
        {
            villainRigidbody.velocity = new Vector2(-moveSpeed, villainRigidbody.velocity.y);
            transform.localScale = new Vector2(1, 1);
            facingRight = false;
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

        isActive = false;
        myAnimator.SetBool("PreAttack", true);
        myAnimator.SetBool("Approach", false);
        villainRigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        GetComponent<BoxCollider2D>().enabled = false;
        isActive = false;
        villainRigidbody.velocity = Vector2.zero;
        myAnimator.SetBool("PreAttack", false);
        myAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        myAnimator.SetBool("Attack", false);
        shortAttack = false;
        yield return new WaitForSeconds(1f);
        shortAttack = true;
        isActive = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Flee()
    {
        myAnimator.SetBool("Attack", false);

        if(isFleeing == true)
        {
            if (facingRight == true)
            {
                villainRigidbody.velocity = new Vector2(-fleeSpeed, villainRigidbody.velocity.y);
            }

            if (facingRight == false)
            {
                villainRigidbody.velocity = new Vector2(fleeSpeed, villainRigidbody.velocity.y);
            }
        }

    }

    public IEnumerator ChargeAttack()
    {
        myAnimator.SetTrigger("Flee");
        isFleeing = false;

        if (facingRight == true)
        {
            villainRigidbody.velocity = new Vector2(-spottingSpeed, villainRigidbody.velocity.y);
        }

        if (facingRight == false)
        {
            villainRigidbody.velocity = new Vector2(spottingSpeed, villainRigidbody.velocity.y);
        }

        yield return new WaitForSeconds(3f);

        myAnimator.SetBool("Charge", true);
        chargeTimer = 0;

        if (facingRight == true)
        {
            villainRigidbody.velocity = new Vector2(chargeSpeed, villainRigidbody.velocity.y);
        }

        if (facingRight == false)
        {
            villainRigidbody.velocity = new Vector2(-chargeSpeed, villainRigidbody.velocity.y);
        }

        yield return new WaitForSeconds(1f);
        villainRigidbody.velocity = Vector2.zero;
        myAnimator.SetBool("Charge", false);
        myAnimator.SetBool("Charge2", true);

        yield return new WaitForSeconds(1f);
        villainRigidbody.velocity = Vector2.zero;
        myAnimator.SetBool("Charge2", false);
        chargeTimer = 0;
        isFleeing = true;
    }



    void EnemyBoundaries()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }



    public void BossFinalHealth(int damage)
    {
        print("läpi");
        bossHealth -= damage;
        gameManager.bossHealthImage.fillAmount = bossHealth * 0.01f;


        if (bossHealth <= 0)
        {
            isDead = true;
            Time.timeScale = 0.2f;
            myAnimator.SetTrigger("Dead");
            villainRigidbody.velocity = Vector2.zero;
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(gameMenuScreen.EndScreen());
        }
    }

}
