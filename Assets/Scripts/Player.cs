using System;
using Core.Utility;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject lantern;
    [SerializeField] private Transform _cam;
    [SerializeField] private float _moveSpeed = 0.1f;
    [SerializeField] private float _senseX = 1f;
    [SerializeField] private float _senseY = 1f;
    [SerializeField] private float _minY = -60f;
    [SerializeField] private float _maxY = 60f;
    [SerializeField] private float _jumpHeight = 4f;
    private float _rotationY;
    private CharacterController _ch;

    private void Awake()
    {
        _ch = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            lantern.Switch();
        }
    }

    private void Move()
    {
        var moveH = Input.GetAxis("Horizontal");
        var moveV = Input.GetAxis("Vertical");
        var moveVector = transform.TransformDirection(new Vector3(moveH, 0, moveV)) * _moveSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && _ch.isGrounded)
        {
            moveVector.y = _jumpHeight;
        }
        moveVector.y += Physics.gravity.y * Time.deltaTime;
        _ch.Move(moveVector);
        
        float x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _senseX;
        _rotationY += Input.GetAxis("Mouse Y") * _senseY;
        _rotationY = Mathf.Clamp(_rotationY, _minY, _maxY);
        _cam.transform.localEulerAngles = new Vector3(-_rotationY, 0f, 0f);
        transform.localEulerAngles = new Vector3(0f, x, 0f);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}

