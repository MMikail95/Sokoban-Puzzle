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
        CheckCubeSides(transform.position);
        isBackEmpty = true;
        isFrontEmpty = true;
        isRightEmpty = true;
        isLeftEmpty = true;
        isCube = false;
        isCubeBackEmpty = true;
        isCubeFrontEmpty = true;
        isCubeLeftEmpty = true;
        isCubeRightEmpty = true;
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
            CheckCubeSides();
            SetMove(targetPos);
        }

        if (isRightEmpty && Input.GetKeyUp(KeyCode.RightArrow))
        {
            Vector3 targetPos = transform.position + Vector3.right * _move;
            CheckAround(targetPos);
            CheckCubeSides(transform.position);
            SetMove(targetPos);
        }

        if (isFrontEmpty && isCubeFrontEmpty && Input.GetKeyUp(KeyCode.UpArrow))
        {
            Vector3 targetPos = transform.position + Vector3.forward * _move;
            CheckAround(targetPos);
            CheckCubeSides(transform.position);
            SetMove(targetPos);
        }

        if (isBackEmpty && Input.GetKeyUp(KeyCode.DownArrow))
        {
            Vector3 targetPos = transform.position + Vector3.back * _move;
            CheckAround(targetPos);
            CheckCubeSides(transform.position);
            SetMove(targetPos);
        }
    }

    private void CheckAround(Vector3 pos)
    {
        CheckLeft(pos);
        CheckRight(pos);
        CheckForward(pos);
        CheckBack(pos);
    }

    private void CheckCubeSides(Vector3 pos)
    {
        CheckCubeLeft(pos);
        CheckCubeRight(pos);
        CheckCubeFoward(pos);
        CheckCubeBack(pos);
    }

    private void CheckFirst(Vector3 center, Vector3 direction)
    {
        if (Physics.Raycast(center, direction, out RaycastHit raycastHit, 1f))
        {

            if (raycastHit.collider.CompareTag("Push"))
            {
                isCube = true;
                Debug.DrawRay(center, direction, Color.white, 10f);
                Debug.Log("KÜP VAR");
                if (isCube == true)
                {
                    CheckTwo(raycastHit.collider.transform.position, direction);                    
                }              
            }
            else if (raycastHit.collider.CompareTag("Wall"))
            {
                Debug.Log("DUVAR VAR");
            }
        }
    }
    private void CheckTwo(Vector3 cubeCenter, Vector3 cubeDirection)
    {
        if (Physics.Raycast(cubeCenter, cubeDirection, out RaycastHit raycastHit, 1f))
        {
            if (raycastHit.collider.tag == ("Wall"))
            {
                Debug.DrawRay(cubeCenter, cubeDirection, Color.magenta, 10f);
                isCubeFrontEmpty = false;
                Debug.Log("KÜPÜN ÖNÜNDE DUVAR VAR");

            }

            else if (raycastHit.collider.tag != ("Wall"))
            {
                Debug.DrawRay(cubeCenter, cubeDirection, Color.blue, 10f);
                isCubeFrontEmpty = true;
                isCube = false;
                Debug.Log("KÜPÜN ÖNÜDE DUVAR YOK");
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

    private void CheckCubeLeft(Vector3 pos)
    {
        CheckTwo(pos, Vector3.left);
    }

    private void CheckCubeRight(Vector3 pos)
    {
        CheckTwo(pos, Vector3.right);
    }

    private void CheckCubeFoward(Vector3 pos)
    {
        CheckTwo(pos, Vector3.forward);
    }

    private void CheckCubeBack(Vector3 pos)
    {
        CheckTwo(pos, Vector3.back);
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