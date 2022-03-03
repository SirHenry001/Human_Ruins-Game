using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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


    // Start is called before the first frame update
    void Start()
    {
        // DEFINING SCRIPT VARIABLES TO FIND OTHER SCRIPTS
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameMenuScreen = GameObject.Find("Canvas").GetComponent<GameMenuScreen>();

    }

    // Update is called once per frame
    void Update()
    {
        
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
