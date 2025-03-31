using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ServerRoomMission : MonoBehaviour
{
    [SerializeField] private RectTransform redWire;
    [SerializeField] private RectTransform blueWire;
    [SerializeField] private RectTransform yellowWire;

    [SerializeField] private RectTransform redWireDestination;
    [SerializeField] private RectTransform blueWireDestination;
    [SerializeField] private RectTransform yellowWireDestination;

    private RectTransform selectedWire = null;
    private Vector2 startPoint;
    private Vector2 mousePosition;

    public int completedConnections { get; private set; } = 0;
    private List<RectTransform> completedWires = new List<RectTransform>();

    private void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
    }

    public void OnMouseDown()
    {
        if (IsMouseOverWire(redWire, mousePosition)) SelectWire(redWire);
        else if (IsMouseOverWire(blueWire, mousePosition)) SelectWire(blueWire);
        else if (IsMouseOverWire(yellowWire, mousePosition)) SelectWire(yellowWire);
    }

    public void OnMouseDrag()
    {
        if (selectedWire != null)
        {
            StretchWire(selectedWire, startPoint, mousePosition);
        }
    }

    public void OnMouseUp()
    {
        if (selectedWire != null)
        {
            CheckConnection(selectedWire);
            selectedWire = null;
        }
    }

    void SelectWire(RectTransform wire)
    {
        if (completedWires.Contains(wire)) return;

        selectedWire = wire;
        startPoint = wire.position;
    }

    void StretchWire(RectTransform wire, Vector2 start, Vector2 end)
    {
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);

        wire.pivot = new Vector2(0, 0.5f);
        wire.position = start;
        wire.sizeDelta = new Vector2(distance, wire.sizeDelta.y);
        wire.right = direction;
    }

    void CheckConnection(RectTransform wire)
    {
        RectTransform correctDestination = null;

        if (wire == redWire) correctDestination = redWireDestination;
        else if (wire == blueWire) correctDestination = blueWireDestination;
        else if (wire == yellowWire) correctDestination = yellowWireDestination;

        if (correctDestination != null)
        {
            Vector2 wireEndPoint = wire.position + (Vector3)(wire.right * wire.sizeDelta.x);
            float distance = Vector2.Distance(wireEndPoint, correctDestination.position);

            if (distance < 80f)
            {
                Debug.Log("연결됨");
                StretchWire(wire, startPoint, correctDestination.position);
                completedConnections++;
                completedWires.Add(wire);
            }
            else
            {
                ResetWire(wire);
            }
        }
    }

    void ResetWire(RectTransform wire)
    {
        wire.pivot = new Vector2(0, 0.5f);
        wire.position = startPoint;
        wire.sizeDelta = new Vector2(100f, wire.sizeDelta.y);
        wire.right = Vector2.right;
    }

    bool IsMouseOverWire(RectTransform wire, Vector2 mousePos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(wire, mousePos);
    }

    public void InputMouseDown(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            OnMouseDown();
        }
    }

    public void InputMouseDrag(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnMouseDrag();
        }
    }

    public void InputMouseUp(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Canceled)
        {
            OnMouseUp();
        }
    }
}
