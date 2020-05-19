using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILocationPointer : MonoBehaviour, ILogicEvent
{
    EventSet EventSetCache;
    Transform Tf;
    public Transform Player;
    public Transform Anchor;
    [HideInInspector] public Transform Target;
    [SerializeField] float RotatingSpeed;

    void OnEnable()
    {
        EventSetCache = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSetCache);
    }
    
    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSetCache);
    }

    void Start()
    {
        Tf = transform;
        Tf.localScale *= 0.6f;
        Anchor = GameObject.Find("LocationPointerAnchor").transform;
        Anchor.SetParent(Player, false);
        Anchor.localPosition = Vector3.zero;
        Tf.SetParent(Anchor, true);
        Tf.localPosition = new Vector3(
            UnityEngine.Random.insideUnitSphere.x * 4.0f,
            1.0f,
            UnityEngine.Random.insideUnitSphere.z * 4.0f);
    }

    void Update()
    {
        Vector3 Dir = Target.position - Tf.position;
        Dir.y = 0.0f;
        Vector3 newDir = Vector3.RotateTowards(Tf.forward, Dir, RotatingSpeed * Time.deltaTime, 0.0f);
        Debug.DrawRay(Tf.position, newDir, Color.red);
        Tf.rotation = Quaternion.LookRotation(newDir);
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_MISSILE_EXPLOSION:
                if (Target == (obj[0] as Transform))
                {
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
