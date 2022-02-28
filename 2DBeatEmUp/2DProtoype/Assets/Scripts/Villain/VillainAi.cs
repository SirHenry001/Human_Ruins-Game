using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainAi : MonoBehaviour
{

    public Transform player;

    public int enemyHealth = 10;
    public int getHittedCount = 5;

    public float aggroRange;
    public float faceToFaceRange;

    public float moveSpeed;
    public float stanceSpeed;
    public float timer;
    public float attackTimer;
    public float hittedTimer;
    public float knockTimer;

    public bool isActive = true; // EN SAA VIEL TAKAISIN PÄÄLLE KUN POISTAN SEN KÄYTÖSTÄ HIT JA KNOCK KOHDASSA. FUNTSI
    //tee vielä min/ max y ja x rajaukset liikkeelle

    public Rigidbody2D villainRigidbody;
    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        villainRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
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

        if (distToPlayer < faceToFaceRange)
        {
            //ATTACK TO PLAYER
            Attack();
        }

        if (distToPlayer > faceToFaceRange && isActive)
        {
            //ENEMY STOPS ATTACKING WHEN GET FURTHER FROM FRONT OF PLAYER
            myAnimator.SetBool("Attack", false);
        }

    }

    public void Activation()
    {
        print("aktivoidu");
        isActive = true;
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
    void Attack()
    {

        myAnimator.SetBool("Attack", true);
        villainRigidbody.velocity = Vector2.zero;
        attackTimer += Time.deltaTime;

        if(attackTimer > 1 && attackTimer < 2)
        {
            myAnimator.SetBool("Attack", false);
            attackTimer = 0;
        }

    }

    public void Gethit()
    {
        
        myAnimator.SetTrigger("GetHitted");

        /*
        if(5 < getHittedCount)
        {
            Knocked();
        }
        */
    }

    public void Knocked()
    {
        /*
        myAnimator.SetTrigger("Knocked");
        GetComponent<BoxCollider2D>().enabled = false;
        */


    }

    public void VillainHealth(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            myAnimator.SetTrigger("Dead");
            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 1.5f);
        }
    }
}
