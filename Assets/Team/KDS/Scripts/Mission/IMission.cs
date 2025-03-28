using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMission
{
    /// <summary>
    /// 게임종료 밑 초기화
    /// </summary>
    public void GameEnd();



    /// <summary>
    ///  retrunScore해주세요
    /// </summary>
    /// <returns></returns>
    public int GetScore();


    /// <summary>
    /// 스트레스 반환해주세요
    /// 예시코드입니다
    //float stress = 0;
    //if (score > 0) { stress = 5f; }
    //else { stress = 10f; }
    //return stress;
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public float GetStress();
}
