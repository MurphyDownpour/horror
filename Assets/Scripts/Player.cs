using System;
using Core.Utility;
using UnityEngine;
using UnityEngine.UI;

enum PickableMode
{
    Enable,
    Disable
}

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private GameObject _pickIcon;
    [SerializeField] private float _maxInteractableDistance;
    
    [SerializeField] private GameObject lantern;
    [SerializeField] private Camera _cam;
    [SerializeField] private float _moveSpeed = 5f;
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
        LookAround();

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

    private void LookAround()
    {
        var ray = _cam.ScreenPointToRay(_crosshair.transform.position);

        if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo, _maxInteractableDistance))
        {
            var target = hitInfo.transform.gameObject;

            if (target.CompareTag(Constants.PickableTag))
            {
                SetPickableMode(PickableMode.Enable);
                
                if (Input.GetMouseButtonDown(0))
                {
                    PickItem(target);
                }
            }
        }
        else
        {
            SetPickableMode(PickableMode.Disable);
        }
    }

    private void PickItem(GameObject target)
    {
        Destroy(target);
    }

    private void SetPickableMode(PickableMode mode)
    {
        switch (mode)
        {
            case PickableMode.Enable:
                _pickIcon.SetActive(true);
                _crosshair.SetActive(false);
                break;
            case PickableMode.Disable:
                _pickIcon.SetActive(false);
                _crosshair.SetActive(true);
                break;
        }
    }
}

