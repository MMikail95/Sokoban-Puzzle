using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    #region Instance
    public static UiManager instance;
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
    #region Panels
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _hudPanel;
    #endregion

    private void Awake()
    {
        InitSingleton();
    }
    public void StartGame()
    {
        _startPanel.SetActive(false);
        _hudPanel.SetActive(true);
    }
    public void NextGame()
    {
        _winPanel.SetActive(false);
        _hudPanel.SetActive(true);
    }
    public void RestartGame()
    {
        _losePanel.SetActive(false);
        _hudPanel.SetActive(true);
    }
    public void OnGameEnd(bool isWin)
    {
        _hudPanel.SetActive(false);

        if (isWin)
        {
            _winPanel.SetActive(true);
        }
        else
        {
            _losePanel.SetActive(true);
        }
    }
}
