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

    public void NewGame()
    {
        gameStarted = true;
        UiManager.instance.NewGame();
    }
    public void Continue()
    {
        gameStarted = true;
        //Player.instance.ResetPlayer();
        UiManager.instance.Continue();
    }
    public void Options()
    {
        gameStarted = true;
        UiManager.instance.Options();
    }

    public void Quit()
    {
        UiManager.instance.Quit();
    }
}
