using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyAi : MonoBehaviour
{

    // ENEMY TARGET TO ALL FUNCTIONS
    public Transform player;

    // ENEMY HEALTH
    public int enemyHealth = 100;

    // ENEMY COUNTER WHEN GOT HIT
    public int getHittedCount = 0;

    // VARIABLES FOR DISTANCES
    public float aggroRange;
    public float fleeRange;
    public float longAttackRange;
    public float shortAttackRange;

    //VARIABLES FOR SPEED
    public float moveSpeed;
    public float ySnapSpeed;

    // VARIABLES FOR TIMERS
    public float attackTimer;
    public float shortAttackTimer;

    //COMPONENTS
    public Rigidbody2D bigRigidbody;
    public Animator myAnimator;

    public float minX, maxX;
    public float minY, maxY;

    //BOOLEANS
    public bool facingLeft;
    public bool isAttacking;

    public EnemySpawnerScript enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        bigRigidbody = GetComponentInChildren<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
        enemySpawner = GameObject.Find("EnemySpawnerObject").GetComponent<EnemySpawnerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {

        EnemyBoundaries();

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        //IDLE
        if(distToPlayer > aggroRange)
        {
            Idle();
        }

        //APPROACH
        if(distToPlayer < aggroRange && isAttacking == false)
        {
            Approach();
        }

        //LONGATTACK
        if(distToPlayer < longAttackRange && distToPlayer > shortAttackRange && isAttacking == false)
        {
            Stop();
        }

        //SHORTATTACK
        if(distToPlayer < shortAttackRange)
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
        myAnimator.SetBool("Walk", true);
        myAnimator.SetBool("Attack", false);

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
        myAnimator.SetBool("Walk", false);
        myAnimator.SetBool("Attack", false);
        bigRigidbody.velocity = Vector2.zero;

        if(attackTimer > 0.5)
        {
            StartCoroutine(LongAttack());
        }
    }

    public IEnumerator LongAttack()
    {
        attackTimer = 0;
        isAttacking = true;
        bigRigidbody.velocity = Vector2.zero;
        myAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(2f);
        myAnimator.SetBool("Attack", false);
        yield return new WaitForSeconds(2f);
        isAttacking = false;   
    }

    public void ShortAttack()
    {
        shortAttackTimer += Time.deltaTime;

        if (shortAttackTimer > 1)
        {
            shortAttackTimer = 0;
            myAnimator.SetTrigger("AttackShort");
        }

    }

    public void GetHit()
    {
        shortAttackTimer = 0;
        bigRigidbody.velocity = Vector2.zero;
        myAnimator.SetTrigger("TakeHit");
    }

    void EnemyBoundaries()
    {
        //SET Y & X AXIS BOUNDARIES FOR MOVEMENT OF THE PLAYER
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY));
    }

    public void BigMonsterHealth(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            myAnimator.SetTrigger("Dead");
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<BigEnemyAi>().enabled = false;
            Destroy(gameObject, 2f);

            if(enemySpawner.enabled == true)
            {
                enemySpawner.EnemyCounter();
                enemySpawner.WaweKillCounter();
            }

        }
    }

}
