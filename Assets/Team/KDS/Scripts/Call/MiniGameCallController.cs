using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCallController : MonoBehaviour
{
    public CircleController cirele;
    [Header("GameScore")]
    public float progerss;
    public int score;

    public GameObject lineRenderer;

    public void Start()
    {
        lineRenderer=Instantiate(lineRenderer, Vector3.zero, Quaternion.identity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        progerss = cirele.GetProgerss();
        Destroy(lineRenderer);
    }

    private void ProgressToScore()
    {
        if (progerss > 730) { score = 3; }
        else if (progerss > 650) { score = 2; }
        else if (progerss > 600) { score = 1; }
        else { score = 0; }//½ÇÆÐ
    }
}
