
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CircleController : MonoBehaviour
{
    private float maxHeight=3f;
    private float minHeight=-3f;
    public float moveDuration = 2f;
    private bool isMouseDown = false;


    private void Update()
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
    }
    private void OnMouseHold()
    {
        if (transform.position.y < maxHeight)
        {
            transform.position += new Vector3(0, 0.01f, 0);
        }
    }
    private void OnMouseUnHold()
    {
        if (transform.position.y > minHeight)
        {
            transform.position -= new Vector3(0, 0.01f, 0);
        }
    }
}

