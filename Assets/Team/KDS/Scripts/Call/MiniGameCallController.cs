using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCallController : MonoBehaviour ,IMission
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
        ProgressToScore();
        Destroy(lineRenderer);
    }

    private void ProgressToScore()
    {
        if (progerss > 730) { score = 5; }
        else if (progerss > 650) { score = 3; }
        else if (progerss > 600) { score = 1; }
        else { score = 0; }//실패
    }
    public void GameEnd() { }
    public int GetScore() { return score; }    
    public float GetStress()
    {
        float stress = 0;
        if (score > 0) { stress = 5f; }
        else { stress = 10f; }
        return stress;
    }
}
