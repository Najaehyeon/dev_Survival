
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    private float maxHeight=3f;
    private float minHeight=-3f;
    private bool isMouseDown = false;
    private bool isTrigger = false;
    public float MaxSpeed = 0.08f;

    public float progress;

    private void FixedUpdate()
    {

        if (Mouse.current.leftButton.isPressed)
        {
            if (!isMouseDown)
            {
                isMouseDown = true;
            }

            OnMouseHold();
        }
        else
        {
            if (isMouseDown)
            {
                isMouseDown = false;
            }
            OnMouseUnHold();
        }


        if (isTrigger)
            progress += 1;
    }
    private void OnMouseHold()
    {
        if (transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, MaxSpeed, 0);
        }
    }
    private void OnMouseUnHold()
    {
        if (transform.position.y > minHeight)
        {
            transform.position -= new Vector3(0, MaxSpeed, 0);
        }
    }

    public float GetProgerss()
    {
        return progress;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
    }
}

