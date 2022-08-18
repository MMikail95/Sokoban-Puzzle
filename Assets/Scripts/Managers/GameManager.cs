using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance
    public static GameManager instance;
    void InitSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private void Awake()
    {
        InitSingleton();
    }
    public bool gameStarted;

    public void StartGame()
    {
        gameStarted = true;
        UiManager.instance.StartGame();
    }
    public void NextGame()
    {
        gameStarted = true;
        Player.instance.ResetPlayer();
        UiManager.instance.NextGame();
    }
    public void RestartGame()
    {
        gameStarted = true;
        Player.instance.ResetPlayer();
        UiManager.instance.RestartGame();
    }
    public void OnGameEnd(bool isWin)
    {
        gameStarted = false;
        UiManager.instance.OnGameEnd(isWin);
    }
}
