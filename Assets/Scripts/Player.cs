using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable    
    //[SerializeField] private float _desiredDuration = 3f;
    //[SerializeField] private Vector3 _startPosition;
    //[SerializeField] private Vector3 _endPosition = new Vector3(5, -2, 0);
    //[SerializeField] private float elapsedTime;
    [SerializeField] private float _move = 1f;
    [SerializeField] private RaycastHit _nesne; 
    public bool isWall;

    #endregion

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
        //_startPosition = transform.position;
    }

    private void Update()
    {
        Movement();
        
        //elapsedTime += _move;
        //float percentageComplete = elapsedTime / _desiredDuration;
        //transform.position = Vector3.Lerp(_startPosition, _endPosition, Mathf.SmoothStep(0,1, percentageComplete));
    }

    private void Movement()
    {
        if (isWall == false)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * _move;
                Direction();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * _move;
                Direction();
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                transform.position += Vector3.forward * _move;
                Direction();
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                transform.position += Vector3.back * _move;
                Direction();
            }

        }
       
    }

    private void Direction()
    {
        Vector3 playerPosition = transform.position;
        Vector3 fowardDirection = transform.forward;
        Ray theRay = new Ray(playerPosition, fowardDirection);

        if (Physics.Raycast(theRay, out RaycastHit hit, 1f))
        {
            isWall = true;
        }
        
    }
}