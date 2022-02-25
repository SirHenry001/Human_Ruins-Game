using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // ENEMY HEALTH
    public int enemyHealth = 100;

    // VARIABLES FOR SPEED
    public float speed = 3f;
    public float fleeSpeed = 3f;
    
    // VARIABLES FOR DISTANCES
    public float aggroRange = 15f;
    public float faceToFaceRange = 6f;

    //VARIABLES FOR BOUNDARIES
    public float minY = -0.3f, maxY = 3f;
    public float minX = -0.3f, maxX = 3f;

    // VECTOR WHICH NEEDED FOR MOVEMENT
    public Vector2 movement;

    // BOOLEANS
    public bool alertOn = false;
    public bool isFleeing = false;
    public bool facingLeft = true;
    public bool attackingLeft = true;

    // COMPONENTS WHICH NEEDED ON CODE
    public Rigidbody2D myRigidbody;
    public Animator myAnimator;
    public Transform target;

    // ACCESS FOR OTHER SCRIPTS
    public FightCollisionEnemy fightCollisionEnemy;

    // TYÖN ALLA
    //public bool canHit = false;
    //public bool getHitted = false;
    //public bool isActive = true;
    public int getHittedCount = 5;


    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Idle", 0f, 0.01f);
        //fightCollisionEnemy = GetComponentInChildren<FightCollisionEnemy>();
        myAnimator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        EnemyBoundaries();

        float x = target.position.x - transform.position.x;
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;

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
                myRigidbody.MovePosition((Vector2)transform.position - (Vector2.right * -fleeSpeed * Time.deltaTime));
            }

            else if (attackingLeft == false)
            {
                myRigidbody.MovePosition((Vector2)transform.position - (Vector2.right * fleeSpeed * Time.deltaTime));
            }
        }

        if (distance <= aggroRange && !isFleeing)
        {
            alertOn = true;
            Approach(movement);
        }

        if (distance <= faceToFaceRange)
        {
            StartCoroutine(Punch());
        }

    }

    void Approach(Vector3 direction)
    {

            // RIGIDBODY BASED ENEMY MOVEMENT

        if (alertOn == true)
        {
            myRigidbody.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
            myAnimator.SetBool("MonsterWalk", true);

            if (facingLeft)
            {
                attackingLeft = true;
            }

            else if (!facingLeft)
            {
                attackingLeft = false;
            }
        }
            

    }

    public IEnumerator Punch()
    {

        alertOn = false;
        myAnimator.SetBool("MonsterWalk", false);
        myAnimator.SetBool("MonsterHit", true);
        yield return new WaitForSeconds(1f);
        myAnimator.SetBool("MonsterHit", false);
        StartCoroutine(Flee(movement));

    }

    public IEnumerator Flee(Vector2 direction)
    {
            isFleeing = true;
            alertOn = false;
            myAnimator.SetBool("MonsterFlee", true);
            myAnimator.SetBool("MonsterHit", false);

            yield return new WaitForSeconds(2f);

            myAnimator.SetBool("MonsterFlee", false);
            alertOn = false;
            isFleeing = false;
    }

    public void GetHitted()
    {

        myAnimator.SetTrigger("GetHitted");

        if (5 <= getHittedCount)
        {
            getHittedCount = 0;
            StartCoroutine(KnockDown());
        }

    }
        
    public IEnumerator KnockDown()
    {
        myAnimator.SetBool("KnockedDown", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<CapsuleCollider2D>().enabled = true;
        myAnimator.SetBool("KnockedDown", false);
    }

    void EnemyBoundaries()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY));
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
    }

    public void EnemyHealth()
    {
        enemyHealth -= 1;

        if(enemyHealth <= 0)
        {
            //animaatio ja tuhoaminen
        }
    }



}
