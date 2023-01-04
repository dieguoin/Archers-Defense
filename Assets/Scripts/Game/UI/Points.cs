using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    public static Points instance;
    [SerializeField] private GameObject PointsText;
    [SerializeField] private GameObject highScoreText;

    private int points;
    private int highScore;

    private string highScorePrefsName = "HighScore";


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        LoadData();
    }
    void OnDestroy()
    {
        if (points > highScore)
        {
            NewHighScore();
        }
        SaveData();

        if (instance == this)
        {
            instance = null;
        }
    }

    public void AddPoints(int plusPoints)
    {
        points += plusPoints;
        PointsText.GetComponent<Text>().text = points.ToString(); 
    }
    private void NewHighScore()
    {
        highScore = points;
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(highScorePrefsName, highScore);
    }
    private void LoadData()
    {
        highScore = PlayerPrefs.GetInt(highScorePrefsName, 0);
        highScoreText.GetComponent<Text>().text = "HighScore: " + highScore.ToString();

    }
}
