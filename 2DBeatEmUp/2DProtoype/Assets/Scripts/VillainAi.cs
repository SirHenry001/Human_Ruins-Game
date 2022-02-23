using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainAi : MonoBehaviour
{

    public Transform player;

    public int enemyHealth = 100;
    public int getHittedCount = 5;

    public float aggroRange;
    public float faceToFaceRange;

    public float moveSpeed;
    public float stanceSpeed;
    public float timer;
    public float attackTimer;

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

        if (getHittedCount > 6)
        {
            Knocked();
        }


        // DISATANCE TO PLAYER
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        print("distToPlayer" + distToPlayer);

        if(distToPlayer > aggroRange)
        {
            myAnimator.SetBool("Approach", false);
            transform.Translate(Vector3.right * stanceSpeed * Time.deltaTime);
            timer += Time.deltaTime;

            if (timer > 2)
            {
                stanceSpeed = -stanceSpeed;
                timer = 0;
            }
        }

        if(distToPlayer < aggroRange)
        {
            //START CHASING PLAYER
            ChasePlayer();
        }

        else
        {
            //STOP CHASING PLAYER
            StopChasingPlayer();
        }

        if(distToPlayer < faceToFaceRange)
        {
            Attack();
        }

        if (distToPlayer > faceToFaceRange)
        {
            myAnimator.SetBool("Attack", false);
        }

    }

    void ChasePlayer()
    {
        myAnimator.SetBool("Approach", true);

        // ENEMY IS ON THE LEFT SIDE, GO RIGHT
        if(transform.position.x < player.position.x)
        {
            villainRigidbody.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }

        // ENEMY IS ON THE RIGHT SIDE, GO LEFT
        else
        {
            villainRigidbody.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }

    }

    void StopChasingPlayer()
    {

        villainRigidbody.velocity = Vector2.zero;
    }

    void Attack()
    {
        myAnimator.SetBool("Attack", true);
        villainRigidbody.velocity = Vector2.zero;
        attackTimer += Time.deltaTime;

        if(attackTimer > 2)
        {
            myAnimator.SetBool("Attack", false);
            attackTimer = 0;
        }

    }

    public void Knocked()
    {
        villainRigidbody.velocity = Vector2.zero;
        myAnimator.SetBool("Knocked", true);
        getHittedCount = 0;
        //knocked falseksi ja collider 2d disabele/enblae ni se on siinä
    }

    public void VillainHealth(int damage)
    {
        enemyHealth -= damage;
        print("LÄPI");
        if(enemyHealth <= 0)
        {

            //animaatio tähän ja vihun tuhoutuminen
        }
    }
}
