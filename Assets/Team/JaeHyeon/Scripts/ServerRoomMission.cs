using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

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
    private Vector2 mousePos;

    public int completedConnections { get; private set; } = 0;
    private List<RectTransform> completedWires = new List<RectTransform>(); // 이미 연결된 전선들

    void SelectWire(RectTransform wire)
    {
        if (completedWires.Contains(wire)) return; // 이미 연결된 전선은 선택 불가

        selectedWire = wire;
        startPoint = wire.position;
        Debug.Log("와이어 선택됨");
    }

    void StretchWire(RectTransform wire, Vector3 start, Vector2 screenMousePosition)
    {
        Vector3 end = Camera.main.ScreenToWorldPoint(screenMousePosition);
        start.z = 0;
        end.z = 0;
        Debug.Log(start);
        Debug.Log(end);
        Debug.Log(screenMousePosition);
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, screenMousePosition);

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

            if (distance < 3f) // 연결 성공
            {
                Debug.Log("연결됨");
                Vector2 end = Camera.main.WorldToScreenPoint(correctDestination.position);
                StretchWire(wire, startPoint, end);
                completedConnections++;

                completedWires.Add(wire); // 연결된 전선 목록에 추가
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
        Debug.Log("와이어 되돌아감");
    }


    bool IsMouseOverWire(RectTransform wire, Vector2 mousePos)
    {
        // Screen Space - Camera에서 마우스 좌표를 UI의 RectTransform에 맞게 처리합니다.
        return RectTransformUtility.RectangleContainsScreenPoint(wire, mousePos, Camera.main);
    }

    public void MouseInput(InputAction.CallbackContext context)
    {
        // 클릭될 때 마우스 좌표를 로그로 출력
        Vector2 screenMousePosition = mousePos;  // MouseDrag에서 업데이트된 값 사용

        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("클릭 시작, 마우스 좌표: " + screenMousePosition);
            Debug.Log(redWire.position);
            if (IsMouseOverWire(redWire, screenMousePosition)) SelectWire(redWire);
            else if (IsMouseOverWire(blueWire, screenMousePosition)) SelectWire(blueWire);
            else if (IsMouseOverWire(yellowWire, screenMousePosition)) SelectWire(yellowWire);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            if (selectedWire != null)
            {
                CheckConnection(selectedWire);
                selectedWire = null;
            }
        }
    }

    public void MouseDrag(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();  // 마우스 위치 업데이트

        if (selectedWire != null)
        {
            // 마우스 좌표를 바탕으로 와이어 늘리기
            Vector2 screenMousePosition = mousePos;
            StretchWire(selectedWire, startPoint, screenMousePosition);
        }
    }
}
