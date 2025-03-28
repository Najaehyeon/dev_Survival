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

    MissionTimer MissionTimer;
    bool isTriggerOn = false;
    bool isGaming = false;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void FixedUpdate()
    {
        Move();
        Debug.Log("게임중"+isGaming);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isGaming)
        {
            movementDirection = context.ReadValue<Vector2>();
            //Debug.Log(movementDirection);
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

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isTriggerOn)
        {
            //Debug.Log("상호작용");
            MissionTimer.OnGameStart();
            isGaming = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggerOn = true;
        MissionTimer = collision.gameObject.GetComponent<MissionTimer>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerOn = false;
    }
}
