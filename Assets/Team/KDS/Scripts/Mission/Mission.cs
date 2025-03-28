using UnityEngine;


public abstract class Mission : MonoBehaviour
{
    [Header("GameScore")]
    protected int score;
    protected float stress;

    public bool isGameEnd;
    public virtual int GetScroe()
    {
        Debug.Log(score);
        return score;
    }

    public virtual float GetStress()
    {
  
        float stress = 0;
        if (score > 0) { stress = 5f; }
        else { stress = 10f; }

        Debug.Log(stress);
        return stress;
    }
    public virtual void GameEnd()
    {
        //게임매니저랑 플레이어랑 연동해서 점수랑 스트레스 변환
        GetScroe();
        GetStress();
        isGameEnd = true;
        MissionManager.Instance.controller.IsAllGameEnd();
        Destroy(gameObject);
    }
}