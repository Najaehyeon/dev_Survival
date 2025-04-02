using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class ServerRoomMission : MonoBehaviour
{
    private RectTransform selectedWire = null;
    private Vector2 startPoint;
    private Vector2 mousePos;
    private Vector2 screenMousePosition;

    [Header("Wires")]
    [SerializeField] private RectTransform redWire;
    [SerializeField] private RectTransform blueWire;
    [SerializeField] private RectTransform yellowWire;

    [Header("Destination")]
    [SerializeField] private RectTransform redWireDestination;
    [SerializeField] private RectTransform blueWireDestination;
    [SerializeField] private RectTransform yellowWireDestination;

    public AudioClip wireAudioClip;

    public int completedConnections { get; private set; } = 0;
    private List<RectTransform> completedWires = new List<RectTransform>(); // 이미 연결된 전선들

    public ServerRoomController serverRoomController;

    void SelectWire(RectTransform wire)
    {
        if (completedWires.Contains(wire) || !serverRoomController.missionActive) return; // 이미 연결된 전선은 선택 불가

        selectedWire = wire;
        startPoint = wire.position;
    }

    void StretchWire(RectTransform wire, Vector2 start, Vector2 screenMousePosition)
    {
        Vector2 end = Camera.main.ScreenToWorldPoint(screenMousePosition);
        Vector2 direction = (end - start).normalized;
        Vector2 screenStart = RectTransformUtility.WorldToScreenPoint(Camera.main, start);
        Vector2 screenEnd = RectTransformUtility.WorldToScreenPoint(Camera.main, end);

        float distance = Vector2.Distance(screenStart, screenEnd); // 스크린 좌표에서의 거리

        wire.pivot = new Vector2(0, 0.5f);
        wire.position = start;
        wire.sizeDelta = new Vector2(distance, wire.sizeDelta.y);
        wire.right = direction;
    }

    void CheckConnection(RectTransform wire)
    {
        if (!serverRoomController.missionActive) return;

        RectTransform correctDestination = null;

        if (wire == redWire) correctDestination = redWireDestination;
        else if (wire == blueWire) correctDestination = blueWireDestination;
        else if (wire == yellowWire) correctDestination = yellowWireDestination;

        if (correctDestination != null)
        {
            Vector2 wireEndPoint = Camera.main.ScreenToWorldPoint(screenMousePosition);
            float distance = Vector2.Distance(wireEndPoint, correctDestination.position);

            if (distance < 0.4f) // 연결 성공
            {
                SoundManager.Instance.PlayClip(wireAudioClip);
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
    }


    bool IsMouseOverWire(RectTransform wire, Vector2 mousePos)
    {
        // Screen Space - Camera에서 마우스 좌표를 UI의 RectTransform에 맞게 처리
        return RectTransformUtility.RectangleContainsScreenPoint(wire, mousePos, Camera.main);
    }

    public void MouseInput(InputAction.CallbackContext context)
    {
        Vector2 screenMousePosition = mousePos;  // MouseDrag에서 업데이트된 값 사용

        if (context.phase == InputActionPhase.Started)
        {
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
            screenMousePosition = mousePos;
            StretchWire(selectedWire, startPoint, screenMousePosition);
        }
    }
}
