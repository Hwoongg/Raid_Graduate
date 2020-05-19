using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMissileLauncher : MonoBehaviour
{
    void Start()
    {
        Dbg.LogCheckAssigned(Missile, this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var missile = Instantiate(Missile, transform.position, transform.rotation);
            missile.GetComponent<Rigidbody>().AddForce(missile.transform.forward * ShootForce);
        }
    }
    [SerializeField] float ShootForce;
    [SerializeField] GameObject Missile;
}
