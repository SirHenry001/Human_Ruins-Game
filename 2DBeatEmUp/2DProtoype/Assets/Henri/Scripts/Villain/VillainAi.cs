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

    public bool isActive = true;

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

        if (5 <= getHittedCount)
        {
            StartCoroutine(Knocked());
            getHittedCount = 0;
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
    void Attack()
    {
        attackTimer += Time.deltaTime;
        villainRigidbody.velocity = Vector2.zero;
        myAnimator.SetBool("Stance", true);

        if (attackTimer > 1 && attackTimer < 2)
        {
            myAnimator.SetBool("Attack", true);
            myAnimator.SetBool("Stance", false);
        }

        if(attackTimer > 2 && attackTimer < 3)
        {
            myAnimator.SetBool("Attack", false);
            attackTimer = 0;
        }

    }

    public void Gethit()
    {
        
        myAnimator.SetTrigger("GetHitted");

    }

    public IEnumerator Knocked()
    {

        myAnimator.SetTrigger("Knocked");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<VillainAi>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<VillainAi>().enabled = true;



    }

    public void VillainHealth(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            myAnimator.SetTrigger("Dead");
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<VillainAi>().enabled = false;
            Destroy(gameObject, 1.5f);

            if (enemySpawner.enabled == true)
            {
                enemySpawner.EnemyCounter();
                enemySpawner.WaweKillCounter();
            }
        }
    }
}
