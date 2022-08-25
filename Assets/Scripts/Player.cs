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
    [SerializeField] private float _range = 5f;

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
        Vector3 direction = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(direction * _range));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * _range));

        if (Physics.Raycast(theRay, out RaycastHit hit, _range))
        {
            if (hit.collider.tag == "Obstacle")
            {
                Debug.Log("it's Kirmizi");
            }
        }

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