using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public event Action onPlatformGenerate;  
    public void PlatformPassed()
    {
        onPlatformGenerate();
    }
    public event Action<bool> onGameOver;
    public void GameOver(bool success)
    {
        onGameOver(success);
    }
    public event Action onGameStarted;
    public void StartGame()
    {
        onGameStarted();
    }
}
