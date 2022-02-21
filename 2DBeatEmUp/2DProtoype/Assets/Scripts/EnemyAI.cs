using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator myAnimator;

    public int enemyHealth = 100;
    public int getHittedCount = 5;

    public float speed = 3f;
    public float fleeSpeed = 3f;
    public Rigidbody2D myRigidbody;

    public Transform target;
    public float attackDistance = 15f;
    public float hitDistance = 6f;
    public float standDistance = 3f;
    public float minY = -0.3f, maxY = 3f;
    public float minX = -0.3f, maxX = 3f;

    public Vector2 movement;

    public bool alertOn = false;
    public bool isFleeing = false;
    public bool facingLeft = true;
    public bool attackingLeft = true;
    public bool canHit = false;
    public bool canWalk = false;
    public bool getHitted = false;
    public bool isActive = true;
    public LayerMask layerMask;

    //public FightCollisionEnemy fightCollisionEnemy;


    // Start is called before the first frame update
    void Start()
    {
        //fightCollisionEnemy = GetComponentInChildren<FightCollisionEnemy>(); // KORVAA TÄMÄ ANIMAATTORISSA PÄÄLL/POIS JA KOODI POIS KAIKISTA KOHDISTA, ONKO TÄMÄ EDES HYÖDYLLINEN TARKISTA!
        myAnimator = GetComponentInChildren<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("Idle", 0f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;

        // IF PLAYER IS MOVING RIGHT CHARACTER ALSO FLIPS FACING RIGHT IN UNITY

        float x = target.position.x - transform.position.x;

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
        

    }

    void FixedUpdate()
    {
        if(isFleeing == true)
        {
            if(attackingLeft == true)
            {
                myRigidbody.MovePosition((Vector2)transform.position - (Vector2.right * -fleeSpeed * Time.deltaTime));
            }

            else if (attackingLeft == false)
            {
                myRigidbody.MovePosition((Vector2)transform.position - (Vector2.right * fleeSpeed * Time.deltaTime));
            }
        }
    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
    }

    void Idle()
    {
        
        myAnimator.SetBool("MonsterWalk", false);
        EnemyBoundaries();

        if(isActive == true)
        {
            Notice();
        }
        

    }

    void Notice()
    {
        if (isActive == true)
        {
            if (Vector3.Distance(transform.position, target.position) <= attackDistance && !isFleeing)
            {
                alertOn = true;
            }

            if (alertOn == true)
            {
                Approach(movement);
            }
        }




    }

    void Approach(Vector3 direction)
    {

        if (isActive == true)
        {
            // RIGIDBODY BASED ENEMY MOVEMENT
            myRigidbody.MovePosition((Vector3)transform.position + (direction * speed * Time.deltaTime));
            canWalk = true;
            myAnimator.SetBool("MonsterWalk", true);

            if (Vector3.Distance(transform.position, target.position) <= hitDistance)
            {
                StartCoroutine(Punch());
            }

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

        if(isActive == true)
        {
            canHit = true;
            myAnimator.SetBool("MonsterHit", true);
            GetComponentInChildren<CircleCollider2D>().enabled = true;

            yield return new WaitForSeconds(1f);
            canHit = false;
            myAnimator.SetBool("MonsterHit", false);
            GetComponentInChildren<CircleCollider2D>().enabled = false;
            StartCoroutine(Flee(movement));
        }

    }

    public IEnumerator Flee(Vector2 direction)
    {
        if(isActive == true)
        {
            isFleeing = true;
            alertOn = false;
            myAnimator.SetBool("MonsterFlee", true);
            myAnimator.SetBool("MonsterHit", false);

            yield return new WaitForSeconds(2f);
            myAnimator.SetBool("MonsterFlee", false);
            isFleeing = false;
            Idle();
        }

    }

    //public IEnumerator GetHitted()
    public void GetHitted()
    {
        
        //KOKEILU 1
        if (getHitted == true)
        {
            //isActive = false;
            myAnimator.SetBool("GetHitted", true);
            print("odotus alkaa");

        }

        if(getHitted == false)
        {
            //isActive = true;
            myAnimator.SetBool("GetHitted", false);
            print("odotus ohi");
        }
        

        /*
        //KOKEILU 2
        myAnimator.SetBool("GetHitted", true);
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<CapsuleCollider2D>().enabled = true;
        isActive = true;
        myAnimator.SetBool("GetHitted", false);
        */

        //alertOn = false;
        //isFleeing = false;
        //myRigidbody.velocity = Vector3.zero;

        /*
        else if(getHitted == false)
        {
            myAnimator.SetBool("GetHitted", false);
            isActive = true;
        }
        */

        else if(5 <= getHittedCount)
        {
            getHittedCount = 0;
            StartCoroutine(KnockDown());
        }

        else if (enemyHealth <= 0)
        {
            StartCoroutine(Dead());
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

    public void EnemyHealth()
    {
        enemyHealth -= 1;

        if(enemyHealth <= 0)
        {
            StartCoroutine(Dead());
        }
    }


    public IEnumerator Dead()
    {
        myAnimator.SetBool("Dead",true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void EnemyBoundaries()
    {
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY));
    }

}
