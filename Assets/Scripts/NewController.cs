using UnityEngine;
using Photon.Pun;
//
// 플레이어의 회전, 이동을 담당하는 컴포넌트 스크립트 입니다.
// 컨트롤러 모드에 따라 카메라 워크 컴포넌트의 모드도 제어됩니다. 
//

public class NewController : MonoBehaviourPun, ILogicEvent
{
    EventSet EventSet;

    public enum Mode
    {
        STOP,
        NORMAL,
        JET,
        AIMING,
        FREECAM,
        SNIPING,
        MODE_OVER
    }
    [HideInInspector] public Mode mode;

    Transform CameraTransform;
    NewTPSCamera TPSCam;

    Transform PlayerTransform;
    Vector3 MoveVector; // 이동량 벡터
    Vector3 MoveDirection;
    public Transform SpineTransform;


    // 일반 비행 설정값들...
    [SerializeField] float MaxSpeed = 1.0f;
    [SerializeField] float Acceleration = 0.1f; // 가속도
    [SerializeField] float Deceleration = 0.5f; // 감속도

    [SerializeField] float XaxisSpeed = 30.0f, YaxisSpeed = 30.0f;
    float X, Y;

    [SerializeField] float MinAngle = -89f, MaxAngle = 89f;

    [SerializeField] float JetMoveSpeed = 20.0f;

    Animator anim;
    //[SerializeField] Animator wingAnim;
    //[SerializeField] GameObject NormalWing;
    //[SerializeField] GameObject EvedeWing;

    float BodyResetTimer;
    Rigidbody myRBody;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Awake()
    {
        mode = Mode.NORMAL;
        MoveVector = Vector3.zero;
        
    }

    void Start()
    {
        CameraTransform = Camera.main.transform;

        anim = GetComponent<Animator>();
        PlayerTransform = GetComponent<Transform>();
        //SpineTransform = anim.GetBoneTransform(HumanBodyBones.Spine);
        TPSCam = CameraTransform.GetComponent<NewTPSCamera>();

        // 초기 카메라 모드 설정
        TPSCam.mode = NewTPSCamera.Mode.NORMAL;

        myRBody = GetComponent<Rigidbody>();
        BodyResetTimer = 0;
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        InputControllerState();
        
        switch (mode)
        {
            case Mode.STOP:
                break;

            case Mode.NORMAL:
                // 카메라 모드 제어
                TPSCam.mode = NewTPSCamera.Mode.NORMAL;
                NormalStateControll();
                break;

            case Mode.JET:
                // 카메라 모드 제어
                TPSCam.mode = NewTPSCamera.Mode.JETFOLLOW;
                JetStateControll();
                break;

            case Mode.AIMING:
                TPSCam.mode = NewTPSCamera.Mode.AIMIMG;
                NormalStateControll();
                break;

            case Mode.FREECAM:
                TPSCam.mode = NewTPSCamera.Mode.FREE;
                NormalStateRotation();
                break;

            case Mode.SNIPING: 
                // 카메라 전환 코드는 제거하는 편이 좋을 듯
                //TPSCam.mode = NewTPSCamera.Mode.SNIPING;
                NormalStateRotation(); // 회전만 한다
                break;

        }

        BodyUpdate();

    }

    private void LateUpdate()
    {
        // 플레이어 상체 회전. 기능 제거됨.
        //SpineRotate();
    }

    // 입력에 따른 플레이어 모드 세팅.
    void InputControllerState()
    {
        // 저격모드일 경우 제어권이 없습니다.
        if (mode == Mode.SNIPING)
            return;


        // ////////////////////////////////////
        //
        // 입력 지속에 따른 모드 전환부 입니다.
        // 입력 중단 시 NORMAL 모드로 자동전환되도록 설계되었습니다.
        // 원치 않을 시 이전에 return 하는 것을 권장합니다.
        //
        // ////////////////////////////////////

        if (Input.GetKey(KeyCode.LeftShift)) // 일반<->회피 기동 모드전환
        {
            mode = Mode.JET;
            //wingAnim.SetBool("onEvade", true);
            anim.SetBool("onEvade", true);
            anim.SetBool("onAiming", false);
            //NormalWing.SetActive(false);
            //EvedeWing.SetActive(true);
        }
        else if (Input.GetMouseButton(1)) // 우클릭 조준 모드
        {
            mode = Mode.AIMING;
            anim.SetBool("onAiming", true);
        }
        else if (Input.GetKey(KeyCode.F)) // F키 자유 시점
        {
            mode = Mode.FREECAM;
        }
        else // 입력 없을 시 일반상태로 회귀
        {
            mode = Mode.NORMAL;
            //wingAnim.SetBool("onEvade", false);
            anim.SetBool("onEvade", false);
            anim.SetBool("onAiming", false);
            //NormalWing.SetActive(true);
            //EvedeWing.SetActive(false);
        }
    }

