using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public GameObject logoText;
    public GameObject playButton;
    public GameObject introductionButton;
    public GameObject levelsButton;
    public GameObject quitButton;

    public GameObject StoryText;
    public GameObject introductionText;
    public GameObject backButton;
    public GameObject levelBackButton;

    public GameObject helpButton;
    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;
    public GameObject startLevel1Button;
    public GameObject startLevel2Button;
    public GameObject startLevel3Button;
    public GameObject startLevel4Button;

    public GameObject sanityHdText;
    public GameObject sanityInfoText;
    public GameObject controlsHdText;
    public GameObject controlsText;
    public GameObject PickUpHdText;
    public GameObject PickUpText;
    public GameObject PickUp2Text;

    public GameObject highScore1Text;
    public GameObject highScore1TextText;
    public GameObject highScore2Text;
    public GameObject highScore2TextText;
    public GameObject highScore3Text;
    public GameObject highScore3TextText;

    public Image level1Image;
    public Image level2Image;
    public Image level3Image;
    public Image level4Image;
    public Image displayImage;
    public Image sanityBar;
    public Image healthBar;
    public Image HealthSmall;
    public Image HealthMax;

    public AudioManager audioManager;

    public void Start()
    {
        highScore1Text.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("HiScoreText1").ToString();
        highScore2Text.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("HiScoreText").ToString();
        highScore3Text.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("HiScoreText3").ToString();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Level1Menu()
    {
        startLevel1Button.SetActive(true);
        highScore1Text.SetActive(true);
        highScore1TextText.SetActive(true);

        level1Button.SetActive(false);
        level2Button.SetActive(false);
        level3Button.SetActive(false);
        level4Button.SetActive(false);
        backButton.SetActive(false);
        levelBackButton.SetActive(true);

        level1Image.gameObject.SetActive(true);
        audioManager.PlayFX(2);
    }

    public void Level2Menu()
    {
        startLevel2Button.SetActive(true);
        highScore2Text.SetActive(true);
        highScore2TextText.SetActive(true);

        level1Button.SetActive(false);
        level2Button.SetActive(false);
        level3Button.SetActive(false);
        level4Button.SetActive(false);
        backButton.SetActive(false);
        levelBackButton.SetActive(true);

        level2Image.gameObject.SetActive(true);

        audioManager.PlayFX(2);
    }

    public void Level3Menu()
    {
        startLevel3Button.SetActive(true);
        highScore3Text.SetActive(true);
        highScore3TextText.SetActive(true);

        level1Button.SetActive(false);
        level2Button.SetActive(false);
        level3Button.SetActive(false);
        level4Button.SetActive(false);
        backButton.SetActive(false);
        levelBackButton.SetActive(true);

        level3Image.gameObject.SetActive(true);

        audioManager.PlayFX(2);
    }

    public void Level4Menu()
    {
        startLevel4Button.SetActive(true);

        level1Button.SetActive(false);
        level2Button.SetActive(false);
        level3Button.SetActive(false);
        level4Button.SetActive(false);
        backButton.SetActive(false);
        levelBackButton.SetActive(true);

        level4Image.gameObject.SetActive(true);

        audioManager.PlayFX(2);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Cutscene1");
    }

    public void Level2()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Cutscene2");
    }

    public void Level3()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Cutscene3");
    }

    public void Level4()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Cutscene4");
    }

    public void LevelSelect()
    {

        playButton.SetActive(false);
        introductionButton.SetActive(false);
        levelsButton.SetActive(false);
        quitButton.SetActive(false);
        helpButton.SetActive(false);

        level1Button.SetActive(true);
        level2Button.SetActive(true);
        level3Button.SetActive(true);
        level4Button.SetActive(true);

        backButton.SetActive(true);
        levelBackButton.SetActive(false);
        startLevel1Button.SetActive(false);
        startLevel2Button.SetActive(false);
        startLevel3Button.SetActive(false);
        startLevel4Button.SetActive(false);
        highScore1Text.SetActive(false);
        highScore1TextText.SetActive(false);
        highScore2Text.SetActive(false);
        highScore2TextText.SetActive(false);
        highScore3Text.SetActive(false);
        highScore3TextText.SetActive(false);

        level1Image.gameObject.SetActive(false);
        level2Image.gameObject.SetActive(false);
        level3Image.gameObject.SetActive(false);
        level4Image.gameObject.SetActive(false);

        audioManager.PlayFX(2);
    }

    public void Introduction()
    {
        introductionText.SetActive(true);
        logoText.SetActive(false);
        playButton.SetActive(false);
        introductionButton.SetActive(false);
        levelsButton.SetActive(false);
        quitButton.SetActive(false);
        helpButton.SetActive(false);

        backButton.SetActive(true);
        audioManager.PlayFX(2);
    }

    public void HelpButton()
    {
        sanityHdText.SetActive(true);
        sanityInfoText.SetActive(true);
        controlsHdText.SetActive(true);
        controlsText.SetActive(true);
        PickUpHdText.SetActive(true);
        PickUpText.SetActive(true);
        PickUp2Text.SetActive(true);
        displayImage.gameObject.SetActive(true);
        sanityBar.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
        HealthSmall.gameObject.SetActive(true);
        HealthMax.gameObject.SetActive(true);

        logoText.SetActive(false);
        playButton.SetActive(false);
        introductionButton.SetActive(false);
        levelsButton.SetActive(false);
        quitButton.SetActive(false);
        helpButton.SetActive(false);

        backButton.SetActive(true);
        audioManager.PlayFX(2);
    }

    public void BackButton()
    {
        introductionText.SetActive(false);

        sanityHdText.SetActive(false);
        sanityInfoText.SetActive(false);
        controlsHdText.SetActive(false);
        controlsText.SetActive(false);
        PickUpHdText.SetActive(false);
        PickUpText.SetActive(false);
        PickUp2Text.SetActive(false);
        displayImage.gameObject.SetActive(false);
        sanityBar.gameObject.SetActive(false);
        healthBar.gameObject.SetActive(false);
        HealthSmall.gameObject.SetActive(false);
        HealthMax.gameObject.SetActive(false);

        logoText.SetActive(true);
        playButton.SetActive(true);
        introductionButton.SetActive(true);
        levelsButton.SetActive(true);
        quitButton.SetActive(true);
        helpButton.SetActive(true);

        level1Button.SetActive(false);
        level2Button.SetActive(false);
        level3Button.SetActive(false);
        level4Button.SetActive(false);

        backButton.SetActive(false);

        audioManager.PlayFX(1);
        
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
