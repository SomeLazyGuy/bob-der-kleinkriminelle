using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private InputAction playerControls;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rigidbody2D;
    private Vector2 moveDirection = Vector2.zero;
    
    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        moveDirection = playerControls.ReadValue<Vector2>();
    }

    private void FixedUpdate() {
        rigidbody2D.velocity = moveDirection * moveSpeed;
    }
}
