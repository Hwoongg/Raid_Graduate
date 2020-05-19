using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static bool IsNull(UnityEngine.Object o)
    {
        return (null == o || ReferenceEquals(o, null));
    }

    public static bool IsNull(object o)
    {
        return (null == o || ReferenceEquals(o, null));
    }

    public static bool IsValid(UnityEngine.Object o)
    {
        return (null != o && !ReferenceEquals(o, null));
    }

    public static bool IsValid(object o)
    {
        return (null != o && !ReferenceEquals(o, null));
    }

    public static bool AreSame<Ty>(Ty o1, Ty o2) where Ty : UnityEngine.Object
    {
        return (o1 == o2);
    }
};
