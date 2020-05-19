using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//
// 화면 깜빡이 기능이 추가된 플레이어 전용 상속 클래스
//

public class PlayerHealth : Health
{

    Image damageImage;
    [SerializeField] float flashSpeed = 0.5f;
    [SerializeField] Color flashColor = new Color(1f, 0f, 0f, 0.2f);
    [SerializeField] GameObject objBarrier;

    protected override void Awake()
    {
        base.Awake();

        if (Utils.IsNull(damageImage))
        {
            damageImage = GameObject.Find("DamageImg").GetComponent<Image>();
        }
    }

    void Update()
    {
        if (!photonView.IsMine && !PhotonNetwork.IsConnected)
        {
            return;
        }

        // 플레이어용 화면 피격 이펙트 연출
        if (damaged)
        {
            damageImage.color = flashColor;
            objBarrier.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            objBarrier.SetActive(true);
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            objBarrier.transform.localScale = Vector3.Lerp(objBarrier.transform.localScale, new Vector3(3, 3, 3), Time.deltaTime * 20.0f);
        }

        damaged = false;

        if (damageImage.color.a < 0.13f)
        {
            objBarrier.SetActive(false);
        }
    }

    // TakeDamage()는 별도로 선언하지 않으면
    // 묵시적으로 base의 것으로 자동 선언되는 듯 하다.
};
