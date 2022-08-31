using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable    
    [SerializeField] private float _move = 1f;
    [SerializeField] private RaycastHit _nesne;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 velocity = Vector3.zero;
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
    }

    private void Update()
    {
        Movement();
        Lerp();
    }

    private void Movement()
    {
        if (isLeftEmpty == true)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * _move;
            }
        }
        DirectionLeft();

        if (isRightEmpty == true)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * _move;
            }
        }
        DirectionRight();

        if (isFrontEmpty == true)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                transform.position += Vector3.Lerp(transform.position, transform.forward, _move);
            }
        }
        DirectionFoward();

        if (isBackEmpty == true)
        {
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                transform.position += Vector3.back * _move;
            }
        }
        DirectionBack();
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
                Debug.Log("Ön Duvar");
                isFrontEmpty = false;
            }
        }

        else
        {
            isFrontEmpty = true;
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
                Debug.Log("Arka Duvar");
                isBackEmpty = false;
            }
        }

        else
        {
            isBackEmpty = true;
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
                Debug.Log("Sað Duvar");
                isRightEmpty = false;
            }
        }
        else
        {
            isRightEmpty = true;
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
                Debug.Log("Sol Duvar");
                isLeftEmpty = false;
            }
        }
        else
        {
            isLeftEmpty = true;
        }
    }

    private void Lerp ()
    {
        transform.position = Vector3.Lerp(transform.position, transform.forward, Time.deltaTime);
    }
}