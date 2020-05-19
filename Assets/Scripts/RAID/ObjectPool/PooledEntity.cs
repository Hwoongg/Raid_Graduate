using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooledEntity
{
    Pooler Pooler { get; set; }
    void ReturnToPool();
}
