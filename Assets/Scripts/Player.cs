using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable    
    [SerializeField] private float _move = 1f;
    public bool isBackEmpty;
    public bool isLeftEmpty;
    public bool isFrontEmpty;
    public bool isRightEmpty;
    public bool isCube;
    public bool isCubeLeftEmpty;
    public bool isCubeRightEmpty;
    public bool isCubeFrontEmpty;
    public bool isCubeBackEmpty;


    #endregion
    #region MoveVariables
    private bool moveTimer;
    private float moveTimerCounter;
    [SerializeField] private float moveTimerDuration;
    private Vector3 moveEndPos;
    private Vector3 moveStartPos;
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
        CheckAround(transform.position);
        isBackEmpty = true;
        isFrontEmpty = true;
        isRightEmpty = true;
        isLeftEmpty = true;
    }

    private void Update()
    {
        if (!moveTimer && !isCube)
        {
            Movement();
        }
        if (moveTimer)
        {
            Move();
        }
    }

    private void Movement()
    {
        if (isLeftEmpty && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Vector3 targetPos = transform.position + Vector3.left * _move;
            CheckAround(targetPos);
            SetMove(targetPos);
        }

        if (isRightEmpty && Input.GetKeyUp(KeyCode.RightArrow))
        {
            Vector3 targetPos = transform.position + Vector3.right * _move;
            CheckAround(targetPos);
            SetMove(targetPos);
        }

        if (isFrontEmpty && Input.GetKeyUp(KeyCode.UpArrow))
        {
            Vector3 targetPos = transform.position + Vector3.forward * _move;
            CheckAround(targetPos);
            SetMove(targetPos);
        }

        if (isBackEmpty && Input.GetKeyUp(KeyCode.DownArrow))
        {
            Vector3 targetPos = transform.position + Vector3.back * _move;
            CheckAround(targetPos);
            SetMove(targetPos);
        }
    }

    //private void DirectionFoward()
    //{
    //    Vector3 playerPosition = transform.position;
    //    Vector3 fowardDirection = Vector3.forward;
    //    Ray theRay = new Ray(playerPosition, fowardDirection);
    //    if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
    //    {
    //        Debug.Log(rayHit.collider.name);
    //        if (rayHit.collider.gameObject.tag == "Wall")
    //        {
    //            isFrontEmpty = false;
    //        }
    //    }

    //    else
    //    {
    //        isFrontEmpty = true;
    //    }
    //}

    //private void DirectionBack()
    //{
    //    Vector3 playerPosition = transform.position;
    //    Vector3 backwardDirection = Vector3.back;
    //    Ray theRay = new Ray(playerPosition, backwardDirection);

    //    if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
    //    {
    //        if (rayHit.collider.gameObject.tag == "Wall")
    //        {
    //            isBackEmpty = false;
    //        }
    //    }

    //    else
    //    {
    //        isBackEmpty = true;
    //    }
    //}

    //private void DirectionRight()
    //{
    //    Vector3 playerPosition = transform.position;
    //    Vector3 rightDirection = Vector3.right;
    //    Ray theRay = new Ray(playerPosition, rightDirection);

    //    if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
    //    {
    //        if (rayHit.collider.gameObject.tag == "Wall")
    //        {
    //            isRightEmpty = false;
    //        }
    //    }
    //    else
    //    {
    //        isRightEmpty = true;
    //    }
    //}

    //private void DirectionLeft()
    //{
    //    Vector3 playerPosition = transform.position;
    //    Vector3 leftDirection = Vector3.left;
    //    Ray theRay = new Ray(playerPosition, leftDirection);

    //    if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
    //    {
    //        if (rayHit.collider.gameObject.tag == "Wall")
    //        {
    //            isLeftEmpty = false;
    //        }
    //    }
    //    else
    //    {
    //        isLeftEmpty = true;
    //    }
    //}


    private void CheckAround(Vector3 pos)
    {
        CheckBack(pos);
        CheckRight(pos);
        CheckLeft(pos);
        CheckForward(pos);
    }
    //private bool CheckDirection(Vector3 center, Vector3 direction)
    //{
    //    if (Physics.Raycast(center, direction, out RaycastHit raycastHit, 2f) && raycastHit.collider.CompareTag("Wall"))
    //    {
    //        return true;
    //    }
    //    if (Physics.Raycast(center, direction, 1f) && raycastHit.collider.CompareTag("Push"))
    //    {
    //        return false;
    //    }
    //}

    private void CheckFirst(Vector3 center, Vector3 direction)
    {
        if (Physics.Raycast(center, direction, out RaycastHit raycastHit, 1f))
        {
            if (raycastHit.collider.CompareTag("Push"))
            {
                Debug.Log("oldu mu");
                CheckTwo(raycastHit.collider.transform.position, Vector3.right);
                Debug.Log(raycastHit.collider.name);
            }
            if (raycastHit.collider.CompareTag("Wall"))
            {
                Debug.Log("Lan oldu mu");
                isCube = true;
                isCubeBackEmpty = false;
            }
        }
    }
    private void CheckTwo(Vector3 cubeCenter, Vector3 cubeDirection)
    {
        if (Physics.Raycast(cubeCenter, cubeDirection, out RaycastHit raycastHit, 1f))
        {
            Debug.Log("aþama 2");
            if (raycastHit.collider.CompareTag("Wall"))
            {
                isCubeFrontEmpty = false;
                Debug.Log("aþama 2");
            }
            else
            {
                isCubeFrontEmpty = true;
            }

        }

    }




    private void CheckLeft(Vector3 pos)
    {
        CheckFirst(pos, Vector3.left);
    }
    private void CheckRight(Vector3 pos)
    {
        CheckFirst(pos, Vector3.right);
    }

    private void CheckBack(Vector3 pos)
    {
        CheckFirst(pos, Vector3.back);
    }

    private void CheckForward(Vector3 pos)
    {
        CheckFirst(pos, Vector3.forward);
    }

    private void Move()
    {
        if (moveTimerCounter < moveTimerDuration)
        {
            moveTimerCounter += Time.deltaTime;
            transform.position = Vector3.Lerp(moveStartPos, moveEndPos, moveTimerCounter / moveTimerDuration);
        }

        else
        {
            moveTimer = false;
            moveTimerCounter = 0;
            transform.position = moveEndPos;
        }
    }

    private void SetMove(Vector3 endPos)
    {
        moveStartPos = transform.position;
        moveEndPos = endPos;
        moveTimer = true;
    }
}