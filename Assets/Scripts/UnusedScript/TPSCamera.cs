
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 3인칭 카메라 워크 컴포넌트 입니다.
// 카메라 좌표: (0, 3, -3.5)
// 카메라 각도: (10, 0, 0)
// ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
//
public class TPSCamera : MonoBehaviour
{
    public enum Mode
    {
        None,
        Normal,
        Follow
    }
    Transform FollowTarget; // 추적 타겟

    //[SerializeField] private float Distance; // 타겟과의 거리

    private Transform CamTransform; // 카메라 Transform

    //[SerializeField] float CameraFollowingSpeed; // 추적 속도

    [SerializeField] float XaxisSpeed = 30.0f, YaxisSpeed = 30.0f; // 카메라 회전 속도

    float X, Y;

    [SerializeField] float MinAngle = -45f, MaxAngle = 45f; // 카메라 회전각 제한

    //[SerializeField] Vector3 CameraOffset;

    [SerializeField] float PlayerRotationSpeed = 10.0f;

    PlayerControll playerControll;
    
    private void Awake()
    {
        CamTransform = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;

        var vAngles = CamTransform.eulerAngles;
        X = vAngles.y;
        Y = vAngles.x;
    }

    private void Start()
    {
        FollowTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerControll = FollowTarget.GetComponent<PlayerControll>();
    }

    private void Update()
    {
        GetInput();
    }

    private void LateUpdate()
    {
        switch(playerControll.mode)
        {
            case PlayerControll.Mode.Normal:
                NomalStateWork();
                break;

            case PlayerControll.Mode.Jet:
                JetStateWork();
                break;

        }
        

    }

    void GetInput()
    {
        float v = Input.GetAxis("Mouse X");
        float h = Input.GetAxis("Mouse Y");

        X += v * XaxisSpeed * Time.deltaTime;
        Y += h * YaxisSpeed * Time.deltaTime;
        // Clamp the angle.
        // 앵글값 제한.
        Y = Mathf.Clamp(Y, MinAngle, MaxAngle);
    }

    // 플레이어가 통상모드일 때 작동할 카메라 워크입니다.
    void NomalStateWork()
    {
        // 1. 마우스 입력에 기반한 회전. 오일러 기반 쿼터니언 생성.
        var rot = Quaternion.Euler(Y, X, 0.0f);


        // 2. 상대좌표를 회전값만큼 회전. 타겟 위치만큼 이동변환하여 최종 카메라 좌표 생성
        // new Rotation Vector * Distance of camera + Player location + additional camera offset(Height);
        //var newPos = rot * new Vector3(0.0f, 1.0f, -Distance) + FollowTarget.position + CameraOffset;
        var newPos = rot * new Vector3(0.0f, 3.0f, 3.5f) + FollowTarget.position;

        // 3. 위치 적용
        //TF.position = Vector3.LerpUnclamped(TF.position, newPos, Time.fixedDeltaTime * CameraFollowingSpeed);
        CamTransform.position = newPos;


        // 4. 타겟을 바라보도록 회전.
        CamTransform.LookAt(FollowTarget);


        // 5. 대상체 회전. Alt를 누르고있을 시 하지 않도록 설정 추가 필요합니다.3
        FollowTarget.rotation = Quaternion.Slerp(
            FollowTarget.rotation,
            Quaternion.Euler(0.0f, X + 180.0f, 0.0f), // 카메라가 바라보고 있는 방향벡터의 쿼터니언을 만드는 방식 필요할 듯.
            Time.fixedDeltaTime * PlayerRotationSpeed);
    }

    // 플레이어가 회피모드일 때 작동할 카메라 워크입니다.
    void JetStateWork()
    {
        var newPos = FollowTarget.localToWorldMatrix * new Vector4(0.0f, 3.0f, -3.5f, 1);

        transform.position = Vector3.Lerp(CamTransform.position, newPos, Time.fixedDeltaTime * 10.0f);

        CamTransform.LookAt(FollowTarget);
    }
};