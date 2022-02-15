using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScreen : MonoBehaviour
{

    public GameObject pauseText;
    public GameObject resumeButton;
    public GameObject retryButton;
    public GameObject quitButton;

    //public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        //playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
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

    public void Mainmenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
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
}
