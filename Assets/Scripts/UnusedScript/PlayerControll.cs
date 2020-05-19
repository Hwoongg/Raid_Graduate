using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 플레이어 이동 조작 컴포넌트 입니다.
// 일반 모드에서는 카메라 시점 기준으로 방향이 입력됩니다.
// 회피 모드에서는 오브젝트 기준으로 입력됩니다.
//

public class PlayerControll : MonoBehaviour
{
    
    [SerializeField]
    Transform CameraTransform;
    TPSCamera TPSCam;

    public enum Mode
    {
        Normal,
        Jet,
        ModeCount
    }
    public Mode mode;

    Vector3 MoveVector; // 이동량 벡터
    Vector3 MoveDirection;


    [SerializeField] float MaxSpeed = 1.0f;
    [SerializeField] float Acceleration = 0.1f; // 가속도
    [SerializeField] float Deceleration = 0.5f; // 감속도

    private void Awake()
    {
        mode = Mode.Normal;
        MoveVector = Vector3.zero;
    }

    void Update()
    {
        // 모드 변경
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            mode++;
            if (mode == Mode.ModeCount)
                mode = 0;

        }

        // 입력 방향 획득
        MoveDirection = InputNormalMode();

        // 이동 방향 유무 체크
        if (MoveDirection != Vector3.zero)
        {
            // 가속 가능 여부 체크. 최대속력 미만일 때만 가속.
            if (MoveVector.magnitude < MaxSpeed)
                MoveVector += (MoveDirection * Acceleration * Time.deltaTime);
            
        }

        // 통상 감속
        Vector3 DecelVector = new Vector3();
        DecelVector = -MoveVector * Deceleration * Time.deltaTime;
        MoveVector += DecelVector;
        
        Debug.Log("감속중..." + MoveVector + MoveVector.magnitude);



        // 이동 행렬 연산 수행
        MoveForMatrix3();


    }


    // 카메라 기준 이동방향 산출 함수
    Vector3 InputNormalMode()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 worldY = Input.GetKey(KeyCode.Space) ? new Vector3(0, 1, 0) : new Vector3(0, 0, 0);
        Vector3 cameraX = CameraTransform.right * x;
        Vector3 cameraZ = CameraTransform.forward * z;

        Vector3 Movement = cameraX + cameraZ + worldY;
        
        return Movement.normalized;
    }

    // 행렬연산 좌표이동 초기형. 기능이상으로 사용하지 않습니다.
    void MoveForMatrix()
    {

        // ///////////////////////////////////////////////////////////////////////
        //
        // 카메라 방향벡터를 현재 모델 좌표계로 사상시켜 이동변환을 시행합니다.
        //
        // ///////////////////////////////////////////////////////////////////////


        // 현재 플레이어 모델 좌표계 행렬
        Matrix4x4 ModelMatrix = new Matrix4x4();
        ModelMatrix.SetTRS(transform.position, transform.rotation, transform.localScale);


        // 카메라 좌표계 행렬
        Matrix4x4 CameraMatrix = new Matrix4x4();
        CameraMatrix.SetTRS(CameraTransform.position, CameraTransform.rotation, CameraTransform.localScale);


        // 행렬곱을 위한 카메라 방향벡터 행렬화
        Vector4 v = new Vector4(); // 행렬화를 위한 Vector4 형변환
        v = CameraTransform.forward; // 임시로 이동방향을 정면으로 고정합니다.
        v.w = 0;
        
        Matrix4x4 CameraVectorMaxrix = new Matrix4x4();
        CameraVectorMaxrix = Matrix4x4.zero; // 영행렬로 초기화
        CameraVectorMaxrix.SetColumn(0, v); // 벡터 요소 Set


        // 계산부. 변환행렬 * 벡터행렬 형식으로 계산합니다.
        // 후에 행렬 * 벡터 연산자가 정의되어있는 것을 발견. 행렬화는 필요없어졌습니다...
        Matrix4x4 Result =  ModelMatrix * CameraVectorMaxrix;

        // 결과값 추출
        MoveVector = Result.GetColumn(0);
        
    }

    void MoveForMatrix2()
    {
        // ////////////////////////////////////////////////////////////////////
        //
        // worldToLocalMatrix 필드를 활용한 함수입니다.
        // 월드->로컬 변환 행렬을 담고있는 필드입니다.
        //
        // ////////////////////////////////////////////////////////////////////

        //Matrix4x4 m = CameraTransform.worldToLocalMatrix;
        Matrix4x4 m = transform.worldToLocalMatrix; // 카메라 공간이 아닌 오브젝트 공간으로 해석해야 함.

        Vector4 mov =  m * CameraTransform.forward;

        MoveVector = mov;
        
    }

    void MoveForMatrix3()
    {
        // ////////////////////////////////////////////////////////////////////
        //
        // Translate는 지역 좌표계 기준으로 이동변환을 시행합니다.
        // Translate를 사용하지 않고, 직접 좌표변환 행렬을 생성하여 변환합니다.
        //
        // ////////////////////////////////////////////////////////////////////

        // 카메라의 방향벡터로 이동행렬 생성
        Matrix4x4 m = new Matrix4x4();
        m.SetTRS(MoveVector, Quaternion.identity, Vector3.one);

        // 이동될 좌표를 Vector4로 형변환. w 필드가 1이어야 이동변환이 가능하다.
        Vector4 newPos = new Vector4();
        newPos = transform.position;
        newPos.w = 1;

        newPos = m * newPos; // 행렬곱

        transform.position = newPos;
    }
}
