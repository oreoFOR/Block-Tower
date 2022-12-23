using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int score;
    [SerializeField] TextMeshProUGUI scoreCounter;
    bool gameOver;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        GameEvents.instance.onPlatformGenerate += IncrementScore;
        GameEvents.instance.onGameOver += GameOver;
        GameEvents.instance.onGameStarted += StartGame;
    }
    void IncrementScore()
    {
        score++;
        scoreCounter.text = score.ToString();
    }
    void StartGame()
    {
        TinySauce.OnGameStarted();
    }
    public void GameOver()
    {
        gameOver = true;
        PlayerPrefs.SetInt("LastScore", score);
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        TinySauce.OnGameFinished(score);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
