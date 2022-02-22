using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdBossScript : MonoBehaviour
{

    public int bossHealth = 100;

    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;

    public Animator bossThreeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        bossThreeAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossThreeHealth(int damage)
    {
        print("läpi");
        bossHealth -= damage;

        if (bossHealth <= 0)
        {
            Time.timeScale = 0.2f;
            bossThreeAnimator.SetBool("Dead", true);
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            StartCoroutine(gameMenuScreen.ScoreScreen());
        }
    }

}
