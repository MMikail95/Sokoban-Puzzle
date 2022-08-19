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
    [SerializeField] private GameObject _mainPanel;
    #endregion

    private void Awake()
    {
        InitSingleton();
    }
    public void NewGame()
    {
        _mainPanel.SetActive(false);       
    }
    public void Continue()
    {
        _mainPanel.SetActive(false);
    }
    public void Options()
    {
       
    }
    public void Quit()
    {

    }
}
