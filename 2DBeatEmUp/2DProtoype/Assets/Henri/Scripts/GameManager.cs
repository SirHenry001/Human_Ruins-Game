using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // VARIABLES FOR UI & BUTTONS
    public GameObject gameOverText;
    public GameObject retryButton;
    public GameObject quitButton;

    // VARIABLE FOR OTHER SCRIPTS
    public PlayerMovement playerMovement;
    public GameMenuScreen gameMenuScreen;

    // VARIABLES FOR BOSS UI
    public GameObject bossNameText;
    public Image bossHealthImage;

    //---VARIABLES FOR AUDIO
    public AudioSource myAudio;
    public AudioClip[] music;

    // VARIABLES FOR SCORE
    public int sanityScore;
    public GameObject hpBonusText;
    public GameObject sanityBonusText;

    public bool musicOn;
    public bool bossMusicOn;

    // Start is called before the first frame update
    void Start()
    {
        // DEFINING SCRIPT VARIABLES TO FIND OTHER SCRIPTS
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();
        myAudio = GetComponent<AudioSource>();


        // PLAY THIS MUSIC AT START
        //PlayMusic(0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PLayFX()
    {

    }

    public void SanityBonus(int sanitybonus)
    {
        sanityScore += sanitybonus;
        sanityBonusText.GetComponent<TextMeshProUGUI>().text = "Sanity bonus - " + sanityScore.ToString();
    }

    // PLAYER DEAT ACTIVATION
    public IEnumerator PLayerDeath()
    {
        print("alku");
        yield return new WaitForSeconds(1f);
        print("loppu");
        Time.timeScale = 1;
        yield return new WaitForSeconds(1f);
        gameOverText.SetActive(true);
        retryButton.SetActive(true);
        quitButton.SetActive(true);
    }
}
