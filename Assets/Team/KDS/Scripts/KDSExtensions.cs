using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace KDSExtensions
{
    public static class KDSExtension
    {
        // === String Extensions ===
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        // === Vector3 Extensions ===
        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        // === GameObject Extensions ===
        public static bool IsNullOrDestroyed(this GameObject go)
        {
            return go == null || go.Equals(null);
        }

        // === List Extensions ===
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        // === UI Move Using DoTween ===
        public static void MovingUI(GameObject gameobject, float x, float y, float duration)
        {

            RectTransform rect = gameobject.GetComponent<RectTransform>();
            Vector2 targetPos = rect.anchoredPosition + new Vector2(x, y);
            rect.DOAnchorPos(targetPos, duration)
                .SetEase(Ease.OutCubic);
        }
    }
}
