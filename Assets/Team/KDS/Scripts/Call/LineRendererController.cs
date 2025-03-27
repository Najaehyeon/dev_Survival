using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        // ���� ������Ʈ �̵�
        transform.position -= new Vector3(0.05f, 0, 0);
        edgeCollider.transform.position = lineRenderer.transform.position;
    }
}