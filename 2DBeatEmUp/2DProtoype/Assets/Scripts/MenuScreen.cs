using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{

    public GameObject playButton;
    public GameObject ControlsButton;
    public GameObject backButton;
    public GameObject QuitButton;

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }

    public void Controls()
    {

    }

    public void BackButton()
    {

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
