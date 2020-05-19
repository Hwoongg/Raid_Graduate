using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletColor
{
    DEFAULT,
    RED,
    BLUE
}

//
// 객체 풀 클래스. 총알에만 해당하여 진행하도록 사용중
//

//public class ObjPool<T> : MonoBehaviour
//{
//    T[] Objects;

//    ObjPool()
//    {
//        Objects = GetComponentsInChildren<T>();
//    }
//}

public class ObjectPool : MonoBehaviour
{
    //[SerializeField] int BulletCount;
    //[SerializeField] GameObject BulletPrefab;

    Bullet[] bullets;
    
    int Counter;

    [SerializeField] Material RedMaterial;
    [SerializeField] Material BlueMaterial;

    private void Awake()
    {
        Counter = 0;
        bullets = GetComponentsInChildren<Bullet>();

        for (int i = 0; i < bullets.Length; i++)
            bullets[i].gameObject.SetActive(false);
    }
    
    
    public void Spawn(Vector3 _pos, Quaternion _rot, BulletColor _bulletColor = BulletColor.DEFAULT)
    {
        // 사용 가능한지 체크
        if (bullets[Counter].gameObject.activeSelf)
            return;

        // 생성
        bullets[Counter].gameObject.SetActive(true);
        bullets[Counter].transform.position = _pos;
        bullets[Counter].transform.rotation = _rot;
        switch(_bulletColor)
        {
            case BulletColor.DEFAULT:
                break;

            case BulletColor.RED:
                bullets[Counter].transform.GetChild(0).
                    GetComponent<MeshRenderer>().material = RedMaterial;
                break;

            case BulletColor.BLUE:
                bullets[Counter].transform.GetChild(0).
                    GetComponent<MeshRenderer>().material = BlueMaterial;
                break;

        }


        // 카운터 상승
        Counter++;
        if (Counter == bullets.Length)
            Counter = 0;
    }

    [ContextMenu("Show Bullet List")]
    void ShowBulletList()
    {
        Debug.Log(bullets.Length);
        Debug.Log("NowCount: "+Counter);
        for (int i = 0; i < bullets.Length; i++)
            Debug.Log(bullets[i]);
    }

    
}
