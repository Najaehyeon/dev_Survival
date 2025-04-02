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
    float footStepTime = 0;
    float footstepRate = 0.5f;

    Rigidbody2D _rigidbody;

    MissionTimer MissionTimer;
    CoffeeMachine coffeeMachin;
    bool isTriggerOn = false;
    bool isGaming = false;

    public AudioClip moveClip;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void FixedUpdate()
    {
        Move();
        isGaming = GameManager.Instance.isMissionInProgress;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !isGaming)
        {
            movementDirection = context.ReadValue<Vector2>();
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

        
        if (moveClip != null && movementDirection != Vector2.zero)
        {
            if (Mathf.Abs(_rigidbody.velocity.y) > 0.1f || Mathf.Abs(_rigidbody.velocity.x) > 0.1f)
            {
                if (Time.time - footStepTime > footstepRate)
                {
                    footStepTime = Time.time;
                    SoundManager.Instance.PlayClip(moveClip);
                }
            }
        }
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isTriggerOn)
        {
            if (MissionTimer != null) MissionTimer.OnGameStart();
            if (coffeeMachin != null && coffeeMachin.isUse) coffeeMachin.DownStress();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggerOn = true;
        if (collision.tag == "Mission")
        {
            MissionTimer = collision.gameObject.GetComponent<MissionTimer>();
        }
        else if (collision.tag == "Coffee")
        {
            coffeeMachin = collision.gameObject.GetComponent<CoffeeMachine>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerOn = false;
        MissionTimer = null;
        coffeeMachin = null;
    }
}
