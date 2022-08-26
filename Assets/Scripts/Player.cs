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
    public bool isBackEmpty;
    public bool isLeftEmpty;
    public bool isFrontEmpty;
    public bool isRightEmpty;

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
        isBackEmpty = true;
        isFrontEmpty = true;
        isLeftEmpty = true;
        isRightEmpty = true;
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

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * _move;
            DirectionLeft();
        }



        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * _move;
            DirectionRight();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * _move;
            DirectionFoward();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * _move;
            DirectionBack();

        }
    }

    private void DirectionFoward()
    {
        Vector3 playerPosition = transform.position;
        Vector3 fowardDirection = transform.forward;
        Ray theRay = new Ray(playerPosition, fowardDirection);

        if (Physics.Raycast(theRay, out _nesne, 3f))
        {
            if (_nesne.collider.gameObject.tag == "Wall")
            {
                Debug.Log("Anam Duvar");
                isFrontEmpty = false;
            }
        }

    }

    private void DirectionBack()
    {
        Vector3 playerPosition = transform.position;
        Vector3 backwardDirection = -transform.forward;
        Ray theRay = new Ray(playerPosition, backwardDirection);

        if (Physics.Raycast(theRay, out _nesne, 3f))
        {
            if (_nesne.collider.gameObject.tag == "Wall")
            {
                Debug.Log("Anam Duvar");
                isBackEmpty = false;
            }
        }
    }

    private void DirectionRight()
    {
        Vector3 playerPosition = transform.position;
        Vector3 rightDirection = transform.right;
        Ray theRay = new Ray(playerPosition, rightDirection);

        if (Physics.Raycast(theRay, out _nesne, 3f))
        {
            if (_nesne.collider.gameObject.tag == "Wall")
            {
                Debug.Log("Anam Duvar");
                isRightEmpty = false;
            }
        }
    }

    private void DirectionLeft()
    {
        Vector3 playerPosition = transform.position;
        Vector3 leftDirection = -transform.right;
        Ray theRay = new Ray(playerPosition, leftDirection);

        if (Physics.Raycast(theRay, out _nesne, 3f))
        {
            if (_nesne.collider.gameObject.tag == "Wall")
            {
                Debug.Log("Anam Duvar");
                isLeftEmpty = false;
            }
        }
    }
}