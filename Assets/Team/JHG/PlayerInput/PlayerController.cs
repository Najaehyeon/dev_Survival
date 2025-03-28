using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected AnimationHandler animationHandler;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Vector2 movementDirection;
    private Vector2 lookDirection;

    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            movementDirection = context.ReadValue<Vector2>();
            Debug.Log(movementDirection);
            animationHandler.IsMove(movementDirection);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            animationHandler.IsIdle();
            movementDirection = Vector2.zero;  
        }
    }

    private void Move()
    {
        Vector2 dir = transform.up * movementDirection.y + transform.right * movementDirection.x;
        dir *= moveSpeed;

        _rigidbody.velocity = dir;
    }
}
