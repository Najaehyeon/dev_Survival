using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineLoop : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public double startTime = 1.5; // 루프 시작 시간 (초 단위)
    public double endTime = 5.0;   // 루프 종료 시간 (초 단위)

    void Update()
    {
        // 타임라인이 endTime을 넘어서면 startTime으로 돌아가서 루프를 시작
        if (playableDirector.time >= endTime)
        {
            playableDirector.time = startTime;
            playableDirector.Evaluate(); // 바로 적용
        }
    }
}
