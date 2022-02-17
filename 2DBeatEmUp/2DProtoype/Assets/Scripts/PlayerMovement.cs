using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    // VARIABLE FOR HEALTH
    public int playerHealth = 100;

    // VARIABLE FOR HITCOMBOS
    public int maxCombo = 5;
    public int combo = 0;
    public float cooldown = 0.5f;
    public float maxTime = 0.8f;
    public float lastTime;

    // VARIABLES FOR MOVEMENT
    public float speed = 5f;
    public float jumpSpeed = 600f;
    public float x;
    public float y;

    // VARIABLE FOR JUMP DETECTION
    public bool canJump;
    public Transform feet;
    public float radius = 0.1f;
    public LayerMask layerMask;
    public float storedY = 0;
    public GameObject jumpPlatform;
    public GameObject jumpPlatformSpawn;

    public float minY = -0.3f, maxY = 3f;
    public float minX = -25f, maxX = 55f;

    // BOOLEAN ON/OFF FUNCTIONS
    public bool facingRight = true;
    public bool canPunch = false;
    public bool canPunch2 = false;
    public bool canPunch3 = false;
    public bool canKick = false;
    public bool canKick2 = false;

    // VARIABLE FOR PHYSICS
    public Rigidbody2D myRigidbody;

    // VARIABLE FOR ANIMATOR
    public Animator myAnimator;

    // VARIABLE FOR OTHER SCRIPTS
    public EnemyAI enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        // CALL PHYSICS COMPONENT FROM UNITY TO CODE
        myRigidbody = GetComponent<Rigidbody2D>();
        // CALL ANIMATOR COMPONENT FROM UNITY TO CODE
        myAnimator = GetComponentInChildren<Animator>();
        // CALL FIGHT COLLISION SCRIPT
        enemyScript = GameObject.Find("Monster").GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // GET PLAYER TO PUNCH WITH ANIMATION 
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Combo());
        }

        // PLAYER JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {

            myRigidbody.GetComponent<Rigidbody2D>().gravityScale = 30f;
            myRigidbody.AddForce(Vector2.up * jumpSpeed);
            GameObject platform = Instantiate(jumpPlatform, jumpPlatformSpawn.transform.position, jumpPlatformSpawn.transform.rotation);
            Destroy(platform, 1f);

            // PLAYER FEET COLLISION DETECTION
            canJump = Physics2D.OverlapCircle(feet.position, radius, layerMask);

            if (canJump == true)
            {
                storedY = transform.position.y;
            }


            //canJump = true;
            //StartCoroutine(Jump());
           
        }

    }

    private void FixedUpdate()
    {
        // GOES FUNCTION AND SET BOUNDARIES FOR Y AXIS FOR MOVEMENT OF THE PLAYER
        PlayerBoundaries();

        // PLAYER MOVEMENT BASED ON RIGIDBODY PHYSICS IN UNITY
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        myRigidbody.velocity = new Vector2(x, y).normalized * speed;

        // IF PLAYER IS MOVING RIGHT CHARACTER ALSO FLIPS FACING RIGHT IN UNITY
        if(x > 0.01f && facingRight == false)
        {
            Flip();
            facingRight = !facingRight;
        }
        // IF PLAYER IS MOVING LEFT CHARACTER ALSO FLIPS FACING LEFT IN UNITY
        if (x < -0.01f && facingRight == true)
        {
            Flip();
            facingRight = !facingRight;
        }

        myAnimator.SetFloat("Horizontal", x);
        myAnimator.SetFloat("Vertical", y);
    }

    void Flip()
    {
        // FACING THE PLAYER TO WALKING DIRECTION REGARDING TO PLAYERS HORIZONTAL MOVEMENT 
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
    }

    
    void PlayerBoundaries()
    {
        //SET Y & X AXIS BOUNDARIES FOR MOVEMENT OF THE PLAYER
        transform.position = new Vector2(Mathf.Clamp(transform.position.x,minX,maxX), Mathf.Clamp(transform.position.y, minY, maxY));

    }


    // PLAYER HIT ACTIONS IF BUTTON IS PRESSED AND ENEMY IS HITTED
    public IEnumerator Combo()
    {
        
        myAnimator.SetBool("Punch", true);
        canPunch = true;
        lastTime = Time.time;
        combo++;
        //Debug.Log("Hit" + combo);

        while (true)
        {

            while ((Time.time - lastTime) <  maxTime && combo < maxCombo)
            {

                if(combo == 2)
                {
                    //StartCoroutine(enemyScript.GetHitted());
                    canPunch2 = true;
                    myAnimator.SetBool("Punch2", true);
                }

                if(combo == 3)
                {
                    canPunch3 = true;
                    myAnimator.SetBool("Punch3", true);
                }

                if(combo == 4)
                {
                    canKick = true;
                    myAnimator.SetBool("Kick", true);

                }

                if (combo ==5)
                {

                    canKick2 = true;
                    myAnimator.SetBool("Kick2", true);
                }

                yield return null;
            }

            myAnimator.SetBool("Punch", false);
            myAnimator.SetBool("Punch2", false);
            myAnimator.SetBool("Punch3", false);
            myAnimator.SetBool("Kick", false);
            myAnimator.SetBool("Kick2", false);
            canPunch = false;
            canPunch2 = false;
            canPunch3 = false;
            canKick = false;
            canKick2 = false;
            enemyScript.getHitted = false;
            combo = 0;
            enemyScript.getHittedCount = 0;
            
            yield return new WaitForSeconds(cooldown - (Time.time - lastTime));
            

        }
    }

    public IEnumerator Jump()
    {


        yield return new WaitForSeconds(1f);
        canJump = false;
    }

    public void PlayerHealth()
    {
        playerHealth -= 1;

        if (playerHealth <= 0)
        {
            //animaatio tähän mieluummin jatkossa
            Destroy(gameObject);
        }
    }
}
