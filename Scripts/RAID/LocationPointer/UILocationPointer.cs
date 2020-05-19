using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILocationPointer : MonoBehaviour
{
    Transform Tf;
    public Transform Player;
    public Transform Target;
    [SerializeField] float RotatingSpeed;

    void Start()
    {
        Tf = transform;
    }

    void Update()
    {
        Vector3 Dir = Target.position - Tf.position;
        Dir.y = 0.0f;
        Vector3 newDir = Vector3.RotateTowards(Tf.forward, Dir, RotatingSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(Tf.position, newDir, Color.red);
        Tf.rotation = Quaternion.LookRotation(newDir);
    }
}
