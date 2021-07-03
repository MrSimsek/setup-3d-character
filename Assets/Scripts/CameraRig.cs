using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField]
    private bool _lockCursor = true;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _distanceFromTarget = 4f;
    [SerializeField]
    private float _mouseSensivity = 1.0f;
    [SerializeField]
    private Vector2 _picthMinMax = new Vector2(-20, 85);
    [SerializeField]
    private float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;

    private Vector3 _offset;

    private float _yaw;
    private float _pitch;

    private Vector3 currentRotation;

    void Start()
    {
        _offset = transform.position - _target.position;

        if (_lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        _yaw += Input.GetAxis("Mouse X") * _mouseSensivity;
        _pitch -= Input.GetAxis("Mouse Y") * _mouseSensivity;
        _pitch = Mathf.Clamp(_pitch, _picthMinMax.x, _picthMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(_pitch, _yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        // This line makes a fun camera movement
        // transform.position = _target.position + _offset - transform.forward * _distanceFromTarget;

        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }
}
