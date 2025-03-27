using UnityEngine;

public class ServerRoom : MonoBehaviour
{
    [SerializeField] private RectTransform redWire;
    [SerializeField] private RectTransform blueWire;
    [SerializeField] private RectTransform yellowWire;

    [SerializeField] private RectTransform redWireDestination;
    [SerializeField] private RectTransform blueWireDestination;
    [SerializeField] private RectTransform yellowWireDestination;

    private RectTransform selectedWire = null;
    private Vector2 startPoint;

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
        selectedWire = wire;
        startPoint = wire.position;
    }

    void StretchWire(RectTransform wire, Vector2 start, Vector2 end)
    {
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);

        wire.pivot = new Vector2(0, 0.5f); // 왼쪽 끝을 고정
        wire.position = start; // 왼쪽 끝은 고정
        wire.sizeDelta = new Vector2(distance, wire.sizeDelta.y); // 오른쪽으로 길이 조정
        wire.right = direction; // 방향 회전
    }

    void CheckConnection(RectTransform wire)
    {
        RectTransform correctDestination = null;

        if (wire == redWire) correctDestination = redWireDestination;
        else if (wire == blueWire) correctDestination = blueWireDestination;
        else if (wire == yellowWire) correctDestination = yellowWireDestination;

        if (correctDestination != null)
        {
            
        }
    }

    bool IsMouseOverWire(RectTransform wire, Vector2 mousePos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(wire, mousePos);
    }
}
