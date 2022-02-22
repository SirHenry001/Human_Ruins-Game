using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutSceneTwoScript : MonoBehaviour
{

    //VARIABLE FOR SKIP BUTTON TEXT
    public GameObject skipText;

    //VARIABLES FOR STORYTEXTS AND IMAGES
    public GameObject cutsceneOneText;
    public GameObject cutsceneTwoText;
    public Image cutsceneImage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CutsceneTwo());
    }

    // Update is called once per frame
    void Update()
    {
        //SKIPS THE CUTSCENE TO GO TO THE SECOND LEVEL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Level2");
        }
    }

    public IEnumerator CutsceneTwo()
    {
        cutsceneOneText.SetActive(true);
        yield return new WaitForSeconds(2f);
        cutsceneTwoText.SetActive(true);
        skipText.SetActive(true);
        yield return new WaitForSeconds(2f);
        cutsceneImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Level2");


    }
}
