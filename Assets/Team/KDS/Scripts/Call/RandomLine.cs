using UnityEngine;

public class RandomLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    public int numberOfPoints = 13;  // ���� ����  
    public float width = 0.1f;  // ���� �β�  
    public float amplitude = 3f;  
    public float frequency = 1f; 
    public int interpolationPoints = 10;
    Vector2[] temparry;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        lineRenderer.positionCount = (numberOfPoints - 1) * interpolationPoints + 1;

        Vector3[] points = new Vector3[numberOfPoints];
        for (int i =0; i < numberOfPoints; i++)
        {
            float x = i * 2f;  // x ��ǥ

            float randomAmplitude = Random.Range(1f, amplitude); 
            float y = Mathf.Sin(x * frequency) * randomAmplitude;  
            points[i] = new Vector3(x, y, 0);
        }

        // �� �� ���̸� �ε巴�� �����ϴ� ������ ���� ���
        Vector3[] smoothPoints = GenerateSmoothPoints(points);

        // �ε巴�� ����� ���� LineRenderer�� ����
        lineRenderer.SetPositions(smoothPoints);

        edgeCollider.points = new Vector2[smoothPoints.Length];
        temparry = edgeCollider.points;
        for (int i = 0; i < smoothPoints.Length; i++)
        {
            temparry[i] = new Vector2(smoothPoints[i].x, smoothPoints[i].y);
        }
        edgeCollider.points= temparry;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    void FixedUpdate()
    {
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 point = lineRenderer.GetPosition(i);  
            point.x -= 0.05f;  
            lineRenderer.SetPosition(i, point);  
        }
        transform.position -= new Vector3(0.05f, 0, 0);
    }
    // Catmull-Rom �������� �̿��Ͽ� ������ �ε巴�� ����
    Vector3[] GenerateSmoothPoints(Vector3[] points)
    {
        int totalPoints = (points.Length - 1) * interpolationPoints + 1; 
        Vector3[] smoothPoints = new Vector3[totalPoints];

        for (int i = 0; i < points.Length - 1; i++)  
        {
            Vector3 p0 = i > 0 ? points[i - 1] : points[i];  
            Vector3 p1 = points[i];  
            Vector3 p2 = points[i + 1];  
            Vector3 p3 = i + 2 < points.Length ? points[i + 2] : points[i + 1];  

            // �� �� ���̸� �ε巴�� �����ϱ� ���� Catmull-Rom ������ ���
            for (int j = 0; j < interpolationPoints; j++)
            {
                float t = (float)j / interpolationPoints;
                smoothPoints[i * interpolationPoints + j] = CatmullRom(p0, p1, p2, p3, t);
            }
        }
        smoothPoints[smoothPoints.Length - 1] = points[points.Length - 1];

        return smoothPoints;
    }

    // Catmull-Rom ���� �Լ�
    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float x = 0.5f * ((2f * p1.x) + (-p0.x + p2.x) * t + (2f * p0.x - 5f * p1.x + 4f * p2.x - p3.x) * t2 + (-p0.x + 3f * p1.x - 3f * p2.x + p3.x) * t3);
        float y = 0.5f * ((2f * p1.y) + (-p0.y + p2.y) * t + (2f * p0.y - 5f * p1.y + 4f * p2.y - p3.y) * t2 + (-p0.y + 3f * p1.y - 3f * p2.y + p3.y) * t3);

        return new Vector3(x, y, 0);
    }
}

