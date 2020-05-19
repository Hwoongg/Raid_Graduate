using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZone : MonoBehaviour
{
    [SerializeField] float ReactionPower = 100.0f;

    

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rig = other.GetComponent<Rigidbody>();
        if (rig == null)
            return;

        Vector3 reactionDir = other.transform.position - transform.position;
        reactionDir.Normalize();

        rig.GetComponent<Rigidbody>().AddForce(reactionDir * ReactionPower);
    }
    
}
