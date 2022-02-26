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
    public int getHittedCount = 5;

    // VARIABLES FOR DISTANCES
    public float aggroRange;
    public float faceToFaceRange;

    //VARIABLES FOR SPEED
    public float moveSpeed;
    public float fleeSpeed;

    // VARIABLES FOR TIMERS
    public float timer;

    // BOOLEANS
    public bool alertOn;
    public bool facingLeft;

    //COMPONENTS
    public Rigidbody2D monsterRigidbody;
    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // GET COMPONENTS WHICH ARE NEEDED
        monsterRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void FixedUpdate()
    {
        // DISTANCE TO PLAYER
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        // IDLE STATE
        if (distToPlayer > aggroRange)
        {
            //Idle();
        }

        //START CHASING PLAYER
        if (distToPlayer < aggroRange)
        {
            ChasePlayer();
        }

        //ATTACK TO PLAYER
        if (distToPlayer < faceToFaceRange)
        {
            Attack();
        }

        //FLEE FROM PLAYER
        if (distToPlayer < faceToFaceRange)
        {
            Flee();
        }

    }

    public void ChasePlayer()
    {

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

    }

    public void Attack()
    {
        
    }

    public void Flee()
    {

    }

    public void GetHitted()
    {

    }

    public void Knocked()
    {

    }

    public void MonsterHealth(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            myAnimator.SetTrigger("Dead");
            GetComponent<CapsuleCollider2D>().enabled = false;
            Destroy(gameObject, 2f);
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
    }
}
