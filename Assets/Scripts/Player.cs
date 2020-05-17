using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private float _jumpSpeed = 3.5f;
    [SerializeField]
    private float _doubleJumpMultiplier = 0.5f;

    private CharacterController _controller;

    private float _directionY;

    private bool _canDoubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        if (_controller.isGrounded)
        {
            _canDoubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                _directionY = _jumpSpeed;
            }
        } else {
            if(Input.GetButtonDown("Jump") && _canDoubleJump) {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _canDoubleJump = false;
            }
        }

        _directionY -= _gravity * Time.deltaTime;

        direction.y = _directionY;

        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }
}
