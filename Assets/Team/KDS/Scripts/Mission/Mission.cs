using UnityEngine;


public abstract class Mission : MonoBehaviour
{
    [Header("GameScore")]
    protected int score;
    protected float stress;

    public bool isGameEnd;

    //NPC정보 가지고있는애로 변경
    public Employee? target;
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

    /// <summary>
    /// 게임매니저랑 플레이어랑 연동해서 점수랑 스트레스 변환
    /// 미션 종료시 호출
    /// </summary>
    public virtual void GameEnd()
    {
        GameManager.Instance.ChangeScore(GetScroe());
        GameManager.Instance.ChangeStress((int)GetStress());
        isGameEnd = true;
        MissionManager.Instance.controller.IsAllGameEnd();
        Destroy(gameObject);
        GameManager.Instance.isMissionInProgress = false;
    }

    /// <summary>
    /// NPC가 미션클리어했을때 호출
    /// </summary>
    /// <param name="interect"></param>
    public void NPCInterection(Employee interect)
    {
        if(interect==target)
        {
            //target의 스테이터스에따라 스코어랑 스트레스 반환
            MissionManager.Instance.controller.IsAllGameEnd();
            Debug.Log("NPC와 소통");
        }
    }
}