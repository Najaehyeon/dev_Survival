using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCallController : MonoBehaviour
{
    public CircleController cirele;
    [Header("GameScore")]
    public float progerss;
    public int score;

    [Header("GameEntity")]
    public GameObject gameend;


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("��������");
        progerss = cirele.GetProgerss();
        Debug.Log($"{progerss}");
    }
}
