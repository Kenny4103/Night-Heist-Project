using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// This is the script for Player movement in the Hubworld
// It's separate from the PlayerManager script
public class HubworldMovement : MonoBehaviour
{
    /*** References ***/
    private Rigidbody2D _rb;
    private Animator _animator;
    private InputManager _input;

    /*** Class Variables ***/
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _movement;

    // String Constants
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lasthorizontal = "LastHorizontal";
    private const string _lastvertical = "LastVertical";

    /*** Methods ***/
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
    }

    private void Update()
    {
        // Sets the thief's velocity according to movement value
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.velocity = _movement * _moveSpeed;

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        // Updates the Thief's Animator parameters according to left/right & up/down movement
        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lasthorizontal, _movement.x);
            _animator.SetFloat(_lastvertical, _movement.y);
        }
    }

}