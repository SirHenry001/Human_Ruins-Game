using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstBossScript : MonoBehaviour
{
    // BOSS HEALTH
    public int bossHealth = 100;

    // VARIABLES TO OTHER SCRIPTS
    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;
    public GameManager gameManager;



    // ENEMY TARGET TO ALL FUNCTIONS
    public Transform player;

    // ENEMY COUNTER WHEN GOT HIT
    public int getHittedCount = 0;

    // VARIABLES FOR DISTANCES
    public float aggroRange;
    public float fleeRange;
    public float longAttackRange;
    public float shortAttackRange;
    public Vector3 movement;

    //VARIABLES FOR SPEED
    public float moveSpeed;
    public float ySnapSpeed;

    // VARIABLES FOR TIMERS
    public float attackTimer;
    public float shortAttackTimer;

    //COMPONENTS
    public Rigidbody2D bigRigidbody;
    public Animator bossOneAnimator;

    //BOOLEANS
    public bool facingLeft;
    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        bigRigidbody = GetComponentInChildren<Rigidbody2D>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossOneAnimator = GetComponentInChildren<Animator>();

        movement = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        //IDLE
        if (distToPlayer > aggroRange)
        {
            Idle();
        }

        //APPROACH
        if (distToPlayer < aggroRange && isAttacking == false)
        {
            Approach();
        }

        //LONGATTACK
        if (distToPlayer < longAttackRange && distToPlayer > shortAttackRange && isAttacking == false)
        {
            Stop();
        }

        //SHORTATTACK
        if (distToPlayer < shortAttackRange)
        {
            ShortAttack();
        }
    }

    public void Idle()
    {
        bigRigidbody.velocity = Vector2.zero;
    }

    public void Approach()
    {
        bossOneAnimator.SetBool("Walk", true);
        bossOneAnimator.SetBool("Attack", false);

        // ENEMY IS ON THE LEFT SIDE, GO RIGHT
        if (transform.position.x < player.position.x)
        {
            bigRigidbody.velocity = new Vector2(moveSpeed, bigRigidbody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            facingLeft = false;
            CompareY();
        }
        // ENEMY IS ON THE RIGHT SIDE, GO LEFT
        else
        {
            bigRigidbody.velocity = new Vector2(-moveSpeed, bigRigidbody.velocity.y);
            transform.localScale = new Vector2(1, 1);
            facingLeft = true;
            CompareY();
        }
    }
    void CompareY()
    {
        if (transform.position.y < player.position.y)
        {
            bigRigidbody.velocity = new Vector2(bigRigidbody.velocity.x, moveSpeed);
        }

        else if (transform.position.y >= player.position.y)
        {
            bigRigidbody.velocity = new Vector2(bigRigidbody.velocity.x, -moveSpeed);
        }
    }

    public void Stop()
    {
        attackTimer += Time.deltaTime;
        shortAttackTimer = 0;

        //movement.y = Mathf.Lerp(movement.y, player.position.y, 10f * Time.deltaTime);
        //transform.position = movement;

        bossOneAnimator.SetBool("Walk", false);
        bossOneAnimator.SetBool("Attack", false);
        bigRigidbody.velocity = Vector2.zero;

        if (attackTimer > 0.5)
        {

            StartCoroutine(LongAttack());
        }
    }

    public IEnumerator LongAttack()
    {
        attackTimer = 0;
        isAttacking = true;
        bigRigidbody.velocity = Vector2.zero;
        
        
        bossOneAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(2f);
        bossOneAnimator.SetBool("Attack", false);
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }

    public void ShortAttack()
    {
        bigRigidbody.velocity = Vector2.zero;
        shortAttackTimer += Time.deltaTime;

        if (shortAttackTimer > 1)
        {
            shortAttackTimer = 0;
            bossOneAnimator.SetTrigger("AttackShort");
        }

    }

    public void GetHit()
    {
        shortAttackTimer = 0;
        bigRigidbody.velocity = Vector2.zero;
        bossOneAnimator.SetTrigger("TakeHit");
    }

    public void BossOneHealth(int damage)
    {
        bossHealth -= damage;
        gameManager.bossHealthImage.fillAmount = bossHealth * 0.01f;

        if (bossHealth <= 0)
        {
            Destroy(gameManager.bossNameText);
            Time.timeScale = 0.2f;
            bossOneAnimator.SetTrigger("Dead");
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            GetComponent<FirstBossScript>().enabled = false;
            StartCoroutine(gameMenuScreen.ScoreScreen());
        }
    }

}
