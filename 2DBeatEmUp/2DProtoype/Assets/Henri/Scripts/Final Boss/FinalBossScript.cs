using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour
{

    public int bossHealth = 100;

    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;

    public Animator bossFinalAnimator;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        bossFinalAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossFinalHealth(int damage)
    {
        print("läpi");
        bossHealth -= damage;

        if (bossHealth <= 0)
        {
            Time.timeScale = 0.2f;
            bossFinalAnimator.SetBool("Dead", true);
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(gameMenuScreen.ScoreScreen());
        }
    }

}
