using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCallController : MonoBehaviour
{
    [Header("GameScore")]
    public float progerss;
    public int score;

    [Header("GameEntity")]
    public GameObject gameend;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("게임종료");
    }
}
