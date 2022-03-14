using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public GameObject helpButton;
    public GameObject level1Button;
    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;

    public GameObject controlsHdText;
    public GameObject controlsText;
    public GameObject PickUpHdText;
    public GameObject PickUpText;
    public GameObject PickUp2Text;
    public Image HealthSmall;
    public Image HealthMax;

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
    }

    public void HelpButton()
    {

        controlsHdText.SetActive(true);
        controlsText.SetActive(true);
        PickUpHdText.SetActive(true);
        PickUpText.SetActive(true);
        PickUp2Text.SetActive(true);
        HealthSmall.gameObject.SetActive(true);
        HealthMax.gameObject.SetActive(true);

        logoText.SetActive(false);
        playButton.SetActive(false);
        introductionButton.SetActive(false);
        levelsButton.SetActive(false);
        quitButton.SetActive(false);
        helpButton.SetActive(false);

        backButton.SetActive(true);
    }

    public void BackButton()
    {
        introductionText.SetActive(false);

        controlsHdText.SetActive(false);
        controlsText.SetActive(false);
        PickUpHdText.SetActive(false);
        PickUpText.SetActive(false);
        PickUp2Text.SetActive(false);
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
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