    // 일반 상태 회전 + 이동 기능입니다.
    void NormalStateControll()
    {

        // 일반 상태 회전
        NormalStateRotation();

        // 일반 상태 이동
        NormalStateMovement();
    }

    void NormalStateRotation()
    {
        //
        // 입력부
        //
        float v = Input.GetAxis("Mouse X");
        float h = Input.GetAxis("Mouse Y");

        X += v * XaxisSpeed * Time.deltaTime;
        Y -= h * YaxisSpeed * Time.deltaTime;

        Y = Mathf.Clamp(Y, MinAngle, MaxAngle);


        // 
        // 처리부
        //
        var rot = Quaternion.Euler(Y + 90f, X + 180f, 0);

        PlayerTransform.rotation = Quaternion.Euler(Y, X + 180f, 0);


        //float v = Input.GetAxis("Mouse X");
        //float h = Input.GetAxis("Mouse Y");

        //X += v * XaxisSpeed * Time.deltaTime;
        //Y -= h * YaxisSpeed * Time.deltaTime;

        //Y = Mathf.Clamp(Y, MinAngle, MaxAngle);

        //PlayerTransform.rotation = Quaternion.AngleAxis(v * XaxisSpeed * Time.deltaTime, PlayerTransform.up) * PlayerTransform.rotation;
        //PlayerTransform.rotation = Quaternion.AngleAxis(-h * YaxisSpeed * Time.deltaTime, PlayerTransform.right) * PlayerTransform.rotation;


        //SpineTransform.rotation = Quaternion.AngleAxis(X, PlayerTransform.up) * SpineTransform.rotation;
        //SpineTransform.rotation = Quaternion.AngleAxis(Y, PlayerTransform.right) * SpineTransform.rotation;

        //SpineTransform.rotation = Quaternion.AngleAxis(45f, PlayerTransform.up) * SpineTransform.rotation;

    }


    void NormalStateMovement()
    {
        // ////////////////////////////////////////////////////
        //
        // 카메라 기준 방향벡터를 이용한 이동변환을 시행합니다.
        // 전역-모델 좌표계 전환에 유의하여 사용해야 합니다.
        // 
        // MoveVector를 기준으로 프레임마다 이동중입니다...
        // 키가 입력되면 입력된 방향으로 MoveVector에 합연산을 진행합니다.
        // 감속은 상시 모든 프레임에 적용됩니다.
        //
        // ////////////////////////////////////////////////////


        //
        // 입력부
        //
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("dirHorizon", x);
        anim.SetFloat("dirVertical", z);
        //wingAnim.SetFloat("dirHorizon", x);
        //wingAnim.SetFloat("dirVertical", z);

        // 월드 기준 벡터들입니다. 이동 시 모델좌표계로 사상시켜야 함.
        var worldY = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            worldY.Set(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            worldY.Set(0, -1, 0);
        }

        var cameraX = CameraTransform.right * x;
        var cameraZ = CameraTransform.forward * z;

        // 최종 이동방향 결정
        var Movement = cameraX + cameraZ + worldY;
        Movement.Normalize(); // 방향만 남기기 위해 정규화
        Movement = PlayerTransform.worldToLocalMatrix * Movement; // Translate()에서 사용하기 위해 플레이어 기준으로 사상시킨다.

        // 가속
        if (MoveVector.magnitude < MaxSpeed)
        {
            MoveVector += Movement * Acceleration * Time.deltaTime;
        }

        // 감속
        MoveVector = MoveVector - (MoveVector * Deceleration * Time.deltaTime);

        PlayerTransform.Translate(MoveVector);
        //PlayerTransform.Translate(Movement * 20.0f * Time.deltaTime);
    }


