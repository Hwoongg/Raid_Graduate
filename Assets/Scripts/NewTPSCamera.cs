using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//
// 카메라 워크 스크립트 입니다.
// 플레이어 머리 뒤의 상대좌표를 추적합니다.
// 추적 방식에 따른 모드와 그에 따른 추적 방식 함수가 정의되어 있습니다.
// 추적 좌표 : Normal(0, 3, -3.5) , Jet(0, 3, -4) , AIM(1, 2, -2.5)
//

public class NewTPSCamera : MonoBehaviour, ILogicEvent
{

    EventSet EventSetThis;
    /// <summary>Cache field for the PhotonView on this GameObject.</summary>
    private PhotonView pvCache;

    /// <summary>A cached reference to a PhotonView on this GameObject.</summary>
    /// <remarks>
    /// If you intend to work with a PhotonView in a script, it's usually easier to write this.photonView.
    ///
    /// If you intend to remove the PhotonView component from the GameObject but keep this Photon.MonoBehaviour,
    /// avoid this reference or modify this code to use PhotonView.Get(obj) instead.
    /// </remarks>
    public PhotonView photonView {
        get {
            if (this.pvCache == null)
            {
                this.pvCache = GameObject.Find("LocalPlayer").GetComponent<PhotonView>();
            }
            return this.pvCache;
        }
    }

    public enum Mode
    {
        NONE,
        NORMAL,
        JETFOLLOW,
        AIMIMG,
        FREE,
        SNIPING,
        MODE_OVER
    }
    [HideInInspector] public Mode mode;

    public Transform FollowTarget { get; set; } // 추적 타겟. 플레이어

    Vector3 LookCorrection; // 시점 보정값. 타겟보다 조금 더 위를 바라보도록 상대좌표 지정.

    private Transform CamTransform;

    public Transform NormalAnchor;
    public Transform JetAnchor;
    public Transform AimAnchor;
    public Transform FrontAnchor;
    public Transform SnipingAnchor;

    [SerializeField] float FollowSpeed = 5.0f;

    bool ModeChange;

    float lerpSpeed;

    bool isFreeCamState;
    Vector3 FreeCamPos;
    Quaternion FreeCamRot;

    Camera mainCamera;
    float CameraFOV; // 초기 FOV값 보관용 변수
    [SerializeField] float ZoominFOVParam = 30.0f;

    bool IsMenuOpened = false;
    bool IsChatOpened = false;

