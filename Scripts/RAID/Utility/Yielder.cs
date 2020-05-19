using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Yielder
{
    #region Fields
    /// <summary>
    /// WaitNext Frame instance.
    /// </summary>
    public static YieldInstruction WaitNextFrame;

    /// <summary>
    /// WaitSec instance is stored here.
    /// </summary>
    internal static Dictionary<float, YieldInstruction> YieldSecondDic =
        new Dictionary<float, YieldInstruction>();

    internal static Dictionary<int, CustomYieldInstruction> YieldUntilDic =
        new Dictionary<int, CustomYieldInstruction>();

    internal static Dictionary<int, CustomYieldInstruction> YieldWhileDic =
        new Dictionary<int, CustomYieldInstruction>();

    /// <summary>
    /// Return the WaitSec YieldInstruction, if there's no created instance,
    /// It will create new one.
    /// </summary>
    /// <param name="second"></param>
    /// <returns></returns>
    internal static YieldInstruction WaitSeconds(float second)
    {
        var t = default(YieldInstruction);
        if (false == YieldSecondDic.ContainsKey(second))
        {
            t = new WaitForSeconds(second);
            YieldSecondDic.Add(second, t);
        }
        else
        {
            t = YieldSecondDic[second];
        }
        return t;
    }

    /// <summary>
    /// Return the WaitUntil, if there's no created instance, 
    /// It will create new one.
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    internal static CustomYieldInstruction WaitUntil(int idx, System.Func<bool> predicate)
    {
        var t = default(CustomYieldInstruction);
        if (false == YieldUntilDic.ContainsKey(idx))
        {
            t = new WaitUntil(predicate);
            YieldUntilDic.Add(idx, t);
        }
        else
        {
            t = YieldUntilDic[idx];
        }
        return t;
    }

    /// <summary>
    /// Return the WaitWhile, it there's no created instance,
    /// It will create new one.
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    internal static CustomYieldInstruction WaitWhile(int idx, System.Func<bool> predicate)
    {
        var t = default(CustomYieldInstruction);
        if (false == YieldWhileDic.ContainsKey(idx))
        {
            t = new WaitWhile(predicate);
            YieldUntilDic.Add(idx, t);
        }
        else
        {
            t = YieldWhileDic[idx];
        }
        return t;
    }
    #endregion

    #region Static Constructor.
    static Yielder()
    {
        WaitNextFrame = new WaitForEndOfFrame();
    }
    #endregion

    #region Accessors
    public static IEnumerator GetCoroutine(float seconds = 1.0f)
    {
        yield return WaitSeconds(seconds);
    }

    public static IEnumerator GetCoroutine(System.Action action, float seconds = 1.0f)
    {
        yield return WaitSeconds(seconds);
        action.Invoke();
    }

    public static IEnumerator GetCoroutine(System.Action action)
    {
        yield return WaitNextFrame;
        action.Invoke();
    }

    public static IEnumerator Break()
    {
        yield break;
    }
    #endregion

};
