using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearcher : MonoBehaviour
{
    Aggro Target;
    
    private void OnTriggerEnter(Collider other)
    {
        Aggro newTarget = other.GetComponent<Aggro>();

        // Null Check
        if (newTarget == null)
            return;
        
        if(Target == null)
        {
            Target = newTarget;
        }
        else
        {
            if(Target.GetValue() < newTarget.GetValue())
            {
                Target = newTarget;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Target = null;
    }

    public Transform GetTargetTransform()
    {
        if (Target == null)
            return null;

        return Target.transform;
    }
}
