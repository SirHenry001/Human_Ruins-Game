using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstHighScore : MonoBehaviour
{
    public int score;
    public int hiScore;

    public GameObject scoreText;
    public GameObject hiScoreText1;
    public GameObject scoreTextMenu;


    // Start is called before the first frame update
    void Start()
    {
        hiScore = PlayerPrefs.GetInt("HiScoreText1");
        hiScoreText1.GetComponent<TextMeshProUGUI>().text = "High score = " + hiScore.ToString();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int scoreadd)
    {
        score += scoreadd;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        scoreTextMenu.GetComponent<TextMeshProUGUI>().text = "Level Score -  " + score.ToString();

        if (score > hiScore)
        {
            hiScore = score;
            hiScoreText1.GetComponent<TextMeshProUGUI>().text = "High score = " + hiScore.ToString();
            PlayerPrefs.SetInt("HiScoreText1", hiScore);
            PlayerPrefs.Save();
        }
    }
}
