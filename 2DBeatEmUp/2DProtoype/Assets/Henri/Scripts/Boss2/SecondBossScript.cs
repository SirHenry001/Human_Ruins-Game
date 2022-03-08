using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossScript : MonoBehaviour
{

    public int bossHealth = 100;

    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;
    public GameManager gameManager;

    public Animator bossTwoAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossTwoAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossTwoHealth(int damage)
    {
        bossHealth -= damage;
        gameManager.bossHealthImage.fillAmount = bossHealth * 0.01f;

        if (bossHealth <= 0)
        {
            Time.timeScale = 0.2f;
            bossTwoAnimator.SetBool("Dead", true);
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            GetComponent<SecondBossScript>().enabled = false;
            StartCoroutine(gameMenuScreen.ScoreScreen());
        }
    }


}
