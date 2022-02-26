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

    // VARIABLES FOR TIMERS
    public float timer;

    //COMPONENTS
    public Rigidbody2D bigRigidbody;
    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bigRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        //IDLE

        //APPROACH

        //FLEE

        //LONGATTACK

        //SHORTATTACK

    }
}
