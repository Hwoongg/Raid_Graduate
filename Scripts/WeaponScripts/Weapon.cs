using Photon.Pun;
using System.Collections;
using UnityEngine;

//
// 무기 (총기)에 사용되는 스크립트 입니다.
//

public class Weapon : MonoBehaviour, ILogicEvent
{
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
                this.pvCache = GameObject.Find("Player").GetComponent<PhotonView>();
            }
            return this.pvCache;
        }
    }

    EventSet EventSet;

    [SerializeField] int damagePerShot = 1;
    [SerializeField] protected float timeBetweenBullets = 0.1f;
    [SerializeField] float range = 100f;
    [SerializeField] int MaxBullet = 30;
    int CurrentBullet;

    protected float timer;
    Ray ShootRay;
    RaycastHit ShootHit;
    Vector3 ScreenCenter;
    int shootableMask;

    protected float effectDisplayTime = 0.2f;

    protected bool isReloading;
    public bool IsReloading { get { return isReloading; } set { isReloading = value; } }
    float Reloadtime = 1.5f;
    protected Animator animator;

    [SerializeField] GameObject[] objFireEfx;

    AudioSource GunSound;

    protected virtual void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    protected virtual void Awake()
    {
        shootableMask = LayerMask.GetMask("Enemy");
        isReloading = false;
        GunSound = GetComponent<AudioSource>();
        CurrentBullet = MaxBullet;
    }

    protected virtual void Start()
    {
        enabled = false;
        enabled = true;
        ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        //animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //animator = GetComponent<Animator>();

        // 컴포넌트 위치를 플레이어 오브젝트에서 총기 오브젝트로 옮기기 위해 수정.
        animator = GameObject.Find("Player").GetComponent<Animator>();
    }

    protected virtual void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
        }

        if (!isReloading)
        {
            if (Input.GetMouseButton(0) && (Time.timeScale != 0))
            {
                animator.SetBool("onFire", true);
                LogicEventListener.Invoke(eEventType.FOR_PLAYER, eEventMessage.ON_SHOT_FIRED);
                if (timer >= timeBetweenBullets)
                {
                    Fire();
                }
            }
            else
            {
                animator.SetBool("onFire", false);
            }

            if (timer >= timeBetweenBullets * effectDisplayTime)
            {
                DisableEffects();
            }
        }
        else
        {
            Reload();
        }

    }

    protected virtual void Fire()
    {
        // 타이머 초기화
        timer = 0f;

        CurrentBullet -= 1;

        ShootRay = Camera.main.ScreenPointToRay(ScreenCenter);

        // 각종 이펙트 재생
        GunSound.Play();
        //GunParticle.Stop(); // 파티클은 멈추고 시작해주는 작업 필요.
        //GunParticle.Play();
        for (int i = 0; i < objFireEfx.Length; i++)
        {
            objFireEfx[i].SetActive(true);
        }
        LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.ON_AMMUNITION_COUNT_CHANGED, CurrentBullet, MaxBullet);

        // RayCast
        if (Physics.Raycast(ShootRay, out ShootHit, range, shootableMask))
        {
            var objHealth = ShootHit.collider.gameObject.GetComponent<Health>();
            objHealth.TakeDamage(1);
        }

    }

    void Reload()
    {
        CurrentBullet = MaxBullet;
        StartCoroutine("ReloadingAnimationPlay");
    }

    IEnumerator ReloadingAnimationPlay()
    {
        animator.SetBool("isReloading", true);
        LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.ON_AMMUNITION_COUNT_CHANGED, CurrentBullet, MaxBullet);
        float animatorTime = 0f;

        while (animatorTime < Reloadtime)
        {
            animatorTime += Time.deltaTime;
            animator.SetFloat("ReloadTime", animatorTime);
            yield return null;
        }

        animator.SetBool("isReloading", false);
        animator.SetFloat("ReloadTime", 0f);
        isReloading = false;
        yield break;
    }

    // Enable 방식으로 작동하는 이펙트의 경우 이곳에서 해제합니다.
    protected void DisableEffects()
    {
        for (int i = 0; i < objFireEfx.Length; i++)
        {
            objFireEfx[i].SetActive(false);
        }
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

    void CameraRebound()
    {

    }
}
