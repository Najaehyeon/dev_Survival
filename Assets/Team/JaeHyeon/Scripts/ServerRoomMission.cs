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

    public int completedConnections { get; private set; } = 0;
    private List<RectTransform> completedWires = new List<RectTransform>(); // 이미 연결된 전선들

    private bool isDragging = false;

    // 게임 시작 시 와이어 좌표 출력
    void Start()
    {
        LogWirePositions();
    }

    // 게임 시작 시 와이어들의 좌표를 찍어주는 함수
    void LogWirePositions()
    {
        Debug.Log($"빨간 와이어: {redWire.position}");
        Debug.Log($"파란 와이어: {blueWire.position}");
        Debug.Log($"노란 와이어: {yellowWire.position}");
    }

    void MouseDrag()
    {
        if (selectedWire != null && isDragging)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            StretchWire(selectedWire, startPoint, worldMousePosition);
            Debug.Log("와이어 선택됨");
        }
    }

    void MouseUp()
    {
        if (selectedWire != null)
        {
            CheckConnection(selectedWire);
            selectedWire = null;
            isDragging = false;  // 드래그 종료
        }
    }

    void SelectWire(RectTransform wire)
    {
        if (completedWires.Contains(wire)) return; // 이미 연결된 전선은 선택 불가

        selectedWire = wire;
        startPoint = wire.position;
        isDragging = true;  // 드래그 시작
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

            if (distance < 3f) // 연결 성공
            {
                Debug.Log("연결됨");
                StretchWire(wire, startPoint, correctDestination.position);
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
    }

    bool IsMouseOverWire(RectTransform wire, Vector2 mousePos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(wire, mousePos);
    }

    public void MouseInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // 마우스 좌표 출력
            Debug.Log("마우스 좌표 (스크린 좌표): " + mousePosition);
            Debug.Log("마우스 좌표 (월드 좌표): " + worldMousePosition);

            if (IsMouseOverWire(redWire, worldMousePosition)) SelectWire(redWire);
            else if (IsMouseOverWire(blueWire, worldMousePosition)) SelectWire(blueWire);
            else if (IsMouseOverWire(yellowWire, worldMousePosition)) SelectWire(yellowWire);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            MouseUp();
        }
    }

    public void MouseDragInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Vector2 mousePos = context.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            MouseDrag();
        }
    }

    //void Update()
    //{
    //    // 드래그 중일 때 계속 MouseDrag() 호출
    //    if (isDragging)
    //    {
    //        MouseDrag();
    //    }
    //}
}
