using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variable    
    [SerializeField] private float _move = 1f;
    [SerializeField] private Transform _target;
    public bool isBackEmpty;
    public bool isLeftEmpty;
    public bool isFrontEmpty;
    public bool isRightEmpty;
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
    }

    private void Update()
    {
        if (!moveTimer)
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
            // transform.position = targetPos;
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

    private void DirectionFoward()
    {
        Vector3 playerPosition = transform.position;
        Vector3 fowardDirection = Vector3.forward;
        Ray theRay = new Ray(playerPosition, fowardDirection);
        if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
        {
            Debug.Log(rayHit.collider.name);
            if (rayHit.collider.gameObject.tag == "Wall")
            {
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
        Vector3 backwardDirection = Vector3.back;
        Ray theRay = new Ray(playerPosition, backwardDirection);

        if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
        {
            if (rayHit.collider.gameObject.tag == "Wall")
            {
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
        Vector3 rightDirection = Vector3.right;
        Ray theRay = new Ray(playerPosition, rightDirection);

        if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
        {
            if (rayHit.collider.gameObject.tag == "Wall")
            {
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
        Vector3 leftDirection = Vector3.left;
        Ray theRay = new Ray(playerPosition, leftDirection);

        if (Physics.Raycast(theRay, out RaycastHit rayHit, 3f))
        {
            if (rayHit.collider.gameObject.tag == "Wall")
            {
                isLeftEmpty = false;
            }
        }
        else
        {
            isLeftEmpty = true;
        }
    }


    private void CheckAround(Vector3 pos)
    {
        CheckBack(pos);
        CheckRight(pos);
        CheckLeft(pos);
        CheckForward(pos);
    }
    private bool CheckDirection(Vector3 center, Vector3 direction)
    {
        return Physics.Raycast(center, direction, out RaycastHit raycastHit, 1f) && raycastHit.collider.CompareTag("Wall");
    }



    private void CheckLeft(Vector3 pos)
    {
        isLeftEmpty = !CheckDirection(pos, Vector3.left);
    }
    private void CheckRight(Vector3 pos)
    {
        isRightEmpty = !CheckDirection(pos, Vector3.right);
    }

    private void CheckBack(Vector3 pos)
    {
        isBackEmpty = !CheckDirection(pos, Vector3.back);
    }

    private void CheckForward(Vector3 pos)
    {
        isFrontEmpty = !CheckDirection(pos, Vector3.forward);
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