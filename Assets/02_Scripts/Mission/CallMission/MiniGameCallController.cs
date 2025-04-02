using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameCallController : Mission
{
    public CircleController cirele;
  
    public float progerss;

    public GameObject lineRenderer;

    public GameObject result;
    public GameObject scorego;
    public TextMeshPro resultText;
    public TextMeshPro scoreText;

    public AudioClip clip;
    public void Start()
    {
        lineRenderer=Instantiate(lineRenderer, Vector3.zero, Quaternion.identity);
 
    }
    /// <summary>
    /// 게임이 시작되면 사운드
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlayClip(clip);
    }
    /// <summary>
    /// 선이 종점에 부딪히면 점수계산
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == lineRenderer.gameObject)
        {
            progerss = cirele.GetProgerss();
            ProgressToScore();
            Destroy(lineRenderer);
            result.SetActive(true);
            if (score > 0)
            {
                scorego.SetActive(true);
                resultText.text = "성공!";
                scoreText.text=score.ToString();
            }
            else
            {
                resultText.text = "실패";
            }
            Invoke("GameEnd", 1);
        }
    }
    /// <summary>
    /// ProgressToScore, 점수로 변환
    /// </summary>
    private void ProgressToScore()
    {
        if (progerss > 330) { score = 5; }
        else if (progerss > 315) { score = 3; }
        else if (progerss > 300) { score = 1; }
        else { score = 0; }//실패
    }
}
