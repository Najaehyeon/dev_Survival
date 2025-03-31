using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCallController : Mission
{
    public CircleController cirele;
  
    public float progerss;

    public GameObject lineRenderer;

    public void Start()
    {
        lineRenderer=Instantiate(lineRenderer, Vector3.zero, Quaternion.identity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == lineRenderer.gameObject)
        {
            progerss = cirele.GetProgerss();
            ProgressToScore();
            Destroy(lineRenderer);

            GameEnd();
        }
    }

    private void ProgressToScore()
    {
        if (progerss > 330) { score = 5; }
        else if (progerss > 315) { score = 3; }
        else if (progerss > 300) { score = 1; }
        else { score = 0; }//실패
    }
}
