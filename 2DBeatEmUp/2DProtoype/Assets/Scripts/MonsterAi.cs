using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAi : MonoBehaviour
{

    public Transform player;

    public int enemyHealth = 10;
    public int getHittedCount = 5;

    public float aggroRange;
    public float faceToFaceRange;

    public float moveSpeed;
    public float fleeSpeed;
    public float timer;
    public float knockTimer;

    public bool isActive = true; // EN SAA VIEL TAKAISIN PÄÄLLE KUN POISTAN SEN KÄYTÖSTÄ HIT JA KNOCK KOHDASSA. FUNTSI
    //tee vielä min/ max y ja x rajaukset liikkeelle
    public bool alertOn;
    public bool isFleeing;
    public bool facingLeft;
    public bool attackingLeft;

    public Rigidbody2D monsterRigidbody;
    public Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        monsterRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    private void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        /*
        if (distToPlayer > aggroRange && isActive)
        {
            Idle();
        }
        */

        if (distToPlayer < aggroRange && isActive)
        {
            //START CHASING PLAYER
            ChasePlayer();
        }

        if (distToPlayer < faceToFaceRange && isActive)
        {
            //ATTACK TO PLAYER
            Attack();
        }

        if (isActive == false)
        {
            //ATTACK TO PLAYER
            StartCoroutine(Flee());
        }

        if (distToPlayer > faceToFaceRange && isActive)
        {
            //ENEMY STOPS ATTACKING WHEN GET FURTHER FROM FRONT OF PLAYER
            myAnimator.SetBool("Attack", false);
        }

        float x = player.position.x - transform.position.x;

        // IF PLAYER IS MOVING RIGHT CHARACTER ALSO FLIPS FACING RIGHT IN UNITY
        if (x < 0.01f && facingLeft == false)
        {
            Flip();
            facingLeft = !facingLeft;
        }
        // IF PLAYER IS MOVING LEFT CHARACTER ALSO FLIPS FACING LEFT IN UNITY
        if (x > -0.01f && facingLeft == true)
        {
            Flip();
            facingLeft = !facingLeft;
        }

        if (isFleeing == true)
        {
            if (attackingLeft == true)
            {
                monsterRigidbody.MovePosition((Vector2)transform.position - (Vector2.right * -fleeSpeed * Time.deltaTime));
            }

            else if (attackingLeft == false)
            {
                monsterRigidbody.MovePosition((Vector2)transform.position - (Vector2.right * fleeSpeed * Time.deltaTime));
            }
        }

    }

    public void ChasePlayer()
    {
        myAnimator.SetBool("MonsterWalk", true);

        // ENEMY IS ON THE LEFT SIDE, GO RIGHT
        if (transform.position.x < player.position.x)
        {
            monsterRigidbody.velocity = new Vector2(moveSpeed, monsterRigidbody.velocity.y);
            transform.localScale = new Vector2(-1, 1);

            if (facingLeft)
            {
                attackingLeft = true;
            }

            else if (!facingLeft)
            {
                attackingLeft = false;
            }

            CompareY();

        }

        // ENEMY IS ON THE RIGHT SIDE, GO LEFT
        else
        {
            monsterRigidbody.velocity = new Vector2(-moveSpeed, monsterRigidbody.velocity.y);
            transform.localScale = new Vector2(1, 1);

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

    public void Attack()
    {
        alertOn = false;
        monsterRigidbody.velocity = Vector2.zero;
        myAnimator.SetBool("MonsterWalk", false);
        myAnimator.SetTrigger("Attack");

    }

    public IEnumerator Flee()
    {
        isFleeing = true;
        alertOn = false;
        myAnimator.SetTrigger("Flee");
        myAnimator.SetBool("MonsterHit", false);
        myAnimator.SetBool("MonsterWalk", false);

        yield return new WaitForSeconds(2f);

        myAnimator.SetBool("MonsterFlee", false);
        alertOn = false;
        isFleeing = false;
        isActive = true;
    }

    public void GetHitted()
    {
        myAnimator.SetTrigger("GetHitted");

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
