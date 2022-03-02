using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstBossScript : MonoBehaviour
{

    public int bossHealth = 100;

    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;

    public Animator bossOneAnimator;

    public GameObject bossNameText;
    public Image bossHealthImage;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        bossOneAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossOneHealth(int damage)
    {
        bossHealth -= damage;
        bossHealthImage.fillAmount = bossHealth * 0.01f;

        if (bossHealth <= 0)
        {
            Destroy(bossNameText);
            Time.timeScale = 0.2f;
            bossOneAnimator.SetBool("Dead", true);
            playerMovement.GetComponent<PlayerMovement>().enabled = false;
            //print("meni läpi");
            //gameMenuScreen.Trigger();
            StartCoroutine(gameMenuScreen.ScoreScreen());
        }
    }

}
