using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.AI;
using TMPro;

public class CutSceneScript : MonoBehaviour
{
    //VARIABLE FOR SKIP BUTTON TEXT
    public GameObject skipText;

    //VARIABLES FOR STORYTEXTS AND IMAGES
    public GameObject cutsceneOneText;
    public GameObject cutsceneTwoText;
    public GameObject cutsceneThreeText;
    public GameObject cutsceneFourText;
    public Image cutsceneImage;
    public Image fadeOutImageImage;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CutsceneOne());
;
    }

    // Update is called once per frame
    void Update()
    {
        //SKIPS THE CUTSCENE TO GO TO THE FIRST LEVEL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public IEnumerator CutsceneOne()
    {
        yield return new WaitForSeconds(0.5f);
        cutsceneImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneOneText.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneTwoText.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneThreeText.SetActive(true);
        skipText.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneFourText.SetActive(true);
        yield return new WaitForSeconds(6f);
        fadeOutImageImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1");


    }

}
