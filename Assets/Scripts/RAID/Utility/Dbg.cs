using System;
using System.Diagnostics;

public static class Dbg
{
    [Conditional("UNITY_EDITOR")]
    public static void Log(string message, UnityEngine.Object context = default)
    {
        UnityEngine.Debug.Log($"<color=green>{message}</color>.", context);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Log(string message)
    {
        UnityEngine.Debug.Log($"<color=green>{message}</color>");
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogW(string message, UnityEngine.Object context = default)
    {
        UnityEngine.Debug.LogWarning($"<color=yellow>{message}</color>.", context);
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogW(string message)
    {
        UnityEngine.Debug.LogWarning($"<color=yellow>{message}</color>");
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogE(string message, UnityEngine.Object context = default)
    {
        UnityEngine.Debug.LogError($"<color=red>{message}</color>.", context);
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogE(string message)
    {
        UnityEngine.Debug.LogError($"<color=red>{message}</color>");
    }

    [Conditional("UNITY_EDITOR")]
    public static void LogCheckAssigned(UnityEngine.Object item, UnityEngine.Object context = default, string message = default)
    {
        if (Utils.IsNull(item))
        {
            UnityEngine.Debug.LogError($"<color=red>{item.name}</color> is not assigned! and Type is <color=orange>{item.GetType().Name}</color>.", context);
        }
    }

    [Conditional("UNITY_EDITOR")]
    public static void DrawL(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color col = default, float duration = 0.0f, bool isDepthTesting = true)
    {
        UnityEngine.Debug.DrawLine(start, end, col, duration, isDepthTesting);
    }

    [Conditional("UNITY_EDITOR")]
    public static void DrawR(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color col = default, float duration = 0.0f, bool isDepthTesting = true)
    {
        UnityEngine.Debug.DrawRay(start, end, col, duration, isDepthTesting);
    }

    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition)
    {
        if (false == condition)
        {
            throw new Exception();
        }
    }
};
