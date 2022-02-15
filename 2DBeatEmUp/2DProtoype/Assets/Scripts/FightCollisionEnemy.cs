using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightCollisionEnemy : MonoBehaviour
{
    public PlayerMovement playerMovement;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMovement.PlayerHealth();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
