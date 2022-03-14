using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuScreen : MonoBehaviour
{
    // VARIABLES FOR IN GAME MANU UI AND NAVIGATION
    public GameObject pauseText;
    public GameObject resumeButton;
    public GameObject retryButton;
    public GameObject quitButton;

    // VARIABLES FOR SCORESCREEN 1 UI AND NAVIGATION
    public Image fadeOutImage;
    public Image fadeInImage;
    public Image scoreScreenBG;
    public Image firstLevelPlayerImage;
    
    public GameObject levelOne1ClearText;
    public GameObject highScoreText;
    public GameObject clearedTextScorecreen;
    public GameObject scoreTextEnd;
    public GameObject hpBonusTextEnd;
    public GameObject sanityBonusTextEnd;
    public GameObject totalTextEnd;
    public GameObject continueButton;
    public GameObject backButton;

    public PlayerMovement playerMovement;

    public bool scoreScreen = false;



    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if(scoreScreen == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseText.SetActive(false);
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void RetryLevel1()
    {
        Time.timeScale = 1f;
        pauseText.SetActive(false);
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        SceneManager.LoadScene("Level1");
    }

    public void RetryLevel2()
    {
        Time.timeScale = 1f;
        pauseText.SetActive(false);
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        SceneManager.LoadScene("Level2");
    }

    public void RetryLevel3()
    {
        Time.timeScale = 1f;
        pauseText.SetActive(false);
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        SceneManager.LoadScene("Level3");
    }

    public void RetryLevel4()
    {
        Time.timeScale = 1f;
        pauseText.SetActive(false);
        resumeButton.SetActive(false);
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        SceneManager.LoadScene("FinalLevel");
    }


    public void Mainmenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }

    public void CutSceneTwo()
    {
        SceneManager.LoadScene("Cutscene2");
    }

    public void CutSceneThree()
    {
        SceneManager.LoadScene("Cutscene3");
    }

    public void CutSceneFour()
    {
        SceneManager.LoadScene("Cutscene4");
    }


    public void Pause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pauseText.SetActive(true);
            resumeButton.SetActive(true);
            retryButton.SetActive(true);
            quitButton.SetActive(true);
        }

        else
        {
            Time.timeScale = 1f;
            pauseText.SetActive(false);
            resumeButton.SetActive(false);
            retryButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }


    //FUNCTION WHICH NEEDED TO SCORESCREEN IN LEVEL ONE TO POP UP
    public IEnumerator ScoreScreen()
    {
        print("alku");
        scoreScreen = true;
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
        print("loppu");
        playerMovement.playerSanity = 10000;
        levelOne1ClearText.SetActive(true);
        yield return new WaitForSeconds(3f);
        fadeOutImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        scoreScreenBG.gameObject.SetActive(true);
        firstLevelPlayerImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        clearedTextScorecreen.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        scoreTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hpBonusTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        sanityBonusTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        totalTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        highScoreText.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        continueButton.SetActive(true);
        backButton.SetActive(true);


    }

    public IEnumerator EndScreen()
    {
        print("alku");
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
        print("loppu");
        playerMovement.playerSanity = 100;
        levelOne1ClearText.SetActive(true);
        yield return new WaitForSeconds(3f);
        fadeOutImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        scoreScreenBG.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        firstLevelPlayerImage.gameObject.SetActive(true);
        clearedTextScorecreen.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        scoreTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hpBonusTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        totalTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        continueButton.SetActive(true);
        backButton.SetActive(true);


    }
}
