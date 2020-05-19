using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTest : MonoBehaviour
{
    Ray ShootRay;
    Vector3 ScreenCenter;
    Transform camTransform;
    [SerializeField] Text aimText;

    int layerMask;

    void Start()
    {
        camTransform = Camera.main.transform;
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        layerMask = 1 << LayerMask.NameToLayer("Enemy");
        
    }
    
    void Update()
    {
        ShootRay = Camera.main.ScreenPointToRay(ScreenCenter);

        
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        

    }

    void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(ShootRay, out hit, 100.0f, layerMask))
        {
            GameObject obj = hit.collider.gameObject;
            aimText.text = "Hit";
        }
        else
        {
            aimText.text = "+";
        }

    }
}
