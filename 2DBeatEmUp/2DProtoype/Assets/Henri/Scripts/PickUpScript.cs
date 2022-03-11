using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    public PlayerMovement playerMovement;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            if (Input.GetButtonDown("Jump"))
            {
                Destroy(collision.gameObject);
                playerMovement.playerHealth += 20;
                playerMovement.healthImage.fillAmount = playerMovement.playerHealth * 0.01f;
            }
        }

        if (collision.gameObject.tag == "PickUpMax")
        {
            if (Input.GetButtonDown("Jump"))
            {
                Destroy(collision.gameObject);
                playerMovement.playerHealth = 100;
                playerMovement.healthImage.fillAmount = playerMovement.playerHealth * 0.01f;
            }
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
