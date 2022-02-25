using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCollisionEnemy : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public VillainAi villainAi;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerMovement.PlayerHealth(2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