    void JetStateControll()
    {
        //
        // 입력부
        // 오브젝트 기준 회전입니다.
        // 마우스 좌우 : Z축 회전
        // 마우스 상하 : X축 회전
        // 키보드 좌우 : Y축 회전
        // Z축 회전 시 카메라도 같이 회전이 이루어짐.
        // 이동은 오브젝트의 정면으로만 진행합니다.
        //




        float TurnSpeed = 20.0f;
        anim.SetFloat("dirVertical", 1.0f);
        anim.SetFloat("dirHorizon", 0.0f);


        // 수직축
        float yaw = Input.GetAxis("Horizontal");
        if (yaw != 0)
        {
            var Axis = new Vector3(0, yaw, 0);
            var newAxis = PlayerTransform.rotation * Axis;

            var rot = Quaternion.AngleAxis(TurnSpeed * Time.deltaTime, newAxis);
            PlayerTransform.rotation = rot * PlayerTransform.rotation;
        }

        // 수평축
        float pitch = Input.GetAxis("Mouse Y");
        if (pitch != 0)
        {
            var Axis = new Vector3(pitch, 0, 0);
            var newAxis = PlayerTransform.rotation * Axis;

            var rot = Quaternion.AngleAxis(TurnSpeed * 5.0f * Time.deltaTime, newAxis);
            PlayerTransform.rotation = rot * PlayerTransform.rotation;
        }

        // 전후축
        float roll = Input.GetAxis("Mouse X");
        if (roll != 0)
        {
            var Axis = new Vector3(0, 0, -roll);
            var newAxis = PlayerTransform.rotation * Axis;

            var rot = Quaternion.AngleAxis(TurnSpeed * 5.0f * Time.deltaTime, newAxis);
            PlayerTransform.rotation = rot * PlayerTransform.rotation;
        }


        // 직진 이동
        PlayerTransform.Translate(Vector3.forward * JetMoveSpeed * Time.deltaTime);

    }

    void SpineRotate()
    {
        //float v = Input.GetAxis("Mouse X");
        //float h = Input.GetAxis("Mouse Y");

        //X += v * XaxisSpeed * Time.deltaTime;
        //Y -= h * YaxisSpeed * Time.deltaTime;

        //Y = Mathf.Clamp(Y, MinAngle, MaxAngle);

        //PlayerTransform.rotation = Quaternion.AngleAxis(v * XaxisSpeed * Time.deltaTime, PlayerTransform.up) * PlayerTransform.rotation;
        //PlayerTransform.rotation = Quaternion.AngleAxis(-h * YaxisSpeed * Time.deltaTime, PlayerTransform.right) * PlayerTransform.rotation;


        //SpineTransform.rotation = Quaternion.AngleAxis(X, PlayerTransform.up) * SpineTransform.rotation;
        SpineTransform.rotation = Quaternion.AngleAxis(Y, PlayerTransform.right) * SpineTransform.rotation; // ★사용중

        //SpineTransform.rotation = Quaternion.AngleAxis(45f, PlayerTransform.up) * SpineTransform.rotation;
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_MENU_OPENED:
                enabled = false;
                break;

            case eEventMessage.ON_MENU_CLOSED:
                enabled = true;
                break;

            case eEventMessage.ON_CHAT_UI_OPENED:
                enabled = false;
                break;

            case eEventMessage.ON_CHAT_UI_CLOSED:
                enabled = true;
                break;
        }
    }

    void BodyUpdate()
    {
        BodyResetTimer += Time.deltaTime;

        if (BodyResetTimer > 1.0f)
        {
            myRBody.Sleep();
            BodyResetTimer = 0;
        }
            
    }

    public void ShootRebound(float _rebound = 1.0f)
    {
        Y -= 1.3f * _rebound;
        X += Random.Range(-0.3f, 0.3f) * _rebound;
    }
}
