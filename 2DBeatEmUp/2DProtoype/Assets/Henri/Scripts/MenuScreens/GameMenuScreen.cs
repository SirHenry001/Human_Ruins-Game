using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public GameObject highScoreText1;
    public GameObject highScoreText;
    public GameObject highScoreText3;
    public GameObject clearedTextScorecreen;
    public GameObject scoreTextEnd;
    public GameObject hpBonusTextEnd;
    public GameObject sanityBonusTextEnd;
    public GameObject totalTextEnd;
    public GameObject continueButton;
    public GameObject backButton;

    public PlayerMovement playerMovement;
    public GameManager gameManager;
    public MusicManager musicManager;
    public AudioManager audioManager;
    public LevelHighScore levelScore;
    public FirstHighScore levelScore1;
    public ThirdHighScore levelScore3;

    //VARIABLES FOR END SCREEN
    public Image endBG;
    public Image Layer1;
    public GameObject gameByText;
    public GameObject name1;
    public GameObject name2;
    public Image Layer2;
    public GameObject conceptText;
    public GameObject name_1;
    public GameObject name_2;
    public GameObject codeText;
    public GameObject name__1;
    public GameObject artText;
    public GameObject name__2;
    public Image Layer3;
    public GameObject thankYouText;
    public GameObject quitButton_2;

    public bool scoreScreen = false;



    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent < AudioManager>();
        hpBonusTextEnd = gameManager.hpBonusText;
        sanityBonusTextEnd = gameManager.sanityBonusText;

        levelScore = GameObject.Find("LevelHighScore").GetComponent<LevelHighScore>();
        levelScore1 = GameObject.Find("LevelHighScore").GetComponent<FirstHighScore>();
        levelScore3 = GameObject.Find("LevelHighScore").GetComponent<ThirdHighScore>();

        if (levelScore != null)
        {
            scoreTextEnd = levelScore.scoreTextMenu;
            highScoreText = levelScore.hiScoreText;
        }

        if (levelScore1 != null)
        {
            scoreTextEnd = levelScore1.scoreTextMenu;
            highScoreText1 = levelScore1.hiScoreText1;
        }

        if (levelScore3 != null)
        {
            scoreTextEnd = levelScore3.scoreTextMenu;
            highScoreText3 = levelScore3.hiScoreText3;
        }



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
        musicManager.myAudio.Stop();
        Time.timeScale = 0.2f;
        audioManager.PlayFX(1);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
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
        int totalscore = levelScore1.score + gameManager.sanityScore + playerMovement.playerHealth;
        totalTextEnd.GetComponent<TextMeshProUGUI>().text = "Total score - " + totalscore.ToString();
        totalTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if(totalscore > levelScore1.hiScore)
        {
            levelScore1.hiScore = totalscore;
            PlayerPrefs.SetInt("HiScoreText1", totalscore);
        }

        highScoreText1.SetActive(true);
        highScoreText1.GetComponent<TextMeshProUGUI>().text = "High Score - " + PlayerPrefs.GetInt("HiScoreText1").ToString();
        yield return new WaitForSeconds(0.2f);
        continueButton.SetActive(true);
        backButton.SetActive(true);
        //PlayerPrefs.SetInt("Level1HighScore", levelScore1.hiScore);
        print("loppu");
    }

    public IEnumerator ScoreScreen2()
    {

        print("alku");
        scoreScreen = true;
        yield return new WaitForSeconds(0.1f);
        musicManager.myAudio.Stop();
        Time.timeScale = 0.2f;
        audioManager.PlayFX(1);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
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
        int totalscore = levelScore.score + gameManager.sanityScore + playerMovement.playerHealth;
        totalTextEnd.GetComponent<TextMeshProUGUI>().text = "Total score - " + totalscore.ToString();
        totalTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if (totalscore > levelScore.hiScore)
        {
            levelScore.hiScore = totalscore;
            PlayerPrefs.SetInt("HiScoreText", totalscore);
        }

        highScoreText.SetActive(true);
        highScoreText.GetComponent<TextMeshProUGUI>().text = "High Score - " + PlayerPrefs.GetInt("HiScoreText").ToString();
        yield return new WaitForSeconds(0.2f);
        continueButton.SetActive(true);
        backButton.SetActive(true);
        //PlayerPrefs.SetInt("Level2HighScore", levelScore.hiScore);
        print("loppu");


    }

    public IEnumerator ScoreScreen3()
    {

        print("alku");
        scoreScreen = true;
        yield return new WaitForSeconds(0.1f);
        musicManager.myAudio.Stop();
        Time.timeScale = 0.2f;
        audioManager.PlayFX(1);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
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
        int totalscore = levelScore3.score + gameManager.sanityScore + playerMovement.playerHealth;
        totalTextEnd.GetComponent<TextMeshProUGUI>().text = "Total score - " + totalscore.ToString();
        totalTextEnd.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if (totalscore > levelScore3.hiScore)
        {
            levelScore3.hiScore = totalscore;
            PlayerPrefs.SetInt("HiScoreText3", totalscore);
        }

        highScoreText3.SetActive(true);
        highScoreText3.GetComponent<TextMeshProUGUI>().text = "High Score - " + PlayerPrefs.GetInt("HiScoreText3").ToString();
        yield return new WaitForSeconds(0.2f);
        continueButton.SetActive(true);
        backButton.SetActive(true);
        //PlayerPrefs.SetInt("Level3HighScore", levelScore3.hiScore);
        print("loppu");

    }

    public IEnumerator EndScreen()
    {
        scoreScreen = true;
        yield return new WaitForSeconds(0.1f);
        musicManager.myAudio.Stop();
        Time.timeScale = 0.2f;
        audioManager.PlayFX(1);
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1;
        playerMovement.playerSanity = 10000;
        levelOne1ClearText.SetActive(true);
        yield return new WaitForSeconds(3f);
        musicManager.PlayMusic(0);
        fadeOutImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        endBG.gameObject.SetActive(true);
        Layer1.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameByText.SetActive(true);
        name1.SetActive(true);
        name2.SetActive(true);
        yield return new WaitForSeconds(10f);
        Layer2.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        conceptText.SetActive(true);
        name_1.SetActive(true);
        name_2.SetActive(true);
        codeText.SetActive(true);
        name__1.SetActive(true);
        artText.SetActive(true);
        name__2.SetActive(true);
        yield return new WaitForSeconds(10f);
        Layer3.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        thankYouText.SetActive(true);
        yield return new WaitForSeconds(1f);
        quitButton_2.SetActive(true);
    }
}