    void OnEnable()
    {
        EventSetThis = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSetThis);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSetThis);
    }

    void Awake()
    {
        CamTransform = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mode = Mode.NONE;

        LookCorrection.Set(0, 2.0f, 5.0f);

        mainCamera = GetComponent<Camera>();
        CameraFOV = mainCamera.fieldOfView;
    }

    void Start()
    {
        FollowTarget = GameObject.Find("LocalPlayer").GetComponent<Transform>();
        

        NormalAnchor = GameObject.Find("NormalAnchor").transform;
        JetAnchor = GameObject.Find("JetAnchor").transform;
        AimAnchor = GameObject.Find("AimAnchor").transform;
        FrontAnchor = GameObject.Find("FrontAnchor").transform;
        SnipingAnchor = GameObject.Find("SnipingAnchor").transform;

        NormalAnchor.LookAt(FollowTarget.position + LookCorrection);
        JetAnchor.LookAt(FollowTarget.position + LookCorrection);
        AimAnchor.LookAt(FollowTarget.position + LookCorrection);
        FrontAnchor.LookAt(FollowTarget.position + new Vector3(0, 2, 0));
        SnipingAnchor.rotation = NormalAnchor.rotation;

        //Transform spine = FollowTarget.GetComponent<NewController>().SpineTransform;
        //NormalAnchor.SetParent(spine, true);
        //JetAnchor.SetParent(spine, true);
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_LOCAL_PLAYER_CREATED:
                FollowTarget = (obj[0] as GameObject).transform;
                break;

            case eEventMessage.ON_MENU_OPENED:
                IsMenuOpened = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                enabled = false;
                break;

            case eEventMessage.ON_MENU_CLOSED:
                IsMenuOpened = false;
                if (!IsChatOpened)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                enabled = true;
                break;

            case eEventMessage.ON_CHAT_UI_OPENED:
                IsChatOpened = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                enabled = false;
                break;

            case eEventMessage.ON_CHAT_UI_CLOSED:
                IsChatOpened = false;
                if (!IsMenuOpened)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                enabled = true;
                break;
        }
    }

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.NONE:
                break;

            case Mode.NORMAL:
                NormalStateWork();
                break;

            case Mode.JETFOLLOW:
                JetStateWork();
                break;
                
            case Mode.AIMIMG:
                AimingStateWork();
                break;

            case Mode.FREE:
                FreeStateWork();
                break;

            case Mode.SNIPING:
                SnipingStateWork();
                break;
        }
    }

    // 기본 모드 카메라 워크입니다.
    void NormalStateWork()
    {
        isFreeCamState = false;
        
        //// 타겟 뒤 상대좌표를 직접 계산하는 방식. 현재 오브젝트를 별도로 마련하여 생략했기 때문에 사용하지 않음.
        //var newPos = FollowTarget.localToWorldMatrix * new Vector4(0.0f, 3.0f, -3.5f, 1);

        //CamTransform.position = newPos;
        ////CamTransform.position = Vector3.Slerp(CamTransform.position, newPos, Time.deltaTime * FollowSpeed * 5.0f);
        //CamTransform.LookAt(FollowTarget.position + LookCorrection);
        //if(Vector3.Magnitude(CamTransform.position - NormalAnchor.position) < 0.5f)
        //{
        //    ModeChange = false;
        //}

        if (ModeChange)
        {
            CamTransform.position = Vector3.Slerp(CamTransform.position, NormalAnchor.position, lerpSpeed);
            CamTransform.rotation = Quaternion.Slerp(CamTransform.rotation, NormalAnchor.rotation, lerpSpeed);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, CameraFOV, lerpSpeed);
            

            lerpSpeed += FollowSpeed * Time.deltaTime;
            if (lerpSpeed > 1.0f)
                ModeChange = false;
        }
        else
        {
            CamTransform.position = NormalAnchor.position;
            CamTransform.rotation = NormalAnchor.rotation;
        }

        //CamTransform.position = Vector3.Lerp(CamTransform.position, NormalAnchor.position, Time.deltaTime * FollowSpeed * 5.0f);
        //CamTransform.rotation = Quaternion.Lerp(CamTransform.rotation, NormalAnchor.rotation, Time.deltaTime * FollowSpeed * 5.0f);

    }

    // 추적모드 카메라 워크입니다.
    void JetStateWork()
    {
        //CamTransform.position = JetAnchor.position;
        //CamTransform.rotation = JetAnchor.rotation;
        ModeChange = true;
        lerpSpeed = 0;

        CamTransform.position = Vector3.Slerp(CamTransform.position, JetAnchor.position, Time.deltaTime * FollowSpeed);
        CamTransform.rotation = Quaternion.Slerp(CamTransform.rotation, JetAnchor.rotation, Time.deltaTime * FollowSpeed);


    }

    // 자유시점 카메라 워크입니다.
    void FreeStateWork()
    {
        //FreeRotation();
        if (!isFreeCamState)
        {
            SetFreeCamPosition();
        }

        ModeChange = true;
        lerpSpeed = 0;

        CamTransform.position = Vector3.Slerp(CamTransform.position, FreeCamPos, Time.deltaTime * FollowSpeed);
        CamTransform.rotation = Quaternion.Slerp(CamTransform.rotation, FreeCamRot, Time.deltaTime * FollowSpeed);
    }

    void SetFreeCamPosition()
    {
        FreeCamPos = FrontAnchor.position;
        FreeCamRot = FrontAnchor.rotation;
        isFreeCamState = true;
    }

    // 플레이어를 중심에 놓고 회전합니다.
    void FreeRotation()
    {
        float v = Input.GetAxis("Mouse X");
        float h = Input.GetAxis("Mouse Y");

        var newPos =
            Quaternion.AngleAxis(v * Time.deltaTime * 10.0f, Vector3.up)
            * Quaternion.AngleAxis(h * Time.deltaTime * 10.0f, Vector3.Cross(CamTransform.forward, Vector3.up))
             * CamTransform.position;

        CamTransform.position = newPos;

        CamTransform.LookAt(FollowTarget);
    }

    void TargetTrackRotation()
    {
        // 각도를 잰다.
        Vector3 TargetHorizontal = Vector3.Cross(Vector3.up, FollowTarget.forward);
        float RotAngle = Vector3.SignedAngle(TargetHorizontal, FollowTarget.right, CamTransform.forward);
        
        // 나도 돈다
        Quaternion rot = Quaternion.AngleAxis(RotAngle, CamTransform.forward);
        CamTransform.rotation = rot * CamTransform.rotation;
    }

    void AimingStateWork()
    {
        CamTransform.position = Vector3.Slerp(CamTransform.position, AimAnchor.position, Time.deltaTime * FollowSpeed);
        CamTransform.rotation = Quaternion.Slerp(CamTransform.rotation, AimAnchor.rotation, Time.deltaTime * FollowSpeed);
    }

    void SnipingStateWork()
    {
        CamTransform.position = Vector3.Slerp(CamTransform.position, SnipingAnchor.position, Time.deltaTime * FollowSpeed);
        CamTransform.rotation = Quaternion.Slerp(CamTransform.rotation, NormalAnchor.rotation, Time.deltaTime * FollowSpeed);

        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, ZoominFOVParam, Time.deltaTime * 5.0f);
    }

    void TargetFocusing(Transform Target)
    {

    }

    // 모드 접근용 함수. 두번째 인자는 ModeChange 연출 활성화 여부입니다.
    public void ChangeMode(Mode _mode, bool _change = false)
    {
        mode = _mode;
        ModeChange = _change;
        if (_change)
            lerpSpeed = 0;
    }
}
