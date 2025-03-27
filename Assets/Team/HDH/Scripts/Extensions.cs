using System;
using UnityEngine;

internal static class Extensions
{
    public static string FormatTime(this float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    public static string FormatTime2(this float time)
    {
        // time을 초로 가정하고 DateTime으로 변환합니다.
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}.{timeSpan.Milliseconds / 10:00}"; // 밀리초 두 자리
    }
}
