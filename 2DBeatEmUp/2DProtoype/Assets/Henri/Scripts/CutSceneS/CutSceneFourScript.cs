using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutSceneFourScript : MonoBehaviour
{

    //VARIABLE FOR SKIP BUTTON TEXT
    public GameObject skipText;

    //VARIABLES FOR STORYTEXTS AND IMAGES
    public GameObject cutsceneOneText;
    public GameObject cutsceneTwoText;
    public GameObject cutsceneThreeText;
    public GameObject cutsceneFourText;
    
    public Image cutsceneImage;

    public Image insultImage1;
    public Image insultImage2;
    public Image insultImage3;
    public Image insultImage4;

    public Image fadeOutImageImage;

    public AudioSource myAudio;
    public AudioClip[] effects;


    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        StartCoroutine(CutsceneFour());   
    }

    // Update is called once per frame
    void Update()
    {
        //SKIPS THE CUTSCENE TO GO TO THE FIRST LEVEL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("FinalLevel");
        }
    }
    public void PlayAudio(int trackNumber)
    {
        myAudio.clip = effects[trackNumber];
        myAudio.Play();
    }

    public IEnumerator CutsceneFour()
    {
        yield return new WaitForSeconds(0.5f);
        cutsceneImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneOneText.SetActive(true);
        insultImage1.gameObject.SetActive(true);
        insultImage2.gameObject.SetActive(true);
        insultImage3.gameObject.SetActive(true);
        insultImage4.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneTwoText.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneThreeText.SetActive(true);
        skipText.SetActive(true);
        yield return new WaitForSeconds(4f);
        cutsceneFourText.SetActive(true);
        yield return new WaitForSeconds(6f);
        fadeOutImageImage.gameObject.SetActive(true);
        PlayAudio(0);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("FinalLevel");

    }

}
