using UnityEngine;
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

    public int completedConnections { get; private set; } = 0;
    private List<RectTransform> completedWires = new List<RectTransform>(); // 이미 연결된 전선들

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;

            if (IsMouseOverWire(redWire, mousePos)) SelectWire(redWire);
            else if (IsMouseOverWire(blueWire, mousePos)) SelectWire(blueWire);
            else if (IsMouseOverWire(yellowWire, mousePos)) SelectWire(yellowWire);
        }

        if (selectedWire != null && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            StretchWire(selectedWire, startPoint, mousePos);
        }

        if (Input.GetMouseButtonUp(0) && selectedWire != null)
        {
            CheckConnection(selectedWire);
            selectedWire = null;
        }
    }

    void SelectWire(RectTransform wire)
    {
        if (completedWires.Contains(wire)) return; // 이미 연결된 전선은 선택 불가

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

            if (distance < 80f) // 연결 성공
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
}
