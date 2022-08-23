using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable    

    #endregion
    [SerializeField] float _move = 1f;
    private Vector3 moveDirection = Vector3.zero;
    #region Instance

    public static Player instance;
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

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * _move;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * _move;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * _move;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * _move;
        }
    }
}